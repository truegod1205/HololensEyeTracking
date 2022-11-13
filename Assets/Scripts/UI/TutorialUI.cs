using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TutorialUI : MonoBehaviour
    {
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
        }

        public void FinishUI()
        {
        }

        public void GoToLobbyStage()
        {
            GameStage.GameStageManager.Instance.SetGameStage(GameStage.GameStageType.LobbyStage);
        }
    }
}