using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TestUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject testPage;

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
            this.testPage.SetActive(false);
            this.MaximizeButton.SetActive(false);
        }

        public void MaxmizeTestPage()
        {
            this.MaximizeButton.SetActive(false);
            this.testPage.SetActive(true);
        }

        public void MinimizeTestPage()
        {
            this.testPage.SetActive(false);
            this.MaximizeButton.SetActive(true);
        }

        public void GoToLobbyStage()
        {
            GameStage.GameStageManager.Instance.SetGameStage(GameStage.GameStageType.LobbyStage);
        }
    }
}
