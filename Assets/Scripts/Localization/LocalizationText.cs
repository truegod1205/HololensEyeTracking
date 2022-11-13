using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    [RequireComponent(typeof(TextMesh))]
    public class LocalizationText : MonoBehaviour
    {
        [SerializeField]
        private TextMesh localizationText;

        [SerializeField]
        private string localizationKey;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ApplyLanguaue()
        {
            this.localizationText.text = LocalizationManager.Instance.GetLocalizationContent(localizationKey);
        }
    }
}