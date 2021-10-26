using System;
using System.Collections.Generic;
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

        public string LastExePath;

        public DS1Character LastDS1Character;

        public void Save(string path)
        {
            var jsonString = JsonConvert.SerializeObject(LocalUserSettings, Formatting.Indented);
            File.WriteAllText(path, jsonString);
        }

        public static UserSettings GetUserSettings(string path)
        {
            var jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<UserSettings>(jsonString);
        }
    }
}
