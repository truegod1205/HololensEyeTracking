using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial.Condition
{
    public class CustomTutorialChapterCondition : MonoBehaviour
    {
        protected Action OnConditionTrigger;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RegisterConditionTrigger(Action callback)
        {
            this.OnConditionTrigger += callback;
        }

        public void UnregisterConditionTrigger(Action callback)
        {
            this.OnConditionTrigger -= callback;
        }

        public virtual void ResetCondition()
        {

        }

        public virtual void FinishCondition()
        {

        }
    }
}