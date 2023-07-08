using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

namespace GamePlay
{
    public class GazeRender : MonoBehaviour
    {
        [SerializeField]
        private GameObject teleportObject;

        [SerializeField]
        private FollowCube followCube;

        [SerializeField]
        private GameObject lockObject;

        [SerializeField]
        private UI.TestUI testUI;

        private static DateTime lastEyeSignalUpdateTimeFromET = DateTime.MinValue;
        private static DateTime lastEyeSignalUpdateTimeLocal = DateTime.MinValue;

        private float lastFollowContinueTime = 0.0f;

        private Vector3 lastGazeDirection = Vector3.zero;
        private Vector3 currentGazeDirection = Vector3.zero;

        private bool isInFollowAngle = false;
        private bool isOutTeleportAngle = false;
        private bool isLockFollow = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void ResetRender()
        {
            this.followCube.gameObject.SetActive(true);
            this.followCube.SetAlpha(0.0f);
            this.lockObject.SetActive(false);
            lastEyeSignalUpdateTimeFromET = DateTime.MinValue;
            lastEyeSignalUpdateTimeLocal = DateTime.MinValue;
            this.lastFollowContinueTime = 0.0f;
            this.isLockFollow = false;
            this.lastGazeDirection = Vector3.zero;
            this.currentGazeDirection = Vector3.zero;
        }

        public void CloseRender()
        {
            this.followCube.gameObject.SetActive(false);
        }

        // Update is called once per frame

        void FixedUpdate()
        {
            if ((CoreServices.InputSystem != null) && (CoreServices.InputSystem.EyeGazeProvider != null) &&
                    CoreServices.InputSystem.EyeGazeProvider.IsEyeTrackingEnabled &&
                    CoreServices.InputSystem.EyeGazeProvider.IsEyeTrackingDataValid)
            {
                if (lastEyeSignalUpdateTimeFromET != CoreServices.InputSystem?.EyeGazeProvider?.Timestamp)
                {
                    //update Direction
                    this.lastGazeDirection = this.currentGazeDirection;
                    this.currentGazeDirection = CoreServices.InputSystem.EyeGazeProvider.GazeDirection.normalized;
                    //update Timestamp
                    lastEyeSignalUpdateTimeFromET = (CoreServices.InputSystem?.EyeGazeProvider?.Timestamp).Value;
                }
                else
                {
                    this.lastGazeDirection = this.currentGazeDirection;
                    //this.currentGazeDirection won't change
                }
            }
            else
            {
                this.lastGazeDirection = Vector3.zero;
                this.currentGazeDirection = Vector3.zero;
            }
            this.UpdateAngle();
            this.UpdateFollowCube();
            this.UpdateTeleportCube();
        }

        private void UpdateAngle()
        {
            if (this.currentGazeDirection == Vector3.zero)
            {
                this.isInFollowAngle = false;
                this.isOutTeleportAngle = false;
                return;
            }

            var angle = -1.0f;
            if (this.lastGazeDirection != Vector3.zero)
            {
                angle = Vector3.Angle(this.lastGazeDirection, this.currentGazeDirection);
            }

            if (angle >= 0.0f && angle < Config.ConfigManager.Instance.Config.followAngleSpeed)
            {
                this.isInFollowAngle = true;
                this.isOutTeleportAngle = false;
            }
            else if (angle > Config.ConfigManager.Instance.Config.teleportAngleSpeed)
            {
                this.isInFollowAngle = false;
                this.isOutTeleportAngle = true;
            }
            else
            {
                this.isInFollowAngle = false;
                this.isOutTeleportAngle = false;
            }

            return;
        }

        private void UpdateFollowCube()
        {
            if (!this.isInFollowAngle)
            {
                lastFollowContinueTime = Time.time;
                this.followCube.SetAlpha(0.0f);
                this.isLockFollow = false;
                this.lockObject.SetActive(false);
            }
            else
            {
                float passTime = Time.time - lastFollowContinueTime;
                if (passTime < 0.1f)
                {
                    this.followCube.SetAlpha(0.0f);
                }
                else if (passTime >= 0.1f && passTime < 3f)
                {
                    this.followCube.SetAlpha(0.25f);
                }
                else
                {
                    if (!this.isLockFollow)
                    {
                        this.isLockFollow = true;
                        this.lockObject.SetActive(true);
                        this.lockObject.transform.position = CoreServices.InputSystem.EyeGazeProvider.GazeOrigin + this.currentGazeDirection.normalized * 2;
                        this.followCube.SetAlpha(0.0f);
                    }
                    else
                    {
                        var hitLockObject = false;
                        RaycastHit[] hits;
                        hits = Physics.RaycastAll(CoreServices.InputSystem.EyeGazeProvider.GazeOrigin, this.currentGazeDirection.normalized, 100.0F);
                        for (int i = 0; i < hits.Length; i++)
                        {
                            if (hits[i].transform.name == this.lockObject.name)
                            {
                                hitLockObject = true;
                                break;
                            }
                        }

                        if (!hitLockObject)
                        {
                            lastFollowContinueTime = Time.time;
                            this.followCube.SetAlpha(0.0f);
                            this.isLockFollow = false;
                            this.lockObject.SetActive(false);
                        }
                    }
                }
            }
            this.followCube.SetPosition(CoreServices.InputSystem.EyeGazeProvider.GazeOrigin + this.currentGazeDirection.normalized * 2);
        }

        private void UpdateTeleportCube()
        {
            if (this.isOutTeleportAngle)
            {
                // GameObject teleportCube = GameObject.Instantiate(teleportObject);
                // teleportCube.transform.position = CoreServices.InputSystem.EyeGazeProvider.GazeOrigin + this.currentGazeDirection.normalized * 2;
            }
        }
    }
}
