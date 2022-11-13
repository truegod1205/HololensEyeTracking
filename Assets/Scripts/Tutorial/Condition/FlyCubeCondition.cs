using Microsoft.MixedReality.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial.Condition
{
    public class FlyCubeCondition : CustomTutorialChapterCondition
    {
        [SerializeField]
        private Transform stareCube;

        [SerializeField]
        private GameObject DialogRoot;

        [SerializeField]
        private GameObject teleportObject;

        [SerializeField]
        private GameObject hintCube;

        [SerializeField]
        private TextMesh successText;

        private static DateTime lastEyeSignalUpdateTimeFromET = DateTime.MinValue;

        private float lastFollowContinueTime = 0.0f;

        private bool isHeadStable = false;
        private bool isAngleLegal = false;
        private bool isDistanceLegal = false;

        private Vector3 lastGazeDirection = Vector3.zero;
        private Vector3 lastHeadDirection = Vector3.zero;
        private Vector3 currentGazeDirection = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void Update()
        {
            if (this.successText.color.a > 0)
            {
                var alpha = this.successText.color.a - Time.deltaTime;
                if (alpha < 0)
                {
                    alpha = 0;
                }
                this.successText.color = new Color(this.successText.color.r, this.successText.color.g,
                    this.successText.color.b, alpha);
            }
        }

        public override void ResetCondition()
        {
            this.stareCube.gameObject.SetActive(true);
            this.hintCube.SetActive(false);
            this.DialogRoot.SetActive(true);
            lastEyeSignalUpdateTimeFromET = DateTime.MinValue;
            this.lastFollowContinueTime = 0.0f;
            this.lastGazeDirection = Vector3.zero;
            this.lastHeadDirection = Vector3.zero;
            this.currentGazeDirection = Vector3.zero;
            this.isHeadStable = false;
            this.isAngleLegal = false;
            this.isDistanceLegal = false;
            this.successText.color = new Color(this.successText.color.r, this.successText.color.g,
                    this.successText.color.b, 0f);
        }

        public override void FinishCondition()
        {
            this.stareCube.gameObject.SetActive(false);
            this.DialogRoot.SetActive(false);
            this.hintCube.SetActive(false);
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
            this.UpdtaeAngleLegal();
            this.UpdateDistanceLegal();
            this.UpdateFollowCube();
            this.UpdateIsHeadStable();
            this.UpdateCondition();
        }

        private void UpdtaeAngleLegal()
        {
            if (this.currentGazeDirection == Vector3.zero)
            {
                this.isAngleLegal = false;
                return;
            }

            var angle = -1.0f;
            if (this.lastGazeDirection != Vector3.zero)
            {
                angle = Vector3.Angle(this.lastGazeDirection, this.currentGazeDirection);
            }

            this.isAngleLegal = (angle >= 0.0f && angle < Config.ConfigManager.Instance.Config.followAngleSpeed);
            return;
        }

        private void UpdateDistanceLegal()
        {
            if (this.currentGazeDirection == Vector3.zero)
            {
                this.isDistanceLegal = false;
                return;
            }

            var focusPosition = CoreServices.InputSystem.EyeGazeProvider.GazeOrigin + this.currentGazeDirection * 2;
            var distance = Vector3.Distance(focusPosition, this.stareCube.position);

            this.isDistanceLegal = distance < 0.4f;
            return;
        }

        private void UpdateFollowCube()
        {
            if (!this.isAngleLegal || !this.isDistanceLegal)
            {
                this.lastFollowContinueTime = Time.time;
            }
            else
            {
                float passTime = Time.time - lastFollowContinueTime;
                if (passTime > 1f)
                {
                    this.lastFollowContinueTime = Time.time;
                    this.StartCoroutine(this.UpdateHintCube());
                }
            }
        }

        private IEnumerator UpdateHintCube()
        {
            this.hintCube.SetActive(true);
            yield return new WaitForSeconds(1f);
            this.hintCube.SetActive(false);
        }

        private void UpdateIsHeadStable()
        {
            if (this.isHeadStable)
            {
                if (this.hintCube.activeInHierarchy)
                {
                    if (this.lastHeadDirection == Vector3.zero)
                    {
                        this.isHeadStable = true;
                        this.lastHeadDirection = Camera.main.transform.forward;
                    }
                    else
                    {
                        var headAngle = Vector3.Angle(this.lastHeadDirection, Camera.main.transform.forward) / Time.deltaTime;
                        if (headAngle > 60)
                        {
                            this.isHeadStable = false;
                            this.lastHeadDirection = Vector3.zero;
                        }
                    }
                }
                else
                {
                    this.isHeadStable = false;
                    this.lastHeadDirection = Vector3.zero;
                }
            }
            else
            {
                if (this.hintCube.activeInHierarchy)
                {
                    this.isHeadStable = true;
                    this.lastHeadDirection = Camera.main.transform.forward;
                }
                else
                {
                    this.isHeadStable = false;
                    this.lastHeadDirection = Vector3.zero;
                }
            }
        }

        private void UpdateCondition()
        {
            if (!this.hintCube.activeInHierarchy)
            {
                return;
            }

            if (!this.isHeadStable)
            {
                return;
            }

            if (this.currentGazeDirection == Vector3.zero)
            {
                return;
            }

            var angle = -1.0f;
            if (this.lastGazeDirection != Vector3.zero)
            {
                angle = Vector3.Angle(this.lastGazeDirection, this.currentGazeDirection);
            }

            if (angle > Config.ConfigManager.Instance.Config.teleportAngleSpeed)
            {
                var telePosition = CoreServices.InputSystem.EyeGazeProvider.GazeOrigin + currentGazeDirection * 2;
                if (Vector3.Distance(this.hintCube.transform.position, telePosition) < 0.4f)
                {
                    GameObject teleportCube = GameObject.Instantiate(teleportObject);
                    teleportCube.transform.position = telePosition;
                    //this.OnConditionTrigger?.Invoke();
                    this.TriggerShowSuccess();
                }
            }
        }

        private void TriggerShowSuccess()
        {
            this.successText.color = new Color(this.successText.color.r, this.successText.color.g,
                    this.successText.color.b, 1.0f);
        }
    }
}