using Microsoft.MixedReality.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial.Condition
{
    public class HeadFollowCondition : CustomTutorialChapterCondition
    {
        [SerializeField]
        private TextMesh successCountText;

        [SerializeField]
        private GameObject DialogRoot;

        [SerializeField]
        private Transform stareCube;

        [SerializeField]
        private Transform headFrontBall;

        [SerializeField]
        private GamePlay.FollowCube followCube;

        [SerializeField]
        private int targetSuccessCount;

        private static DateTime lastEyeSignalUpdateTimeFromET = DateTime.MinValue;

        private float lastFollowContinueTime = 0.0f;

        private bool isAngleLegal = false;
        private bool isDistanceLegal = false;
        private bool isStaring = false;

        private Vector3 lastGazeDirection = Vector3.zero;
        private Vector3 lastHeadDirection = Vector3.zero;
        private Vector3 currentGazeDirection = Vector3.zero;

        private float sumStareTime = 0.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        public override void ResetCondition()
        {
            this.DialogRoot.SetActive(true);
            lastEyeSignalUpdateTimeFromET = DateTime.MinValue;
            this.stareCube.gameObject.SetActive(true);
            this.stareCube.position = new Vector3(0, 0, 2);
            this.followCube.gameObject.SetActive(true);
            this.followCube.SetAlpha(0.0f);
            this.headFrontBall.gameObject.SetActive(true);
            this.lastFollowContinueTime = 0.0f;
            this.lastFollowContinueTime = 0.0f;
            this.lastGazeDirection = Vector3.zero;
            this.lastHeadDirection = Vector3.zero;
            this.currentGazeDirection = Vector3.zero;
            this.isAngleLegal = false;
            this.isDistanceLegal = false;
            this.isStaring = false;
            this.successCountText.text = 0.ToString();
            this.sumStareTime = 0.0f;
        }

        public override void FinishCondition()
        {
            this.stareCube.gameObject.SetActive(false);
            this.DialogRoot.SetActive(false);
            this.followCube.gameObject.SetActive(false);
            this.headFrontBall.gameObject.SetActive(false);
        }

        void Update()
        {
            this.headFrontBall.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
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

            var headFrontPosition = Camera.main.transform.position + Camera.main.transform.forward * 2; ;
            var distance1 = Vector3.Distance(headFrontPosition, this.followCube.transform.position);
            var distance2 = Vector3.Distance(headFrontPosition, this.stareCube.position);

            this.isDistanceLegal = distance1 < 0.4f && distance2 < 0.4f;
            return;
        }

        private void UpdateFollowCube()
        {
            this.followCube.SetPosition(CoreServices.InputSystem.EyeGazeProvider.GazeOrigin + CoreServices.InputSystem.EyeGazeProvider.GazeDirection.normalized * 2);
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

        private void UpdateCondition()
        {
            if (this.isStaring)
            {
                this.sumStareTime += Time.deltaTime;
                this.successCountText.text = ((int)Mathf.Floor(this.sumStareTime / 5f)).ToString();
                if (this.sumStareTime / 5f >= this.targetSuccessCount)
                {
                    this.OnConditionTrigger?.Invoke();
                    return;
                }

                int direction = (int)Mathf.Floor(this.sumStareTime / 5f) % 2;

                if (direction == 0)
                {
                    this.stareCube.transform.position += this.stareCube.transform.right * Config.ConfigManager.Instance.Config.cubeSpeed * Time.deltaTime;
                }
                else
                {
                    this.stareCube.transform.position -= this.stareCube.transform.right * Config.ConfigManager.Instance.Config.cubeSpeed * Time.deltaTime;
                }
            }
        }
    }
}