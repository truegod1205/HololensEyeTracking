using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStage
{
    public class TutorialGameStage : GameStage
    {
        [SerializeField]
        private UI.TutorialUI tutorialUI;

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
            Tutorial.TutorialChapterPlayer.Instance.ResetTutorial();
            Tutorial.TutorialChapterPlayer.Instance.PlayTutorialSection();
        }

        public override void LeaveStage()
        {
            this.CloseUI();
            Tutorial.TutorialChapterPlayer.Instance.ResetTutorial();
        }

        private void ResetUI()
        {
            this.tutorialUI.gameObject.SetActive(true);
            this.tutorialUI.InitUI();
        }

        private void CloseUI()
        {
            this.tutorialUI.FinishUI();
            this.tutorialUI.gameObject.SetActive(false);
        }
    }
}