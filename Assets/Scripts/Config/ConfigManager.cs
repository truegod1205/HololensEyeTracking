using System;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    public class EyeTrackingTestConfig
    {
        [DefaultValue("Chinese")]
        public string language;
        [DefaultValue(15f)]
        public float teleportAngleSpeed;
        [DefaultValue(5f)]
        public float followAngleSpeed;
        [DefaultValue(0.15f)]
        public float cubeSpeed;
        [DefaultValue(60)]
        public int uiDistance;
    }

    public class ConfigManager : MonoBehaviour
    {
        private static ConfigManager instance = null;
        public static ConfigManager Instance
        {
            get
            {
                return instance;
            }
        }

        private EyeTrackingTestConfig config;

        public EyeTrackingTestConfig Config
        {
            get
            {
                return this.config;
            }
        }

        public Action OnConfigChange;

        private string defaultLanguage = "Chinese";

        private float defaultTeleportAngleSpeed = 15f;

        private float defaultFollowAngleSpeed = 5f;

        private float defaultCubeSpeed = 0.15f;

        private int defaultUIDistance = 60;

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
            this.config = new EyeTrackingTestConfig();
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadConfig()
        {
            var filePath = Application.persistentDataPath + "/config.json";
            if (!File.Exists(filePath))
            {
                this.config.language = this.defaultLanguage;
                this.config.teleportAngleSpeed = this.defaultTeleportAngleSpeed;
                this.config.followAngleSpeed = this.defaultFollowAngleSpeed;
                this.config.cubeSpeed = this.defaultCubeSpeed;
                this.config.uiDistance = this.defaultUIDistance;
                string output = JsonConvert.SerializeObject(this.config);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(output);
                sw.Close();
            }
            else
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.DefaultValueHandling = DefaultValueHandling.Populate;

                    var readedConfig = sr.ReadToEnd();
                    this.config = JsonConvert.DeserializeObject<EyeTrackingTestConfig>(readedConfig, settings);
                }
            }
        }

        public void SaveConfig(EyeTrackingTestConfig saveConfig)
        {
            var filePath = Application.persistentDataPath + "/config.json";
            this.config.language = saveConfig.language;
            this.config.teleportAngleSpeed = saveConfig.teleportAngleSpeed;
            this.config.followAngleSpeed = saveConfig.followAngleSpeed;
            this.config.cubeSpeed = saveConfig.cubeSpeed;
            this.config.uiDistance = saveConfig.uiDistance;
            this.OnConfigChange?.Invoke();
            string output = JsonConvert.SerializeObject(this.config);

            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(output);
            sw.Close();
        }
    }
}