﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FromSoft_Game_Build_Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string ExeDir = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        public MainWindow()
        {
            InitializeComponent();
        }

        static UserControl Content;
        static string Name;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var settingsPath = UserSettings.UserSettingsPath();

            if (File.Exists(settingsPath))
                UserSettings.LocalUserSettings = UserSettings.GetUserSettings();

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
                Close();

            WindowTitle.Text = Name;
            MainWindowContent.Content = Content;
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
                Name = "Dark Souls 1";
                Content = DS1;
                return true;
            }
            else if (exePath.EndsWith("DarkSoulsRemastered.exe"))
            {
                UserSettings.LocalUserSettings.LastExePath = exePath;
                var DS1R = new DarkSouls1(System.IO.Path.GetDirectoryName(exePath), true);
                Name = "Dark Souls Remastered";
                Content = DS1R;
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            //Initialize(ExePath);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var exePath = MainWindow.BrowseFiles();
            UserSettings.LocalUserSettings.LastExePath = exePath;
            var result = MainWindow.StartPlanner(exePath);
            if (result)
                Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
