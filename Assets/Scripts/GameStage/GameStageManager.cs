using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStage
{
    public enum GameStageType { InitStage = 0, LobbyStage, TutorialStage, TestStage };

    public class GameStageManager : MonoBehaviour
    {
        private static GameStageManager instance = null;
        public static GameStageManager Instance
        {
            get
            {
                return instance;
            }
        }

        private GameStageType currentGameStage = GameStageType.InitStage;

        public GameStageType CurrentGameStage
        {
            get
            {
                return this.currentGameStage;
            }
        }

        private GameStageType previousGameStage = GameStageType.InitStage;

        public GameStageType PreviousGameStage
        {
            get
            {
                return this.previousGameStage;
            }
        }

        [SerializeField]
        private GameStage lobbyGameStage;

        [SerializeField]
        private GameStage tutorialGameStage;

        [SerializeField]
        private GameStage testGameStage;

        private Dictionary<GameStageType, GameStage> gameStageList;


        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
            this.gameStageList = new Dictionary<GameStageType, GameStage>();
            this.gameStageList.Add(GameStageType.LobbyStage, this.lobbyGameStage);
            this.gameStageList.Add(GameStageType.TutorialStage, this.tutorialGameStage);
            this.gameStageList.Add(GameStageType.TestStage, this.testGameStage);
        }


        // Start is called before the first frame update
        void Start()
        {
            this.Initialize();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Initialize()
        {
            Config.ConfigManager.Instance.LoadConfig();
            Localization.LocalizationManager.Instance.SetLanguage();
            this.SetGameStage(GameStageType.LobbyStage);
        }

        public void SetGameStage(GameStageType stage)
        {
            if (stage == this.currentGameStage)
            {
                return;
            }

            if (this.gameStageList.ContainsKey(this.currentGameStage))
            {
                this.gameStageList[this.currentGameStage]?.LeaveStage();
            }

            this.previousGameStage = this.currentGameStage;
            this.currentGameStage = stage;

            if (this.gameStageList.ContainsKey(this.currentGameStage))
            {
                this.gameStageList[this.currentGameStage].EnterStage();
            }
        }
    }
}