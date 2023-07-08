using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DescriptionPageUI : MonoBehaviour
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
            //TODO init Description text
        }

        public void BackToMainMenuPage()
        {
            this.lobbyUI.BackToMainMenuPage(this.gameObject);
        }
    }
}