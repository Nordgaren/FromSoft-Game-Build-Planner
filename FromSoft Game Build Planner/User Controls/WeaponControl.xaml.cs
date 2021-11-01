using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for WeaponControl.xaml
    /// </summary>
    public partial class WeaponControl : UserControl
    {
        public WeaponControl()
        {
            InitializeComponent();
            WeaponListCollectionView = new ListCollectionView(DS1Weapon.WeaponsList);
            WeaponListCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));
            //WeaponListCollectionView.Filter += FilterWeapons;
            cmbWeapon.ItemsSource = WeaponListCollectionView;
        }

        public ICollectionView WeaponListCollectionView { get; private set; }

        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControlName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlNameProperty =
            DependencyProperty.Register("ControlName", typeof(string), typeof(WeaponControl), new PropertyMetadata(""));

        public DS1DamageModel Damage
        {
            get { return (DS1DamageModel)GetValue(DamageProperty); }
            set { SetValue(DamageProperty, value); }
        }

        public static readonly DependencyProperty DamageProperty =
            DependencyProperty.Register("Damage", typeof(DS1DamageModel), typeof(WeaponControl), new PropertyMetadata(UpdateValues));

        public DS1Weapon Weapon
        {
            get { return (DS1Weapon)GetValue(WeaponProperty); }
            set { SetValue(WeaponProperty, value); }
        }

        public static readonly DependencyProperty WeaponProperty =
            DependencyProperty.Register("Weapon", typeof(DS1Weapon), typeof(WeaponControl), new PropertyMetadata(default(DS1Weapon)));

        

        public DS1Infusion Infusion
        {
            get { return (DS1Infusion)GetValue(InfusionProperty); }
            set { SetValue(InfusionProperty, value); }
        }

        public static readonly DependencyProperty InfusionProperty =
            DependencyProperty.Register("Infusion", typeof(DS1Infusion), typeof(WeaponControl), new PropertyMetadata(default(DS1Infusion)));

        public bool TwoHand
        {
            get { return (bool)GetValue(TwoHandProperty); }
            set { SetValue(TwoHandProperty, value); }
        }

        public static readonly DependencyProperty TwoHandProperty =
            DependencyProperty.Register("TwoHand", typeof(bool), typeof(WeaponControl), new PropertyMetadata(default(bool)));

        internal void Reset()
        {
            cmbWeapon.SelectedIndex = 0;
        }

        public int Upgrade
        {
            get { return (int)GetValue(UpgradeProperty); }
            set { SetValue(UpgradeProperty, value);}
        }

        public static readonly DependencyProperty UpgradeProperty =
            DependencyProperty.Register("Upgrade", typeof(int), typeof(WeaponControl), new PropertyMetadata(default(int)));

        private static void UpdateValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (WeaponControl)d;

            control.SetWeaponStatText();
        }

        private void cmbInfusion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DS1Infusion infusion = cmbInfusion.SelectedItem as DS1Infusion;

            if (infusion == null)
                return;

            HandleMinValue();
            nudUpgrade.Maximum = infusion.MaxUpgrade;
        }

        private void cmbWeapon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Weapon == null)
                return;

            TwoHand = false;
            TwoHandChecked.IsEnabled = true;
            nudUpgrade.Minimum = 0;
            nudUpgrade.Maximum = 15;
            HandleMinValue();
            ChangeWeapon();

            if (Weapon.WeaponType == DS1Weapon.Type.Bow || Weapon.WeaponType == DS1Weapon.Type.Crossbow)
            {
                TwoHand = true;
                TwoHandChecked.IsEnabled = false;
            }

        }

        public void Reload()
        {
            var weapon = Weapon;
            var infusion = Infusion;
            cmbWeapon.SelectedIndex = -1;
            cmbWeapon.SelectedIndex = cmbWeapon.Items.GetIndexByProperty<DS1Weapon>(x => x.ID == weapon.ID);
            cmbInfusion.SelectedIndex = -1;
            cmbInfusion.SelectedIndex = cmbInfusion.Items.GetIndexByProperty<DS1Infusion>(x => x.Value == infusion.Value);
        }

        private void ChangeWeapon()
        {
            if (Weapon == null)
                return;

            switch (Weapon.UpgradePath)
            {
                case DS1Weapon.Upgrade.None:
                    cmbInfusion.IsEnabled = false;
                    cmbInfusion.Items.Clear();
                    cmbInfusion.Items.Add(DS1Infusion.All[0]);
                    cmbInfusion.SelectedIndex = 0;
                    nudUpgrade.IsEnabled = false;
                    nudUpgrade.Maximum = 0;
                    cmbInfusion.SelectedIndex = 0;
                    break;
                case DS1Weapon.Upgrade.Unique:
                    cmbInfusion.IsEnabled = false;
                    cmbInfusion.Items.Clear();
                    cmbInfusion.Items.Add(DS1Infusion.All[0]);
                    cmbInfusion.SelectedIndex = 0;
                    nudUpgrade.Maximum = 5;
                    nudUpgrade.IsEnabled = true;
                    break;
                case DS1Weapon.Upgrade.Infusable:
                    cmbInfusion.Items.Clear();
                    foreach (DS1Infusion infusion in DS1Infusion.All)
                        cmbInfusion.Items.Add(infusion);
                    cmbInfusion.SelectedIndex = 0;
                    cmbInfusion.IsEnabled = true;
                    nudUpgrade.IsEnabled = true;
                    break;
                case DS1Weapon.Upgrade.InfusableRestricted:
                    cmbInfusion.Items.Clear();
                    foreach (DS1Infusion infusion in DS1Infusion.All)
                        if (!infusion.Restricted)
                            cmbInfusion.Items.Add(infusion);
                    cmbInfusion.SelectedIndex = 0;
                    cmbInfusion.IsEnabled = true;
                    nudUpgrade.IsEnabled = true;
                    break;
                case DS1Weapon.Upgrade.PyroFlame:
                    cmbInfusion.IsEnabled = false;
                    cmbInfusion.Items.Clear();
                    cmbInfusion.Items.Add(DS1Infusion.All[0]);
                    cmbInfusion.SelectedIndex = 0;
                    nudUpgrade.Maximum = 15;
                    nudUpgrade.IsEnabled = true;
                    break;
                case DS1Weapon.Upgrade.PyroFlameAscended:
                    cmbInfusion.IsEnabled = false;
                    cmbInfusion.Items.Clear();
                    cmbInfusion.Items.Add(DS1Infusion.All[0]);
                    cmbInfusion.SelectedIndex = 0;
                    nudUpgrade.Maximum = 5;
                    nudUpgrade.IsEnabled = true;
                    break;
            }
        }


        private bool FilterWeapons(object obj)
        {
            //if (cmbWeapon.IsDropDownOpen)
            //{
            //    if (obj is CategorizedItem item)
            //        return item.Name.Contains(cmbWeapon.Text, StringComparison.InvariantCultureIgnoreCase) || item.Category.Contains(cmbWeapon.Text, StringComparison.InvariantCultureIgnoreCase);

            //    return false;
            //}
            //else
            //    return true;
            return true;
        }

        private void cmbWeapon_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (cmbWeapon.IsDropDownOpen)
            //    WeaponListCollectionView.Refresh();

            if (string.IsNullOrWhiteSpace(cmbWeapon.Text))
                cmbWeapon.ItemsSource = WeaponListCollectionView;
            else
            {
                var newList = DS1Weapon.WeaponsList.Where(w => w.Name.ToLower().Contains(cmbWeapon.Text)).ToList();
                var newWeaponListCollectionView = new ListCollectionView(newList);
                newWeaponListCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));
                cmbWeapon.ItemsSource = newWeaponListCollectionView;
            }
        }

        private void cmbWeapon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            

        }

        private void cmbWeapon_DropDownOpened(object sender, EventArgs e)
        {
            //cmbWeapon.IsEditable = true;
            cmbWeapon.Text = "";
        }

        private void cmbWeapon_DropDownClosed(object sender, EventArgs e)
        {
            //cmbWeapon.IsEditable = false;
            if (Weapon != null)
                cmbWeapon.Text = Weapon.Name;
        }

        int StatState = 0;

        private void txtStats_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Damage.Useable)
                StatState++;

            SetWeaponStatText();
        }

        private void SetWeaponStatText()
        {
            if (Damage == null)
                return;

            if (Damage.Useable)
            {
                switch (StatState % 4)
                {
                    case 0:
                        txtStats.Text = $@"Total AR: {Damage.TotalAR}";
                        break;
                    case 1:
                        ARColors();
                        break;
                    case 2:
                        txtStats.Text = $@"Mag Adjust: {Damage.MagAdjust}";
                        break;
                    case 3:
                        DefColors();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                txtStats.Inlines.Clear();
                txtStats.Inlines.Add(new Run(@"Unusable") { Foreground = Brushes.Red });
            }
            
        }
        public void ARColors()
        {
            txtStats.Inlines.Clear();
            txtStats.Inlines.Add(new Run(@"Atk: ") { Foreground = Brushes.White });
            txtStats.Inlines.Add(new Run(Damage.PhysAR.ToString()) { Foreground = Brushes.RosyBrown });
            txtStats.Inlines.Add(new Run(@" \ ") { Foreground = Brushes.White });
            txtStats.Inlines.Add(new Run(Damage.MagAR.ToString()) { Foreground = Brushes.SkyBlue });
            txtStats.Inlines.Add(new Run(@" \ ") { Foreground = Brushes.White });
            txtStats.Inlines.Add(new Run(Damage.FireAR.ToString()) { Foreground = Brushes.OrangeRed });
            txtStats.Inlines.Add(new Run(@" \ ") { Foreground = Brushes.White });
            txtStats.Inlines.Add(new Run(Damage.LightAR.ToString()) { Foreground = Brushes.Yellow });
        }

        public void DefColors()
        {
            var weapon = DS1Weapon.GetWeapon(Weapon, Infusion, nudUpgrade.Value);
            txtStats.Inlines.Clear();
            txtStats.Inlines.Add(new Run(@"Def: ") { Foreground = Brushes.White });
            txtStats.Inlines.Add(new Run(weapon.PhysicalReduction.ToString()) { Foreground = Brushes.RosyBrown });
            txtStats.Inlines.Add(new Run(@"-") { Foreground = Brushes.White });
            txtStats.Inlines.Add(new Run(weapon.MagicReduction.ToString()) { Foreground = Brushes.SkyBlue });
            txtStats.Inlines.Add(new Run(@"-") { Foreground = Brushes.White });
            txtStats.Inlines.Add(new Run(weapon.FireReduction.ToString()) { Foreground = Brushes.OrangeRed });
            txtStats.Inlines.Add(new Run(@"-") { Foreground = Brushes.White });
            txtStats.Inlines.Add(new Run(weapon.FireReduction.ToString()) { Foreground = Brushes.Yellow });
            txtStats.Inlines.Add(new Run(@"-") { Foreground = Brushes.White });
            txtStats.Inlines.Add(new Run(weapon.Stability.ToString()) { Foreground = Brushes.White });
        }

        private void Max_Checked(object sender, RoutedEventArgs e)
        {
            HandleMinValue();
        }

        private void HandleMinValue() 
        {
            var weapon = DS1Weapon.GetWeapon(Weapon, Infusion, Upgrade);
            nudUpgrade.Minimum = Max.IsChecked.Value ? weapon.MaxUpgrade : 0;
        }

        private void WepControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (cmbWeapon.SelectedIndex == -1)
                cmbWeapon.SelectedIndex = 0;
        }


    }
}
