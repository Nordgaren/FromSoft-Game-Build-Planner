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
    public partial class DarkSouls1 : Window
    {

        private int SoulLevel;

        bool DSR = false;

        string ExePath;

        //public DS1Character Chr = new DS1Character();

        public DarkSouls1(string exePath, bool dsr)
        {

            DSR = dsr;
            ExePath = exePath;
            if (DSR)
                DS1Planner.Title = "Dark Souls: Remastered";

            //DataContext = Chr;
            
            Initialize(ExePath);
            HeadList = new(DS1Armor.ArmorHead);
            BodyList = new(DS1Armor.ArmorBody);
            ArmsList = new(DS1Armor.ArmorArms);
            LegsList = new(DS1Armor.ArmorLegs);
            InitializeComponent();

            //wcRH1.SetChr(Chr);
            //wcRH2.SetChr(Chr);
            //wcLH1.SetChr(Chr);
            //wcLH2.SetChr(Chr);
            //wcRH1.CalculatAR();
            //wcRH2.CalculatAR();
            //wcLH1.CalculatAR();
            //wcLH2.CalculatAR();
        }

        public ObservableCollection<DS1Armor> HeadList { get; set; }
        public ObservableCollection<DS1Armor> BodyList { get; set; }
        public ObservableCollection<DS1Armor> ArmsList { get; set; }
        public ObservableCollection<DS1Armor> LegsList { get; set; }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //PARAM EquipProParam;
        //PARAM EquipWepParam;
        //PARAM MagicParam;
        //PARAM GoodsParam;
        //PARAM AccessoryParam; //Rings
        //PARAM CharInitParam;
        //PARAM ReinforceParamProt;
        //PARAM ReinforceParamWeap;

        List<FMG> ItemFMGS;
        List<FMG> MenuFMGS;

        //Armor

        //Weapons
        //List<DS1Weapon> WeaponDagger = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponWhip = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponSword = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponRapier = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponCurved = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponAxe = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponBlunt = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponSpear = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponHalberd = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponSpellTool = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponFist = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponBow = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponCrossbow = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponShield = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponArrow = new List<DS1Weapon>();
        //List<DS1Weapon> WeaponBolt = new List<DS1Weapon>();

        //Spells
        List<DS1Spell> Sorceries;
        List<DS1Spell> Miracles;
        List<DS1Spell> Pyromancies;
        //Items
        List<DS1Item> Items;
        List<DS1Item> Consumables;
        //Rings
        List<DS1Ring> Rings;
        //Classes
        //Weapon Upgrades
        Dictionary<int, DS1ArmorUpgrade> ArmorUpgrades;
        Dictionary<string, TPF> Textures;

        private void Initialize(string exePath)
        {
            var dcx = DSR ? ".dcx" : "";
            var gameParamFile = $@"{exePath}\param\GameParam\GameParam.parambnd{dcx}";
            var paramDefFile = $@"{exePath}\paramdef\paramdef.paramdefbnd{dcx}";
            var itemFMGFile = $@"{exePath}\msg\ENGLISH\item.msgbnd{dcx}";
            var menuFMGFile = $@"{exePath}\msg\ENGLISH\menu.msgbnd{dcx}";
            var paramBND = BND3.Read(gameParamFile);
            var paramDefBND = BND3.Read(paramDefFile);
            var itemFMGBND = BND3.Read(itemFMGFile);
            var menuFMGBND = BND3.Read(menuFMGFile);
            var paramDefs = new List<PARAMDEF>();
            var paramList = new List<PARAM>();

            //Read Data
            ReadFMGs(itemFMGBND, menuFMGBND);
            ReadParams(paramBND, paramDefBND, paramDefs, paramList);


            //var drbFile = $@"{ExePath}\menu\menu.drb";
            //var tpfFile = $@"{ExePath}\menu\menu.tpf";
            //var tpfTexFile0 = $@"{ExePath}\menu\menu_0.tpf";
            //var tpfTexFile1 = $@"{ExePath}\menu\menu_1.tpf";
            //var tpfTexFile2 = $@"{ExePath}\menu\menu_2.tpf";
            //var tpfTexFile3 = $@"{ExePath}\menu\menu_3.tpf";
            //var tpfTexFile4 = $@"{ExePath}\menu\menu_4.tpf";
            //var tpfTexFile5 = $@"{ExePath}\menu\menu_5.tpf";
            //var drb = DRBRaw.Read(File.ReadAllBytes(drbFile));
            //var tpf = TPF.Read(tpfFile);
            //List<TPF> tpfs = new List<TPF>()
            //{
            //TPF.Read(tpfTexFile0),
            //TPF.Read(tpfTexFile1),
            //TPF.Read(tpfTexFile2),
            //TPF.Read(tpfTexFile3),
            //TPF.Read(tpfTexFile4),
            //TPF.Read(tpfTexFile5)
            //};
            ////tpfs[0].Textures[0].Bytes
            //DRBRaw.DLGEntry iconOld;

            //foreach (var dlgo in drb.dlg.Entries)
            //{
            //    if (dlgo.Name == "Icon")
            //    {
            //        iconOld = dlgo;
            //        break;
            //    }
            //}

            //ReadDRB(tpf, drb);
            //Debug.Write("Name\tID\t");
            //foreach (var param in DebugParam.Rows[0].Cells)
            //{
            //    Debug.Write($"{param.Def}\t");
            //}
            NotLoading = true;
            
        }

        private void ReadParams(IBinder paramBND, IBinder paramDefBND, List<PARAMDEF> paramDefs, List<PARAM> paramList)
        {
            foreach (var item in paramBND.Files)
            {
                paramList.Add(PARAM.Read(item.Bytes));
            }

            foreach (var item in paramDefBND.Files)
            {
                paramDefs.Add(PARAMDEF.Read(item.Bytes));
            }

            foreach (var param in paramList)
            {
                var result = param.ApplyParamdefCarefully(paramDefs);
                if (!result)
                    foreach (var paramDef in paramDefs)
                        if (paramDef.ParamType == param.ParamType && (param.DetectedSize == -1 || param.DetectedSize == paramDef.GetRowSize()))
                            param.ApplyParamdef(paramDef);
            }

            foreach (var param in paramList)
            {
                switch (param.ParamType)
                {
                    case "EQUIP_PARAM_PROTECTOR_ST":
                        SortArmors(param);
                        break;
                    case "EQUIP_PARAM_WEAPON_ST":
                        SortWeapons(param);
                        break;
                    case "EQUIP_PARAM_GOODS_ST":
                        SortItems(param);
                        break;
                    case "MAGIC_PARAM_ST":
                        SortSpells(param);
                        break;
                    case "EQUIP_PARAM_ACCESSORY_ST":
                        SortRings(param);
                        break;
                    case "CHARACTER_INIT_PARAM":
                        SortClasses(param);
                        break;
                    case "REINFORCE_PARAM_WEAPON_ST":
                        DS1WeaponUpgrade.WeaponUpgrades = param.Rows.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => new DS1WeaponUpgrade(x));
                        break;
                    case "REINFORCE_PARAM_PROTECTOR_ST":
                        ArmorUpgrades = param.Rows.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => new DS1ArmorUpgrade(x)); ;
                        break;
                    case "CACL_CORRECT_GRAPH_ST":
                        DS1CalcCorrect.CalcCorrectGraph = param.Rows.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => new DS1CalcCorrect(x));
                        break;
                }
            }
        }

        private void ReadFMGs(IBinder itemFMGBND, IBinder menuFMGBND)
        {
            ItemFMGS = new List<FMG>();
            MenuFMGS = new List<FMG>();
            foreach (var item in itemFMGBND.Files)
            {
                ItemFMGS.Add(FMG.Read(item.Bytes));
            }

            foreach (var item in menuFMGBND.Files)
            {
                MenuFMGS.Add(FMG.Read(item.Bytes));
            }
        }

        DRBRaw DRB;
        List<SpriteShape> Shapes;
        bool Remastered = false;


        public void ReadDRB(TPF menuTPF, DRBRaw menuDRB)
        {
            var textures = new List<string>();
            foreach (var entry in menuTPF.Textures)
                textures.Add(entry.Name);

            DRB = menuDRB;
            Shapes = new List<SpriteShape>();
            foreach (var dlg in menuDRB.dlg.Entries)
            {
                if (dlg.Name == "Icon")
                {
                    foreach (DRBRaw.DLGOEntry dlgo in dlg.DLGOEntries)
                        Shapes.Add(new SpriteShape(dlgo, DRB, textures, Remastered));
                }
            }
        }

        PARAM DebugParam;

        private void SortArmors(PARAM equipProParam)
        {
            var armorNames = ItemFMGS[2].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            if (!DSR)
            {
                foreach (var item in MenuFMGS[31].Entries)
                {
                    if (!armorNames.ContainsKey(item.ID))
                        armorNames.Add(item.ID, item.Text);
                    else if (string.IsNullOrWhiteSpace(armorNames[item.ID]))
                        armorNames[item.ID] = item.Text;
                }
            }

            foreach (var armor in equipProParam.Rows)
            {
                if (armorNames.ContainsKey(armor.ID))
                    armor.Name = armorNames[armor.ID];
                else
                    continue;

                if (string.IsNullOrWhiteSpace(armor.Name))
                    continue;

                new DS1Armor(armor);
            }
        }

        private void SortWeapons(PARAM equipWepParam)
        {
            //Make weaponNames dictionary
            var weaponNames = ItemFMGS[1].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            if (!DSR)
            {
                foreach (var item in MenuFMGS[29].Entries)
                {
                    if (!weaponNames.ContainsKey(item.ID))
                        weaponNames.Add(item.ID, item.Text);
                    else if (string.IsNullOrWhiteSpace(weaponNames[item.ID]))
                        weaponNames[item.ID] = item.Text;
                }
            }

            //Make weaponCategories dictionary
            var weaponCategories = ItemFMGS[11].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);
            if (!DSR)
            {
                foreach (var item in MenuFMGS[28].Entries)
                {
                    if (!weaponCategories.ContainsKey(item.ID))
                        weaponCategories.Add(item.ID, item.Text);
                    else if (string.IsNullOrWhiteSpace(weaponCategories[item.ID]))
                        weaponCategories[item.ID] = item.Text;
                }
            }

            //Add Weapons to WeaponList
            foreach (var weapon in equipWepParam.Rows)
            {
                if (weaponNames.ContainsKey(weapon.ID))
                    weapon.Name = weaponNames[weapon.ID];
                else
                    continue;

                if (string.IsNullOrWhiteSpace(weapon.Name))
                    continue;

                var dsWeapon = new DS1Weapon(weapon, weaponCategories.ContainsKey(weapon.ID) ? weaponCategories[weapon.ID] : "Misc");

                //Gets rid of base weapon entries like "Bolt" and "Arrow" that don't actually exist in game.
            }
        }

        private void SortItems(PARAM goodsParam)
        {
            Items = new List<DS1Item>();
            Consumables = new List<DS1Item>();

            var itemNames = ItemFMGS[0].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            if (!DSR)
            {
                foreach (var item in MenuFMGS[25].Entries)
                {
                    if (!itemNames.ContainsKey(item.ID))
                        itemNames.Add(item.ID, item.Text);
                    else if (string.IsNullOrWhiteSpace(itemNames[item.ID]))
                        itemNames[item.ID] = item.Text;
                }

            }

            foreach (var item in goodsParam.Rows)
            {
                if (itemNames.ContainsKey(item.ID))
                    item.Name = itemNames[item.ID];
                else
                    continue;

                if (string.IsNullOrWhiteSpace(item.Name))
                    continue;

                if ((byte)item.Cells[49].Value == 1)
                {
                    var dsItem = new DS1Item(item);

                    if (dsItem.Consumable)
                        Consumables.Add(dsItem);
                    else
                        Items.Add(dsItem);
                }
            }
        }

        private void SortSpells(PARAM magicParam)
        {
            Sorceries = new List<DS1Spell>();
            Miracles = new List<DS1Spell>();
            Pyromancies = new List<DS1Spell>();

            var spellNames = ItemFMGS[4].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            if (!DSR)
            {
                foreach (var item in MenuFMGS[25].Entries)
                {
                    if (!spellNames.ContainsKey(item.ID))
                        spellNames.Add(item.ID, item.Text);
                    else if (string.IsNullOrWhiteSpace(spellNames[item.ID]))
                        spellNames[item.ID] = item.Text;
                }
            }

            foreach (var spell in magicParam.Rows)
            {
                if (spellNames.ContainsKey(spell.ID))
                    spell.Name = spellNames[spell.ID];
                else
                    continue;

                if (string.IsNullOrWhiteSpace(spell.Name))
                    continue;

                var dsSpell = new DS1Spell(spell, (byte)spell.Cells[19].Value);

                switch (dsSpell.SpellType)
                {
                    case DS1Spell.Type.Sorcery:
                        Sorceries.Add(dsSpell);
                        break;
                    case DS1Spell.Type.Miracle:
                        Miracles.Add(dsSpell);
                        break;
                    case DS1Spell.Type.Pyromancy:
                        Pyromancies.Add(dsSpell);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SortRings(PARAM accessoryParam)
        {
            Rings = new List<DS1Ring>();

            var ringNames = ItemFMGS[3].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            if (!DSR)
            {
                foreach (var item in MenuFMGS[27].Entries)
                {
                    if (!ringNames.ContainsKey(item.ID))
                        ringNames.Add(item.ID, item.Text);
                    else if (string.IsNullOrWhiteSpace(ringNames[item.ID]))
                        ringNames[item.ID] = item.Text;
                }
            }

            foreach (var ring in accessoryParam.Rows)
            {
                if (ringNames.ContainsKey(ring.ID))
                    ring.Name = ringNames[ring.ID];
                else
                    continue;

                if (string.IsNullOrWhiteSpace(ring.Name))
                    continue;

                Rings.Add(new DS1Ring(ring));
            }
        }

        private void SortClasses(PARAM charInitParam)
        {
            var classNames = MenuFMGS[6].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            foreach (var param in charInitParam.Rows)
            {
                switch (param.ID)
                {
                    case 3000:
                        param.Name = classNames[132020];
                        DS1Class.Classes.Add(new DS1Class(param));
                        break;
                    case 3001:
                        param.Name = classNames[132021];
                        DS1Class.Classes.Add(new DS1Class(param));
                        break;
                    case 3002:
                        param.Name = classNames[132022];
                        DS1Class.Classes.Add(new DS1Class(param));
                        break;
                    case 3003:
                        param.Name = classNames[132023];
                        DS1Class.Classes.Add(new DS1Class(param));
                        break;
                    case 3004:
                        param.Name = classNames[132024];
                        DS1Class.Classes.Add(new DS1Class(param));
                        break;
                    case 3005:
                        param.Name = classNames[132025];
                        DS1Class.Classes.Add(new DS1Class(param));
                        break;
                    case 3006:
                        param.Name = classNames[132026];
                        DS1Class.Classes.Add(new DS1Class(param));
                        break;
                    case 3007:
                        param.Name = classNames[132027];
                        DS1Class.Classes.Add(new DS1Class(param));
                        break;
                    case 3008:
                        param.Name = classNames[132028];
                        DS1Class.Classes.Add(new DS1Class(param));
                        break;
                    case 3009:
                        param.Name = classNames[132029];
                        DS1Class.Classes.Add(new DS1Class(param));
                        break;
                    default:
                        break;
                }
            }

        }

        public static bool NotLoading = false;

        //private void RecalculateStats()
        //{
        //    var vitality = nudVit.Value;
        //    var attunement = nudAtt.Value;
        //    var endurance = nudEnd.Value;
        //    var strength = nudStr.Value;
        //    var dexterity = nudDex.Value;
        //    var resistance = nudRes.Value;
        //    var intelligence = nudInt.Value;
        //    var faith = nudFai.Value;
        //    var sl = CalculateSL(vitality, attunement, endurance, strength, dexterity, resistance, intelligence, faith);

        //    //txtSoulLevel.Text = sl.ToString();

        //}

        //private int CalculateSL(int vitality, int attunement, int endurance, int strength, int dexterity, int resistance, int intelligence, int faith)
        //{
        //    Chr.Class = cmbClass.SelectedItem as DS1Class;

        //    if (Chr.Class == null)
        //        return 0;

        //    int sl = Chr.Class.SoulLevel;
        //    sl += vitality - Chr.Class.BaseVit;
        //    sl += attunement - Chr.Class.BaseAtt;
        //    sl += endurance - Chr.Class.BaseEnd;
        //    sl += strength - Chr.Class.BaseStr;
        //    sl += dexterity - Chr.Class.BaseDex;
        //    sl += resistance - Chr.Class.BaseRes;
        //    sl += intelligence - Chr.Class.BaseInt;
        //    sl += faith - Chr.Class.BaseFai;
        //    return sl;

        //}

        private void nud_ValueChanged(object sender, RoutedEventArgs e)
        {
            //RecalculateStats();

            //wcRH1.CalculatAR();
            //wcRH2.CalculatAR();
            //wcLH1.CalculatAR();
            //wcLH2.CalculatAR();
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            Initialize(ExePath);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var exePath = App.BrowseFiles();
            UserSettings.LocalUserSettings.LastExePath = exePath;
            var result = App.StartPlanner(exePath);
            if (result)
                Close();
        }
    }
}
