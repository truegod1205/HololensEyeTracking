using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStage
{
    public class LobbyGameStage : GameStage
    {
        [SerializeField]
        private UI.LobbyUI lobbyUI;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void EnterStage()
        {
            this.ResetUI();
            if (GameStageManager.Instance.PreviousGameStage == GameStageType.TutorialStage)
            {
                this.lobbyUI.MaxmizeMainmenu();
                this.lobbyUI.OpenTutorialPage();
            }
        }

        public override void LeaveStage()
        {
            this.CloseUI();
        }

        private void ResetUI()
        {
            this.lobbyUI.gameObject.SetActive(true);
            this.lobbyUI.InitUI();
        }

        private void CloseUI()
        {
            this.lobbyUI.FinishUI();
            this.lobbyUI.gameObject.SetActive(false);
        }
    }
}