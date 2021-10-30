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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using SharpDX.Direct3D9;
using SF_Compatible_DRB_Icon_Appender;
using SoulsFormats;
using SharpDX.Direct3D9;
using System.Collections.ObjectModel;

namespace FromSoft_Game_Build_Planner
{
    /// <summary>
    /// Interaction logic for DarkSouls1.xaml
    /// </summary>
    public partial class DarkSouls1 : UserControl
    {
        public static string ExePath;

        public DarkSouls1(string exePath, bool dsr)
        {
            DS1ViewModel.DSR = dsr;
            
            InitializeComponent();
        }

        private void DS1Planner_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this) // get the parent window
               .Closing += (s1, e1) => CloseControl();
            //if (UserSettings.LocalUserSettings.LastDS1Character != null)
            //    ViewModel.Chr = UserSettings.LocalUserSettings.LastDS1Character;
        }

        private void CloseControl()
        {
            //UserSettings.LocalUserSettings.LastDS1Character = ViewModel.Chr;
        }

    }
}
