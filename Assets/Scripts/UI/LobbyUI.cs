using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField]
        private MainMenuUI MainMenuPage;

        [SerializeField]
        private SettingsPageUI SettingsPage;

        [SerializeField]
        private CalibrationPageUI CalibrationPage;

        [SerializeField]
        private DescriptionPageUI DescriptionPage;

        [SerializeField]
        private TutorialPageUI TutorialPage;

        [SerializeField]
        private GameObject MaximizeButton;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitUI()
        {
            this.MaximizeButton.SetActive(true);
        }

        public void FinishUI()
        {
            this.MainMenuPage.gameObject.SetActive(false);
            this.SettingsPage.gameObject.SetActive(false);
            this.CalibrationPage.gameObject.SetActive(false);
            this.DescriptionPage.gameObject.SetActive(false);
            this.TutorialPage.gameObject.SetActive(false);
            this.MaximizeButton.SetActive(false);
        }

        public void OpenSettingsPage()
        {
            this.MainMenuPage.gameObject.SetActive(false);
            this.SettingsPage.gameObject.SetActive(true);
            this.SettingsPage.InitPage();
        }

        public void OpenCalibrationPage()
        {
            this.MainMenuPage.gameObject.SetActive(false);
            this.CalibrationPage.gameObject.SetActive(true);
            this.CalibrationPage.InitPage();
        }

        public void OpenDescriptionPage()
        {
            this.MainMenuPage.gameObject.SetActive(false);
            this.DescriptionPage.gameObject.SetActive(true);
            this.DescriptionPage.InitPage();
        }

        public void OpenTutorialPage()
        {
            this.MainMenuPage.gameObject.SetActive(false);
            this.TutorialPage.gameObject.SetActive(true);
            this.TutorialPage.InitPage();
        }

        public void BackToMainMenuPage(GameObject uiObject)
        {
            uiObject.SetActive(false);
            this.MainMenuPage.gameObject.SetActive(true);
            this.MainMenuPage.InitPage();
        }

        public void MaxmizeMainmenu()
        {
            this.MaximizeButton.SetActive(false);
            this.MainMenuPage.gameObject.SetActive(true);
            this.MainMenuPage.InitPage();
        }

        public void MinimizeMainmenu()
        {
            this.MainMenuPage.gameObject.SetActive(false);
            this.MaximizeButton.SetActive(true);
        }
    }
}