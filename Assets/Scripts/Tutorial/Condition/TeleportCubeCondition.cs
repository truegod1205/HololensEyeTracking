using Microsoft.MixedReality.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial.Condition
{
    public class TeleportCubeCondition : CustomTutorialChapterCondition
    {
        [SerializeField]
        private TextMesh successCountText;

        [SerializeField]
        private GameObject DialogRoot;

        [SerializeField]
        private GameObject teleportObject;

        [SerializeField]
        private GameObject hintCube;

        [SerializeField]
        private List<Vector3> hintCubePosotionList = new List<Vector3>();

        [SerializeField]
        private int targetSuccessCount;

        [SerializeField]
        private TextMesh successText;

        private static DateTime lastEyeSignalUpdateTimeFromET = DateTime.MinValue;

        private bool isAngleLegal = false;
        private bool isDistanceLegal = false;

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
            this.DialogRoot.SetActive(true);
            lastEyeSignalUpdateTimeFromET = DateTime.MinValue;
            this.hintCube.SetActive(true);
            this.lastGazeDirection = Vector3.zero;
            this.lastHeadDirection = Vector3.zero;
            this.currentGazeDirection = Vector3.zero;
            this.isAngleLegal = false;
            this.isDistanceLegal = false;
            this.successCount = 0;
            this.successCountText.text = 0.ToString();
            this.successText.color = new Color(this.successText.color.r, this.successText.color.g,
                    this.successText.color.b, 0f);
            this.StartCoroutine(this.UpdateHintCube());
        }

        public override void FinishCondition()
        {
            this.StopCoroutine(this.UpdateHintCube());
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

            this.UpdateAngleLegal();
            this.UpdateDistanceLegal();
            this.UpdateCondition();
        }

        private void UpdateAngleLegal()
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

            this.isAngleLegal = (angle > Config.ConfigManager.Instance.Config.teleportAngleSpeed);
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
            var distance = Vector3.Distance(focusPosition, this.hintCube.transform.position);
            this.isDistanceLegal = distance < 0.4f;
            return;
        }

        private IEnumerator UpdateHintCube()
        {
            while (true)
            {
                var randomIndex = UnityEngine.Random.Range(0, 100) % this.hintCubePosotionList.Count;
                this.hintCube.transform.position = this.hintCubePosotionList[randomIndex];
                yield return new WaitForSeconds(1f);
            }

        }

        private void UpdateCondition()
        {
            if (this.currentGazeDirection == Vector3.zero)
            {
                return;
            }

            if (this.isAngleLegal && isDistanceLegal)
            {
                var telePosition = CoreServices.InputSystem.EyeGazeProvider.GazeOrigin + currentGazeDirection * 2;
                GameObject teleportCube = GameObject.Instantiate(teleportObject);
                teleportCube.transform.position = telePosition;
                this.successCount++;
                this.TriggerShowSuccess();
                this.successCountText.text = this.successCount.ToString();
                if (this.successCount >= targetSuccessCount)
                {
                    //this.OnConditionTrigger?.Invoke();
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