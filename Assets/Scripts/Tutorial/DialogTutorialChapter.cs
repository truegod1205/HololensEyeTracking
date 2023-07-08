using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class DialogTutorialChapter : TutorialChapter
    {
        [SerializeField]
        private GameObject NextChapterButton;

        [SerializeField]
        private GameObject DailogRoot;

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
            this.DailogRoot.SetActive(true);
            this.StartCoroutine(this.PlayDialog());
        }

        public override void StopChapter()
        {
            this.NextChapterButton.SetActive(false);
            this.DailogRoot.SetActive(false);
        }

        private IEnumerator PlayDialog()
        {
            yield return new WaitForSeconds(dialogLeastTime);
            this.NextChapterButton.SetActive(true);
        }

    }
}