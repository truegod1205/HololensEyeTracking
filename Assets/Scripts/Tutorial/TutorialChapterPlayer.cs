using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    [Serializable]
    public class TutotialSection
    {
        public List<TutorialChapter> tutorialChapterList = new List<TutorialChapter>();
    }
        

    public class TutorialChapterPlayer : MonoBehaviour
    {
        private static TutorialChapterPlayer instance = null;
        public static TutorialChapterPlayer Instance
        {
            get
            {
                return instance;
            }
        }

        [HideInInspector]
        public int currentTutorialSectionIndex = -1;

        private int currentTutorialChapterIndex = -1;

        [SerializeField]
        private List<TutotialSection> tutorialSectionList = new List<TutotialSection>();

        private List<TutorialChapter> currentTutorialChapterList = null;

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
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ResetTutorial()
        {
            if (GameStage.GameStageManager.Instance.CurrentGameStage == GameStage.GameStageType.TutorialStage &&
                this.currentTutorialChapterList != null && this.currentTutorialChapterIndex >= 0 && 
                this.currentTutorialChapterIndex < this.currentTutorialChapterList.Count)
            {
                this.currentTutorialChapterList[this.currentTutorialChapterIndex].StopChapter();
                this.currentTutorialChapterList[this.currentTutorialChapterIndex].gameObject.SetActive(false);
            }
            this.currentTutorialChapterIndex = -1;
            this.currentTutorialChapterList = null;
        }

        public void PlayNextTutorialChapter()
        {
            if (this.currentTutorialChapterList == null)
            {
                Debug.LogError("Wrong Tutorial Chapter List.");
                GameStage.GameStageManager.Instance.SetGameStage(GameStage.GameStageType.LobbyStage);
                return;
            }

            if (this.currentTutorialChapterIndex >= 0 && this.currentTutorialChapterIndex < this.currentTutorialChapterList.Count)
            {
                this.currentTutorialChapterList[this.currentTutorialChapterIndex].StopChapter();
                this.currentTutorialChapterList[this.currentTutorialChapterIndex].gameObject.SetActive(false);
            }

            this.currentTutorialChapterIndex++;

            if (this.currentTutorialChapterIndex >= this.currentTutorialChapterList.Count)
            {
                GameStage.GameStageManager.Instance.SetGameStage(GameStage.GameStageType.LobbyStage);
            }
            else
            {
                this.currentTutorialChapterList[this.currentTutorialChapterIndex].gameObject.SetActive(true);
                this.currentTutorialChapterList[this.currentTutorialChapterIndex].PlayChapter();
            }
        }

        public void PlayTutorialSection()
        {
            if (currentTutorialSectionIndex < 0 || currentTutorialSectionIndex >= this.tutorialSectionList.Count)
            {
                Debug.LogError("Wrong Section Index.");
                GameStage.GameStageManager.Instance.SetGameStage(GameStage.GameStageType.LobbyStage);
                return;
            }
            this.currentTutorialChapterIndex = -1;
            this.currentTutorialChapterList = this.tutorialSectionList[currentTutorialSectionIndex].tutorialChapterList;
            this.PlayNextTutorialChapter();
        }
    }
}