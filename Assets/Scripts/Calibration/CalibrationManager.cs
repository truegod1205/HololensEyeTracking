using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

namespace Calibration
{
    public class CalibrationManager : MonoBehaviour
    {
        private static CalibrationManager instance = null;
        public static CalibrationManager Instance
        {
            get
            {
                return instance;
            }
        }

        [SerializeField]
        private bool editorTestUserIsCalibrated = true;

        public Action<bool> OnEyeCalibrationDetected;

        private bool? prevCalibrationStatus = null;

        public bool CalibrationStatus
        {
            get
            {
                bool? calibrationStatus;
                if (Application.isEditor)
                {
                    calibrationStatus = editorTestUserIsCalibrated;
                }
                else
                {
                    calibrationStatus = CoreServices.InputSystem?.EyeGazeProvider?.IsEyeCalibrationValid;
                }

                if (calibrationStatus.HasValue)
                {
                    return calibrationStatus.Value;
                }

                return false;
            }
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            bool? calibrationStatus;

            if (Application.isEditor)
            {
                calibrationStatus = editorTestUserIsCalibrated;
            }
            else
            {
                calibrationStatus = CoreServices.InputSystem?.EyeGazeProvider?.IsEyeCalibrationValid;
            }

            if (calibrationStatus.HasValue)
            {
                if (prevCalibrationStatus != calibrationStatus)
                {
                    if (!calibrationStatus.Value)
                    {
                        OnEyeCalibrationDetected?.Invoke(false);
                    }
                    else
                    {
                        OnEyeCalibrationDetected?.Invoke(true);
                    }
                    prevCalibrationStatus = calibrationStatus;
                }
            }
        }


    }
}