using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FromSoft_Game_Build_Planner
{
    /// <summary>
    /// Interaction logic for ArmorControl.xaml
    /// </summary>
    public partial class ArmorControl : UserControl
    {
        public ArmorControl()
        {
            InitializeComponent();
        }

        public ObservableCollection<DS1Armor> ArmorList
        {
            get { return (ObservableCollection<DS1Armor>)GetValue(ArmorListProperty); }
            set { SetValue(ArmorListProperty, value); }
        }

        public static readonly DependencyProperty ArmorListProperty =
            DependencyProperty.Register("ArmorList", typeof(ObservableCollection<DS1Armor>), typeof(ArmorControl), new PropertyMetadata(default(ObservableCollection<DS1Armor>)));

        public DS1Armor Armor
        {
            get { return (DS1Armor)GetValue(ArmorProperty); }
            set { SetValue(ArmorProperty, value); }
        }

        public static readonly DependencyProperty ArmorProperty =
            DependencyProperty.Register("Armor", typeof(DS1Armor), typeof(ArmorControl), new PropertyMetadata(default(DS1Armor)));



        public int Upgrade
        {
            get { return (int)GetValue(UpgradeProperty); }
            set { SetValue(UpgradeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Upgrade.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpgradeProperty =
            DependencyProperty.Register("Upgrade", typeof(int), typeof(ArmorControl), new PropertyMetadata(0));

        private void Max_Checked(object sender, RoutedEventArgs e)
        {
            HandleMinValue();
        }

        private void HandleMinValue()
        {
            nudUpgrade.Minimum = Max.IsChecked.Value ? Armor.MaxUpgrade : 0;
        }

        private void cmbArmor_Loaded(object sender, RoutedEventArgs e)
        {
            cmbArmor.ItemsSource = ArmorList;
            if (cmbArmor.SelectedIndex == -1)
                cmbArmor.SelectedIndex = 0;
        }

        public void Reload()
        {
            var armorIndex = cmbArmor.Items.GetIndexByProperty<DS1Armor>(x => x.ID == Armor.ID);
            cmbArmor.SelectedIndex = -1;
            cmbArmor.SelectedIndex = armorIndex;
        }

        private void ChangeArmor()
        {
            if (Armor == null)
                return;

            switch (Armor.UpgradePath)
            {
                case DS1Armor.Upgrade.None:
                    nudUpgrade.IsEnabled = false;
                    nudUpgrade.Maximum = 0;
                    break;
                case DS1Armor.Upgrade.Unique:
                    nudUpgrade.Maximum = 5;
                    nudUpgrade.IsEnabled = true;
                    break;
                case DS1Armor.Upgrade.Armor:
                    nudUpgrade.Maximum = 10;
                    nudUpgrade.IsEnabled = true;
                    break;
            }
        }

        private void cmbArmor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nudUpgrade.Minimum = 0;
            nudUpgrade.Maximum = 15;
            HandleMinValue();
            ChangeArmor();
        }
    }
}
