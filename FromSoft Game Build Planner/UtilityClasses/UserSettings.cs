using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FromSoft_Game_Build_Planner
{
    class UserSettings
    {
        public static UserSettings LocalUserSettings = new UserSettings();
        
        private static string SettingsPath = $@"{MainWindow.ExeDir}\BuildPlannerSettings.json";

        public string LastExePath;

        public object LastDS1Character;

        public void Save()
        {
            var jsonString = JsonConvert.SerializeObject(LocalUserSettings, Formatting.Indented);
            File.WriteAllText(SettingsPath, jsonString);
        }

        public static UserSettings GetUserSettings()
        {
            var jsonString = File.ReadAllText(SettingsPath);
            var jsonSerializerSettings = new JsonSerializerSettings();

            return JsonConvert.DeserializeObject<UserSettings>(jsonString);
        }

        public static string UserSettingsPath()
        {
            return SettingsPath;
        }

    }
}
