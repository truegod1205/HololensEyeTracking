using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class SettingsPageUI : MonoBehaviour
    {
        [SerializeField]
        private LobbyUI lobbyUI;

        [SerializeField]
        private TextMesh languageText;

        [SerializeField]
        private TextMesh teleportSpeedText;

        [SerializeField]
        private TextMesh followSpeedText;

        [SerializeField]
        private TextMesh cubeSpeedText;

        [SerializeField]
        private TextMesh uiDistanceText;

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
            string LocalizationKey = "";
            if (Config.ConfigManager.Instance.Config.language == "Chinese")
            {
                LocalizationKey = "Lobby_Settings_Language_Chinese";
            }
            else
            {
                LocalizationKey = "Lobby_Settings_Language_English";
            }
            this.languageText.text = Localization.LocalizationManager.Instance.GetLocalizationContent(LocalizationKey);
            this.teleportSpeedText.text = Config.ConfigManager.Instance.Config.teleportAngleSpeed.ToString() + "(degree/s)";
            this.followSpeedText.text = Config.ConfigManager.Instance.Config.followAngleSpeed.ToString() + "(degree/s)";
            this.cubeSpeedText.text = Config.ConfigManager.Instance.Config.cubeSpeed.ToString() + "(m/s)";
            this.uiDistanceText.text = Config.ConfigManager.Instance.Config.uiDistance.ToString() + "(cm)";
        }

        public void BackToMainMenuPage()
        {
            this.lobbyUI.BackToMainMenuPage(this.gameObject);
        }

        public void SetLanguage()
        {
            var config = new Config.EyeTrackingTestConfig();
            config.followAngleSpeed = Config.ConfigManager.Instance.Config.followAngleSpeed;
            config.teleportAngleSpeed = Config.ConfigManager.Instance.Config.teleportAngleSpeed;
            config.cubeSpeed = Config.ConfigManager.Instance.Config.cubeSpeed;
            config.uiDistance = Config.ConfigManager.Instance.Config.uiDistance;
            string LocalizationKey = "";
            if (Config.ConfigManager.Instance.Config.language == "Chinese")
            {
                config.language = "English";
                LocalizationKey = "Lobby_Settings_Language_English";
            }
            else
            {
                config.language = "Chinese";
                LocalizationKey = "Lobby_Settings_Language_Chinese";
            }
            Config.ConfigManager.Instance.SaveConfig(config);
            Localization.LocalizationManager.Instance.SetLanguage();
            this.languageText.text = Localization.LocalizationManager.Instance.GetLocalizationContent(LocalizationKey);
        }

        public void SetTeleportSpeed(int diff)
        {
            if (Config.ConfigManager.Instance.Config.teleportAngleSpeed + diff < 0 || Config.ConfigManager.Instance.Config.teleportAngleSpeed + diff > 180)
            {
                return;
            }
            var config = new Config.EyeTrackingTestConfig();
            config.language = Config.ConfigManager.Instance.Config.language;
            config.followAngleSpeed = Config.ConfigManager.Instance.Config.followAngleSpeed;
            config.teleportAngleSpeed = Config.ConfigManager.Instance.Config.teleportAngleSpeed + diff;
            config.cubeSpeed = Config.ConfigManager.Instance.Config.cubeSpeed;
            config.uiDistance = Config.ConfigManager.Instance.Config.uiDistance;
            Config.ConfigManager.Instance.SaveConfig(config);
            this.teleportSpeedText.text = Config.ConfigManager.Instance.Config.teleportAngleSpeed.ToString() + "(degree/s)";
        }

        public void SetFollowSpeed(int diff)
        {
            if (Config.ConfigManager.Instance.Config.followAngleSpeed + diff < 0 || Config.ConfigManager.Instance.Config.followAngleSpeed + diff > 180)
            {
                return;
            }
            var config = new Config.EyeTrackingTestConfig();
            config.language = Config.ConfigManager.Instance.Config.language;
            config.teleportAngleSpeed = Config.ConfigManager.Instance.Config.teleportAngleSpeed;
            config.followAngleSpeed = Config.ConfigManager.Instance.Config.followAngleSpeed + diff;
            config.cubeSpeed = Config.ConfigManager.Instance.Config.cubeSpeed;
            config.uiDistance = Config.ConfigManager.Instance.Config.uiDistance;
            Config.ConfigManager.Instance.SaveConfig(config);
            this.followSpeedText.text = Config.ConfigManager.Instance.Config.followAngleSpeed.ToString() + "(degree/s)";
        }

        public void SetCubeSpeed(float diff)
        {
            if (Config.ConfigManager.Instance.Config.cubeSpeed + diff < 0)
            {
                return;
            }
            var config = new Config.EyeTrackingTestConfig();
            config.language = Config.ConfigManager.Instance.Config.language;
            config.teleportAngleSpeed = Config.ConfigManager.Instance.Config.teleportAngleSpeed;
            config.followAngleSpeed = Config.ConfigManager.Instance.Config.followAngleSpeed;
            config.cubeSpeed = Config.ConfigManager.Instance.Config.cubeSpeed + diff;
            config.uiDistance = Config.ConfigManager.Instance.Config.uiDistance;
            Config.ConfigManager.Instance.SaveConfig(config);
            this.cubeSpeedText.text = Config.ConfigManager.Instance.Config.cubeSpeed.ToString() + "(m/s)";
        }

        public void SetUIDistance(int diff)
        {
            if (Config.ConfigManager.Instance.Config.uiDistance + diff < 0 ||
                Config.ConfigManager.Instance.Config.uiDistance + diff > 200)
            {
                return;
            }
            var config = new Config.EyeTrackingTestConfig();
            config.language = Config.ConfigManager.Instance.Config.language;
            config.teleportAngleSpeed = Config.ConfigManager.Instance.Config.teleportAngleSpeed;
            config.followAngleSpeed = Config.ConfigManager.Instance.Config.followAngleSpeed;
            config.cubeSpeed = Config.ConfigManager.Instance.Config.cubeSpeed;
            config.uiDistance = Config.ConfigManager.Instance.Config.uiDistance + diff;
            Config.ConfigManager.Instance.SaveConfig(config);
            this.uiDistanceText.text = Config.ConfigManager.Instance.Config.uiDistance.ToString() + "(cm)";
        }
    }
}