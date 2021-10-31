using Microsoft.Win32;
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

        private static UserControl CurrentPlanner;
        private static string GameName;

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

            WindowTitle.Text = GameName;
            MainWindowContent.Content = CurrentPlanner;
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
                GameName = "Dark Souls 1";
                CurrentPlanner = DS1;
                return true;
            }
            else if (exePath.EndsWith("DarkSoulsRemastered.exe"))
            {
                UserSettings.LocalUserSettings.LastExePath = exePath;
                var DS1R = new DarkSouls1(System.IO.Path.GetDirectoryName(exePath), true);
                GameName = "Dark Souls Remastered";
                CurrentPlanner = DS1R;
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
            var planner = CurrentPlanner as IFSPlanner;
            planner.Reload();

            //var result = StartPlanner(UserSettings.LocalUserSettings.LastExePath);
            //if (!result)
            //    Close();

            //WindowTitle.Text = GameName;
            //MainWindowContent.Content = CurrentPlanner;
            //Initialize(ExePath);
        }

        private void SelectExe_Click(object sender, RoutedEventArgs e)
        {
            var exePath = BrowseFiles();
            UserSettings.LocalUserSettings.LastExePath = exePath;
            var result = StartPlanner(exePath);
            if (!result)
                Close();

            WindowTitle.Text = GameName;
            MainWindowContent.Content = CurrentPlanner;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ContextMenu contextMenu = button.ContextMenu;
            contextMenu.PlacementTarget = button;
            contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            contextMenu.IsOpen = true;
        }
    }
}
