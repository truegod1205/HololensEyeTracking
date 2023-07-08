using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CalibrationPageUI : MonoBehaviour
    {
        [SerializeField]
        private LobbyUI lobbyUI;

        [SerializeField]
        private GameObject EyeCalibrationToogle;

        // Start is called before the first frame update
        void Start()
        {
            Calibration.CalibrationManager.Instance.OnEyeCalibrationDetected += this.OnEyeCalibrationChangeDetect;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitPage()
        {
            var value = Calibration.CalibrationManager.Instance.CalibrationStatus;
            this.EyeCalibrationToogle.SetActive(value);
        }

        public void BackToMainMenuPage()
        {
            this.lobbyUI.BackToMainMenuPage(this.gameObject);
        }

        private void OnEyeCalibrationChangeDetect(bool value)
        {
            this.EyeCalibrationToogle.SetActive(value);
        }
    }
}