using Microsoft.MixedReality.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial.Condition
{
    public class FollowCubeCondition : CustomTutorialChapterCondition
    {
        [SerializeField]
        private TextMesh successCountText;

        [SerializeField]
        private GamePlay.FollowCube followCube;

        [SerializeField]
        private Transform stareCube;

        [SerializeField]
        private GameObject DialogRoot;

        [SerializeField]
        private TextMesh successText;

        [SerializeField]
        private List<Vector3> stareCubePosotionList = new List<Vector3>();

        private static DateTime lastEyeSignalUpdateTimeFromET = DateTime.MinValue;

        private float lastFollowContinueTime = 0.0f;

        private bool isHeadStable = false;
        private bool isAngleLegal = false;
        private bool isDistanceLegal = false;
        private bool isStaring = false;

        private Vector3 lastGazeDirection = Vector3.zero;
        private Vector3 lastHeadDirection = Vector3.zero;
        private Vector3 currentGazeDirection = Vector3.zero;

        private int successCount = 0;

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
            this.stareCube.position = stareCubePosotionList[0];
            this.DialogRoot.SetActive(true);
            this.followCube.gameObject.SetActive(true);
            this.followCube.SetAlpha(0.0f);
            lastEyeSignalUpdateTimeFromET = DateTime.MinValue;
            this.lastFollowContinueTime = 0.0f;
            this.lastGazeDirection = Vector3.zero;
            this.lastHeadDirection = Vector3.zero;
            this.currentGazeDirection = Vector3.zero;
            this.isHeadStable = false;
            this.isAngleLegal = false;
            this.isDistanceLegal = false;
            this.isStaring = false;
            this.successCount = 0;
            this.successCountText.text = 0.ToString();
            this.successText.color = new Color(this.successText.color.r, this.successText.color.g,
                    this.successText.color.b, 0f);
        }

        public override void FinishCondition()
        {
            this.stareCube.gameObject.SetActive(false);
            this.DialogRoot.SetActive(false);
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
            this.UpdtaeAngleLegal();
            this.UpdateDistanceLegal();
            this.UpdateFollowCube();
            this.UpdateIsHeadStable();
            this.UpdateStareCube();
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
            this.followCube.SetPosition(CoreServices.InputSystem.EyeGazeProvider.GazeOrigin + this.currentGazeDirection * 2);
            if (!this.isAngleLegal || !this.isDistanceLegal)
            {
                lastFollowContinueTime = Time.time;
                this.followCube.SetAlpha(0.0f);
                this.isStaring = false;
            }
            else
            {
                float passTime = Time.time - lastFollowContinueTime;
                if (passTime < 0.1f)
                {
                    this.followCube.SetAlpha(0.0f);
                }
                else
                {
                    this.followCube.SetAlpha(0.25f);
                    this.isStaring = true;
                }
            }
        }

        private void UpdateIsHeadStable()
        {
            if (this.isHeadStable)
            {
                if (this.isStaring)
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
                if (this.isStaring)
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

        private void UpdateStareCube()
        {
            if (this.successCount >= this.stareCubePosotionList.Count - 1)
            {
                //this.OnConditionTrigger?.Invoke();
                return;
            }

            if (this.isStaring && this.isHeadStable)
            {
                this.stareCube.transform.position =
                    Vector3.MoveTowards(this.stareCube.transform.position,
                    this.stareCubePosotionList[this.successCount + 1],
                    Config.ConfigManager.Instance.Config.cubeSpeed * Time.deltaTime);

                if (Vector3.Distance(this.stareCube.transform.position, this.stareCubePosotionList[this.successCount + 1]) < 0.05f)
                {
                    this.successCount++;
                    this.successCountText.text = this.successCount.ToString();
                    this.stareCube.transform.position = this.stareCubePosotionList[this.successCount];
                    this.TriggerShowSuccess();
                }
            }
            else
            {
                this.stareCube.transform.position = this.stareCubePosotionList[this.successCount];
            }
        }

        private void TriggerShowSuccess()
        {
            this.successText.color = new Color(this.successText.color.r, this.successText.color.g,
                    this.successText.color.b, 1.0f);
        }
    }
}