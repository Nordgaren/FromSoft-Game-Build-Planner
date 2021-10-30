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
using System.Windows.Controls;

namespace FromSoft_Game_Build_Planner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //var settingsPath = UserSettings.UserSettingsPath();

            //if (File.Exists(settingsPath))
            //    UserSettings.LocalUserSettings = UserSettings.GetUserSettings();

            //var exePath = UserSettings.LocalUserSettings.LastExePath;
            //bool result;
            //if (string.IsNullOrWhiteSpace(exePath))
            //{
            //    exePath = BrowseFiles();
            //    result = StartPlanner(exePath);
            //}
            //else
            //{
            //    result = StartPlanner(exePath);
            //}

            //if (!result)
            //    Shutdown();
        }

        //Global event handler memes
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent, new RoutedEventHandler(TextBox_GotFocus));

            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.PreviewMouseDownEvent, new RoutedEventHandler(TextBox_PreviewMouseDown));

            base.OnStartup(e);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();

        }

        private void TextBox_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!textBox.IsFocused)
            {
                textBox.Focus();

                textBox.SelectAll();

                e.Handled = true;
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            UserSettings.LocalUserSettings.Save();
            Shutdown();
        }
    }
}
