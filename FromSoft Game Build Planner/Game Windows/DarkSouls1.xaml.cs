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
using Newtonsoft.Json.Linq;

namespace FromSoft_Game_Build_Planner
{
    /// <summary>
    /// Interaction logic for DarkSouls1.xaml
    /// </summary>
    public partial class DarkSouls1 : UserControl, IFSPlanner
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

            if (UserSettings.LocalUserSettings.LastDS1Character != null)
                LoadLastCharacter();
            //if (UserSettings.LocalUserSettings.LastDS1Character != null)
            //    ViewModel.Chr = UserSettings.LocalUserSettings.LastDS1Character;
        }

        private void CloseControl()
        {
           UserSettings.LocalUserSettings.LastDS1Character = ViewModel.Chr;
        }

        public void Reload()
        {
            //wcRH1.cmbWeapon.SelectedItem = ViewModel.Chr.RHandWeapon2;
            UserSettings.LocalUserSettings.LastDS1Character = ViewModel.Chr;
            ViewModel = new DS1ViewModel();
            LoadLastCharacter();
        }

        private void LoadLastCharacter()
        {
            var chr = UserSettings.LocalUserSettings.LastDS1Character;

            if (chr is JObject jObj)
                chr = jObj.ToObject<DS1Character>();

            ViewModel.Chr = (DS1Character)chr;
            ReloadControls();
        }

        private void ReloadControls()
        {
            wcRH1.Reload();
            wcRH2.Reload();
            wcLH1.Reload();
            wcLH2.Reload();

            cmbClass.SelectedIndex = cmbClass.Items.GetIndexByProperty<DS1Class>(x => x.ID == ViewModel.Chr.Class.ID);

            acHead.Reload();
            acBody.Reload();
            acArms.Reload();
            acLegs.Reload();
        }

        private void DS1Planner_Unloaded(object sender, RoutedEventArgs e)
        {
            CloseControl();
        }

    }
}
