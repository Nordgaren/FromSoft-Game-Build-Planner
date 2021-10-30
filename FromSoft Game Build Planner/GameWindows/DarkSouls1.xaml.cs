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

        private int SoulLevel;

        public static bool DSR = false;

        public static string ExePath;

        //public static bool NotLoading { get; set; }

        //public DS1Character Chr = new DS1Character();

        public DarkSouls1(string exePath, bool dsr)
        {

            DSR = dsr;
            ExePath = exePath;
            //if (DSR)
            //    DS1Planner.Title = "Dark Souls: Remastered";

            //DataContext = Chr;
            //Initialize(ExePath);
            
            InitializeComponent();

            //wcRH1.SetChr(Chr);
            //wcRH2.SetChr(Chr);
            //wcLH1.SetChr(Chr);
            //wcLH2.SetChr(Chr);
            //wcRH1.CalculatAR();
            //wcRH2.CalculatAR();
            //wcLH1.CalculatAR();
            //wcLH2.CalculatAR();
            //NotLoading = true;

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
            UserSettings.LocalUserSettings.LastDS1Character = ViewModel.Chr;
        }

    }
}
