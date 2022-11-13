using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {

        private static LocalizationManager instance = null;
        public static LocalizationManager Instance
        {
            get
            {
                return instance;
            }
        }

        private string[][] Array;
        private string prefix = "KeepString";

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
            this.LoadLocalizationFile();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void LoadLocalizationFile()
        {
            TextAsset binAsset = Resources.Load("Language", typeof(TextAsset)) as TextAsset;
            string[] lineArray = binAsset.text.Split("\r"[0]);
            Array = new string[lineArray.Length][];

            Dictionary<string, string> keepStrings = new Dictionary<string, string>();
            List<int> quotationIndexes = new List<int>();
            for (int i = 0; i < lineArray.Length; i++)
            {
                if (lineArray[i].IndexOf('"') != -1)
                {

                    var processIndex = 0;
                    while (lineArray[i].IndexOf('"', processIndex) != -1)
                    {
                        var index = lineArray[i].IndexOf('"', processIndex);
                        quotationIndexes.Add(index);
                        processIndex = index + 1;
                    }

                    var replaceIndex = 0;
                    var aStringBuilder = new StringBuilder(lineArray[i]);
                    for (int j = quotationIndexes.Count - 1; j >= 0; j -= 2)
                    {
                        var sub = lineArray[i].Substring(quotationIndexes[j - 1], quotationIndexes[j] - quotationIndexes[j - 1] + 1);
                        aStringBuilder.Remove(quotationIndexes[j - 1], quotationIndexes[j] - quotationIndexes[j - 1] + 1);
                        aStringBuilder.Insert(quotationIndexes[j - 1], prefix + replaceIndex.ToString());
                        keepStrings.Add(prefix + replaceIndex.ToString(), sub);
                        replaceIndex++;
                    }

                    Array[i] = aStringBuilder.ToString().Split(',');

                    for (int j = 0; j < Array[i].Length; j++)
                    {
                        if (keepStrings.ContainsKey(Array[i][j]))
                        {
                            Array[i][j] = keepStrings[Array[i][j]].Substring(1, keepStrings[Array[i][j]].Length - 2);
                        }
                    }
                    keepStrings.Clear();
                    quotationIndexes.Clear();
                }
                else
                {
                    Array[i] = lineArray[i].Split(',');
                }

            }
        }

        public void SetLanguage()
        {
            LocalizationText[] localizationTexts = Resources.FindObjectsOfTypeAll<LocalizationText>();
            var language = Config.ConfigManager.Instance.Config.language;
            for (int i = 0; i < localizationTexts.Length; i++)
            {
                localizationTexts[i].ApplyLanguaue();
            }
        }

        public string GetLocalizationContent(string key)
        {
            var language = Config.ConfigManager.Instance.Config.language;
            if (Array.Length <= 0)
                return "";
            int nRow = Array.Length;
            int nCol = Array[0].Length;
            for (int i = 1; i < nRow; ++i)
            {
                string strKey = string.Format("\n{0}", key);
                if (Array[i][0] == strKey)
                {
                    for (int j = 0; j < nCol; ++j)
                    {
                        if (Array[0][j] == language)
                        {
                            return Array[i][j];
                        }
                    }
                }
            }
            return "";

        }
    }
}
