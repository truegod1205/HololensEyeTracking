using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class CustomTutorialChapter : TutorialChapter
    {
        [SerializeField]
        private Condition.CustomTutorialChapterCondition condition;

        [SerializeField]
        private GameObject NextChapterButton;

        [SerializeField]
        private float dialogLeastTime = 0;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void PlayChapter()
        {
            //this.condition.RegisterConditionTrigger(this.OnConditionTrigger);
            this.StartCoroutine(this.PlayDialog());
            this.condition.ResetCondition();
        }

        public override void StopChapter()
        {
            this.NextChapterButton.SetActive(false);
            this.condition.FinishCondition();
        }

        private void OnConditionTrigger()
        {
            this.NextChapterButton.SetActive(true);
        }

        private IEnumerator PlayDialog()
        {
            yield return new WaitForSeconds(dialogLeastTime);
            this.NextChapterButton.SetActive(true);
        }
    }
}
