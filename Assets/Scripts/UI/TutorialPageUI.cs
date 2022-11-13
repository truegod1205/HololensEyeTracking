using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TutorialPageUI : MonoBehaviour
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
        }

        public void BackToMainMenuPage()
        {
            this.lobbyUI.BackToMainMenuPage(this.gameObject);
        }

        public void GoToTutorialSection(int index)
        {
            Tutorial.TutorialChapterPlayer.Instance.currentTutorialSectionIndex = index;
            GameStage.GameStageManager.Instance.SetGameStage(GameStage.GameStageType.TutorialStage);
        }
    }
}
