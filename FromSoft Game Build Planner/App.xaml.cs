using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FromSoft_Game_Build_Planner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string ExeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        private string SettingsPath = $@"{ExeDir}\BuildPlannerSettings.json";

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (File.Exists(SettingsPath))
                UserSettings.LocalUserSettings = UserSettings.GetUserSettings(SettingsPath);

            var exePath = UserSettings.LocalUserSettings.LastExePath;
            bool result;
            if (string.IsNullOrWhiteSpace(exePath))
            {
                exePath = BrowseFiles();
                result = StartPlanner(exePath);
            }
            else
            {
                result = StartPlanner(exePath);
            }

            if (!result)
                Shutdown();
        }

        public static string BrowseFiles()
        {
            var ofd = new OpenFileDialog();

            var result = ofd.ShowDialog();

            if (result.HasValue)
            {
                return ofd.FileName;
            }

            return null;
        }

        public static bool StartPlanner(string exePath)
        {
            if (exePath.EndsWith("DARKSOULS.exe"))
            {
                UserSettings.LocalUserSettings.LastExePath = exePath;
                var DS1 = new DarkSouls1(System.IO.Path.GetDirectoryName(exePath), false);
                DS1.Show();
                return true;
            }
            else if (exePath.EndsWith("DarkSoulsRemastered.exe"))
            {
                UserSettings.LocalUserSettings.LastExePath = exePath;
                var DS1R = new DarkSouls1(System.IO.Path.GetDirectoryName(exePath), true);
                DS1R.Show();
                return true;
            }
            else if (string.IsNullOrWhiteSpace(exePath))
            {
                return false;
            }
            else
            {
                MessageBox.Show("No Supported game detected");
                exePath = BrowseFiles();
                return StartPlanner(exePath);
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            UserSettings.LocalUserSettings.Save(SettingsPath);
            Shutdown();
        }
    }
}
