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
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            UserSettings.LocalUserSettings.LastDS1Character = ViewModel.Chr;
            //ViewModel = new DS1ViewModel();
            //LoadLastCharacter();
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

        public void ResetArmor()
        {
            acHead.Reset();
            acBody.Reset();
            acArms.Reset();
            acLegs.Reset();
        }

        public void ResetWeapons()
        {
            wcRH1.Reset();
            wcRH2.Reset();
            wcLH1.Reset();
            wcLH2.Reset();
        }

        public void ResetStats()
        {
            nudVit.Value = nudVit.Minimum;
            nudAtt.Value = nudAtt.Minimum;
            nudEnd.Value = nudEnd.Minimum;
            nudStr.Value = nudStr.Minimum;
            nudDex.Value = nudDex.Minimum;
            nudRes.Value = nudRes.Minimum;
            nudInt.Value = nudInt.Minimum;
            nudFai.Value = nudFai.Minimum;
        }

        public void ResetCharacter()
        {
            ViewModel.Chr = new DS1Character();
            ResetArmor();
            ResetWeapons();
            cmbClass.SelectedIndex = 0;
            ResetStats();
            nudHumanity.Value = 0;
        }

        public void SaveCharacter()
        {
            var path = MainWindow.SaveFiles("Build", "json", "Select path to save character");

            var jsonString = JsonConvert.SerializeObject(ViewModel.Chr, Formatting.Indented);

            File.WriteAllText(path, jsonString);
        }

        public void LoadCharacter()
        {
            var path = MainWindow.OpenFiles("Build", "json", "Select saved character");
            var jsonString = File.ReadAllText(path);

            ViewModel.Chr = JsonConvert.DeserializeObject<DS1Character>(jsonString);
            ReloadControls();
        }

        public void LoadLast()
        {
            ReloadControls();
        }
    }
}
