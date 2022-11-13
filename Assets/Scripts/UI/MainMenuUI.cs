using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private LobbyUI lobbyUI;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitPage()
        {
            //TODO init setting value
        }

        public void OpenSettingsPage()
        {
            this.lobbyUI.OpenSettingsPage();
        }

        public void OpenCalibrationPage()
        {
            this.lobbyUI.OpenCalibrationPage();
        }

        public void OpenDescriptionPage()
        {
            this.lobbyUI.OpenDescriptionPage();
        }

        public void OpenTutorialPage()
        {
            this.lobbyUI.OpenTutorialPage();
        }

        public void MinimizeMainmenu()
        {
            this.lobbyUI.MinimizeMainmenu();
        }

        public void GoToTestStage()
        {
            GameStage.GameStageManager.Instance.SetGameStage(GameStage.GameStageType.TestStage);
        }

        public void QuitApp()
        {
            Application.Quit();
        }
    }
}