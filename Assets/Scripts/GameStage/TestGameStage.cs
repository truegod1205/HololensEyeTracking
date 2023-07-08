using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStage
{
    public class TestGameStage : GameStage
    {
        [SerializeField]
        private UI.TestUI testUI;

        [SerializeField]
        private GamePlay.GazeRender gazeRender;

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
            this.gazeRender.gameObject.SetActive(true);
            this.gazeRender.ResetRender();
        }

        public override void LeaveStage()
        {
            this.CloseUI();
            this.gazeRender.gameObject.SetActive(false);
        }

        private void ResetUI()
        {
            this.testUI.gameObject.SetActive(true);
            this.testUI.InitUI();
        }

        private void CloseUI()
        {
            this.testUI.FinishUI();
            this.testUI.gameObject.SetActive(false);
        }
    }
}