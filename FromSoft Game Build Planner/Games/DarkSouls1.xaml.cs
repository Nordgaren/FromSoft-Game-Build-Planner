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
using SF_Compatible_DRB_Icon_Appender;
using SoulsFormats;

namespace FromSoft_Game_Build_Planner
{
    /// <summary>
    /// Interaction logic for DarkSouls1.xaml
    /// </summary>
    public partial class DarkSouls1 : Window
    {
        private string ExePath;

        private int SoulLevel;

        public DarkSouls1()
        {
            InitializeComponent();
#if DEBUG
            ExePath = @"F:\Dark Souls Mod Stuff\Remastest 1.4 Beta\DATA";
            //ExePath = @"F:\Dark Souls Mod Stuff\Vanilla PTDE\DATA";


#else
            var ofd = new OpenFileDialog();

            var result = ofd.ShowDialog();

            if (result.HasValue)
            {
                if (ofd.FileName.EndsWith("DARKSOULS.exe"))
                    ExePath = System.IO.Path.GetDirectoryName(ofd.FileName);
                else
                    MessageBox.Show("No Dark Souls Detected");

            }
            
#endif
            Initialize(ExePath);
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
        List<DS1Armor> ArmorHead;
        List<DS1Armor> ArmorBody;
        List<DS1Armor> ArmorArms;
        List<DS1Armor> ArmorLegs;
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
        List<CategorizedItem> WeaponsList;
        List<CategorizedItem> AmmoList;
        Dictionary<int, DS1Weapon> Weapons;
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
        List<DS1Class> Classes;
        //Weapon Upgrades
        Dictionary<int, DS1WeaponUpgrade> WeaponUpgrades;
        Dictionary<int, DS1ArmorUpgrade> ArmorUpgrades;
        Dictionary<int, DS1CalcCorrect> CalcCorrectGraph;
        Dictionary<string, TPF> Textures;

        private void Initialize(string exePath)
        {


            var gameParamFile = $@"{exePath}\param\GameParam\GameParam.parambnd";
            var paramDefFile = $@"{exePath}\paramdef\paramdef.paramdefbnd";
            var itemFMGFile = $@"{exePath}\msg\ENGLISH\item.msgbnd";
            var menuFMGFile = $@"{exePath}\msg\ENGLISH\menu.msgbnd";
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
            CalculatAR();
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
                    Debug.WriteLine($"{param.ParamType} Did not apply!");
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
                        WeaponUpgrades = param.Rows.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => new DS1WeaponUpgrade(x));
                        break;
                    case "REINFORCE_PARAM_PROTECTOR_ST":
                        ArmorUpgrades = param.Rows.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => new DS1ArmorUpgrade(x)); ;
                        break;
                    case "CACL_CORRECT_GRAPH_ST":
                        CalcCorrectGraph = param.Rows.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => new DS1CalcCorrect(x));
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
            ArmorHead = new List<DS1Armor>();
            ArmorBody = new List<DS1Armor>();
            ArmorArms = new List<DS1Armor>();
            ArmorLegs = new List<DS1Armor>();

            var armorNames = ItemFMGS[2].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            foreach (var item in MenuFMGS[31].Entries)
            {
                if (!armorNames.ContainsKey(item.ID))
                    armorNames.Add(item.ID, item.Text);
                else if (string.IsNullOrWhiteSpace(armorNames[item.ID]))
                    armorNames[item.ID] = item.Text;
            }

            foreach (var armor in equipProParam.Rows)
            {
                if (armorNames.ContainsKey(armor.ID))
                    armor.Name = armorNames[armor.ID];
                else
                    continue;

                if (string.IsNullOrWhiteSpace(armor.Name))
                    continue;

                if ((byte)armor.Cells[74].Value == 0x1)
                    ArmorHead.Add(new DS1Armor(armor, DS1Armor.Slot.Head));
                else if ((byte)armor.Cells[75].Value == 0x1)
                    ArmorBody.Add(new DS1Armor(armor, DS1Armor.Slot.Body));
                else if ((byte)armor.Cells[76].Value == 0x1)
                    ArmorArms.Add(new DS1Armor(armor, DS1Armor.Slot.Arms));
                else if ((byte)armor.Cells[77].Value == 0x1)
                    ArmorLegs.Add(new DS1Armor(armor, DS1Armor.Slot.Legs));
            }
        }

        private void SortWeapons(PARAM equipWepParam)
        {
            WeaponsList = new List<CategorizedItem>();
            AmmoList = new List<CategorizedItem>();
            Weapons = new Dictionary<int, DS1Weapon>();

            //Make weaponNames dictionary
            var weaponNames = ItemFMGS[1].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);
            foreach (var item in MenuFMGS[29].Entries)
            {
                if (!weaponNames.ContainsKey(item.ID))
                    weaponNames.Add(item.ID, item.Text);
                else if (string.IsNullOrWhiteSpace(weaponNames[item.ID]))
                    weaponNames[item.ID] = item.Text;
            }

            //Make weaponCategories dictionary
            var weaponCategories = ItemFMGS[11].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);
            foreach (var item in MenuFMGS[28].Entries)
            {
                if (!weaponCategories.ContainsKey(item.ID))
                    weaponCategories.Add(item.ID, item.Text);
                else if (string.IsNullOrWhiteSpace(weaponCategories[item.ID]))
                    weaponCategories[item.ID] = item.Text;
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
                if (dsWeapon.CategoryName == null)
                    continue;

                Weapons.Add(dsWeapon.ID, dsWeapon);

                if (dsWeapon.Name.Contains("+"))
                    continue;

                if (dsWeapon.UpgradePath == DS1Weapon.Upgrade.Infused)
                    continue;

                switch (dsWeapon.WeaponType)
                {
                    case DS1Weapon.Type.Arrow:
                    case DS1Weapon.Type.Bolt:
                        AmmoList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = dsWeapon.CategoryName });
                        break;
                    default:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = dsWeapon.CategoryName });
                        break;
                }

            }

            WeaponsList = WeaponsList.GroupBy(x => x.Name).Select(x => x.First()).OrderBy(x => x.Category).OrderBy(x => x.ID != 900000).ToList();
            ListCollectionView lcv = new ListCollectionView(WeaponsList);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("Category"));

            cmbWeapon.ItemsSource = lcv;
        }

        private void SortItems(PARAM goodsParam)
        {
            Items = new List<DS1Item>();
            Consumables = new List<DS1Item>();

            var itemNames = ItemFMGS[0].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            foreach (var item in MenuFMGS[25].Entries)
            {
                if (!itemNames.ContainsKey(item.ID))
                    itemNames.Add(item.ID, item.Text);
                else if (string.IsNullOrWhiteSpace(itemNames[item.ID]))
                    itemNames[item.ID] = item.Text;
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

            foreach (var item in MenuFMGS[25].Entries)
            {
                if (!spellNames.ContainsKey(item.ID))
                    spellNames.Add(item.ID, item.Text);
                else if (string.IsNullOrWhiteSpace(spellNames[item.ID]))
                    spellNames[item.ID] = item.Text;
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

            foreach (var item in MenuFMGS[27].Entries)
            {
                if (!ringNames.ContainsKey(item.ID))
                    ringNames.Add(item.ID, item.Text);
                else if (string.IsNullOrWhiteSpace(ringNames[item.ID]))
                    ringNames[item.ID] = item.Text;
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
            Classes = new List<DS1Class>();

            var classNames = MenuFMGS[6].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            foreach (var param in charInitParam.Rows)
            {
                switch (param.ID)
                {
                    case 3000:
                        param.Name = classNames[132020];
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3001:
                        param.Name = classNames[132021];
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3002:
                        param.Name = classNames[132022];
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3003:
                        param.Name = classNames[132023];
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3004:
                        param.Name = classNames[132024];
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3005:
                        param.Name = classNames[132025];
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3006:
                        param.Name = classNames[132026];
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3007:
                        param.Name = classNames[132027];
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3008:
                        param.Name = classNames[132028];
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3009:
                        param.Name = classNames[132029];
                        Classes.Add(new DS1Class(param));
                        break;
                    default:
                        break;
                }
            }

            foreach (var dsClass in Classes)
            {
                cmbClass.Items.Add(dsClass);
            }

            cmbClass.SelectedIndex = 0;
        }

        private void cmbWeapon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var weapon = Weapons[((CategorizedItem)cmbWeapon.SelectedItem).ID];

            switch (weapon.UpgradePath)
            {
                case DS1Weapon.Upgrade.None:
                    cmbInfusion.IsEnabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.IsEnabled = false;
                    nudUpgrade.MaxValue = 0;
                    break;
                case DS1Weapon.Upgrade.Unique:
                    cmbInfusion.IsEnabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.MaxValue = 5;
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
                    nudUpgrade.MaxValue = 15;
                    nudUpgrade.IsEnabled = true;
                    break;
                case DS1Weapon.Upgrade.PyroFlameAscended:
                    cmbInfusion.IsEnabled = false;
                    cmbInfusion.Items.Clear();
                    nudUpgrade.MaxValue = 5;
                    nudUpgrade.IsEnabled = true;
                    break;
            }

            CalculatAR();
        }

        bool NotLoading = false;

        private void CalculatAR()
        {
            if (NotLoading)
            {
                

                var infusion = cmbInfusion.SelectedItem as DS1Infusion;
                var infusionID = 000;
                if (infusion != null)
                    infusionID = infusion.Value;

                var weapon = Weapons[((CategorizedItem)cmbWeapon.SelectedItem).ID + infusionID];
                if (weapon.WeaponType == DS1Weapon.Type.PyroFlame || weapon.WeaponType == DS1Weapon.Type.PyroFlameAscended)
                    weapon = Weapons[weapon.ID + (nudUpgrade.Value * 100)];

                

                var multiplier = WeaponUpgrades[infusionID + nudUpgrade.Value];

                var physAttack = weapon.PhysicalAttack * multiplier.PhysicalMutliplier;
                var magicAttack = weapon.MagicAttack * multiplier.MagicMutliplier;
                var fireAttack = weapon.FireAttack * multiplier.FireMutliplier;
                var lightAttack = weapon.LightningAttack * multiplier.LightningMutliplier;

                txtPysAR.Text = ((int)physAttack).ToString();
                txtMagAR.Text = ((int)magicAttack).ToString();
                txtFireAR.Text = ((int)fireAttack).ToString();
                txtLightAR.Text = ((int)lightAttack).ToString();

                if (weapon.WeaponType == DS1Weapon.Type.PyroFlame || weapon.WeaponType == DS1Weapon.Type.PyroFlameAscended)
                {
                    var intScaling = weapon.IntScaling * multiplier.IntMultiplier;
                    var intDMG = GetStatDamage(nudInt.Value, weapon.IntRequired, intScaling, weapon.CorrectType, fireAttack);
                    var humanityScaling = 0f;
                    if (weapon.HumanityScaling)
                        humanityScaling = (float)GetHumanityDamage(intDMG, intDMG, magicAttack);
                    var scalingDMG = intDMG;
                    fireAttack += scalingDMG + humanityScaling;
                    var magAdjust = GetStatDamage(0, 0, intScaling, weapon.CorrectType, fireAttack);
                    txtFireAR.Text = ((int)fireAttack).ToString();
                }


                if (weapon.StrScaling > 0 || weapon.DexScaling > 0)
                {
                    var strScaling = weapon.StrScaling * multiplier.StrMultiplier;
                    var dexScaling = weapon.DexScaling * multiplier.DexMultiplier;
                    var strDMG = GetStatDamage(nudStr.Value, weapon.StrRequired, strScaling, weapon.CorrectType, physAttack);
                    var dexDMG = GetStatDamage(nudDex.Value, weapon.DexRequired, dexScaling, weapon.CorrectType, physAttack);
                    var humanityScaling = 0f;
                    if (weapon.HumanityScaling)
                        humanityScaling = (float)GetHumanityDamage(strDMG, dexDMG, magicAttack);
                    var scalingDMG = strDMG + dexDMG;
                    physAttack += scalingDMG;
                    txtPysAR.Text = ((int)physAttack).ToString();
                }

                if (weapon.IntScaling > 0 || weapon.FaiScaling > 0)
                {
                    var intScaling = weapon.IntScaling * multiplier.IntMultiplier;
                    var faiScaling = weapon.FaiScaling * multiplier.FaiMultiplier;
                    var intDMG = GetStatDamage(nudInt.Value, weapon.IntRequired, intScaling, weapon.CorrectType, magicAttack);
                    var faiDMG = GetStatDamage(nudFai.Value, weapon.FaiRequired, faiScaling, weapon.CorrectType, magicAttack);
                    var humanityScaling = 0f;
                    if (weapon.HumanityScaling)
                        humanityScaling = (float)GetHumanityDamage(intDMG, faiDMG, magicAttack);
                    var scalingDMG = intDMG + faiDMG;
                    magicAttack += scalingDMG + humanityScaling;
                    txtMagAR.Text = ((int)magicAttack).ToString();
                }

                txtTotalAR.Text = ((int)(physAttack + magicAttack + fireAttack + lightAttack)).ToString();

            }
        }

        private float GetStatDamage(int stat, int statRequired, float statScaling, int correctType, float typeAttack)
        {
            if (stat >= statRequired)
            {
                var dS1CalcCorrect = CalcCorrectGraph[correctType];

                if (stat <= dS1CalcCorrect.stgMaxVal0)
                {
                    return 0;
                }
                else if (stat > dS1CalcCorrect.stgMaxVal0 && stat <= dS1CalcCorrect.stgMaxVal1)
                {
                    return (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow1 / 100) - typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow0 / 100)) / (dS1CalcCorrect.stgMaxVal1 - dS1CalcCorrect.stgMaxVal0) * (stat - dS1CalcCorrect.stgMaxVal0) + (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow0 / 100));
                }
                else if (stat > dS1CalcCorrect.stgMaxVal1 && stat <= dS1CalcCorrect.stgMaxVal2)
                {
                    return (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow2 / 100) - typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow1 / 100)) / (dS1CalcCorrect.stgMaxVal2 - dS1CalcCorrect.stgMaxVal1) * (stat - dS1CalcCorrect.stgMaxVal1) + (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow1 / 100));
                }
                else if (stat > dS1CalcCorrect.stgMaxVal2 && stat <= dS1CalcCorrect.stgMaxVal3)
                {
                    return (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow3 / 100) - typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow2 / 100)) / (dS1CalcCorrect.stgMaxVal3 - dS1CalcCorrect.stgMaxVal2) * (stat - dS1CalcCorrect.stgMaxVal2) + (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow2 / 100));
                }
                else if (stat > dS1CalcCorrect.stgMaxVal3 && stat <= dS1CalcCorrect.stgMaxVal4)
                {
                    return (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow4 / 100) - typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow3 / 100)) / (dS1CalcCorrect.stgMaxVal4 - dS1CalcCorrect.stgMaxVal3) * (stat - dS1CalcCorrect.stgMaxVal3) + (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow3 / 100));
                }
                else if (stat > dS1CalcCorrect.stgMaxVal4)
                {
                    return (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow4 / 100) - typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow3 / 100)) / (dS1CalcCorrect.stgMaxVal4 - dS1CalcCorrect.stgMaxVal3) * (dS1CalcCorrect.stgMaxVal4 - dS1CalcCorrect.stgMaxVal3) + (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow3 / 100));
                }
            }

            return 0;
        }

        private float GetHumanityDamage(float statTypeDmg1, float statTypeDmg2, float typeAttack)
        {
            switch (nudHumanity.Value)
            {
                case 0:
                    return 0;
                case 1:
                    return (typeAttack * 0.05f) + (statTypeDmg1 * 0.05f) + (statTypeDmg2 * 0.05f);
                case 2:
                    return (typeAttack * 0.075f) + (statTypeDmg1 * 0.075f) + (statTypeDmg2 * 0.075f);
                case 3:
                    return (typeAttack * 0.1f) + (statTypeDmg1 * 0.1f) + (statTypeDmg2 * 0.1f);
                case 4:
                    return (typeAttack * 0.1157f) + (statTypeDmg1 * 0.1157f) + (statTypeDmg2 * 0.1157f);
                case 5:
                    return (typeAttack * 0.1314f) + (statTypeDmg1 * 0.1314f) + (statTypeDmg2 * 0.1314f);
                case 6:
                    return (typeAttack * 0.1471f) + (statTypeDmg1 * 0.1471f) + (statTypeDmg2 * 0.1471f);
                case 7:
                    return (typeAttack * 0.1628f) + (statTypeDmg1 * 0.1628f) + (statTypeDmg2 * 0.1628f);
                case 8:
                    return (typeAttack * 0.1758f) + (statTypeDmg1 * 0.1758f) + (statTypeDmg2 * 0.1758f);
                case 9:
                    return (typeAttack * 0.1942f) + (statTypeDmg1 * 0.1942f) + (statTypeDmg2 * 0.1942f);
                default:
                    return (typeAttack * 0.21f) + (statTypeDmg1 * 0.21f) + (statTypeDmg2 * 0.21f);
            }
        }

        private void cmbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbClass.SelectedIndex != -1)
            {
                var charClass = cmbClass.SelectedItem as DS1Class;
                nudVit.MinValue = charClass.BaseVit;
                nudAtt.MinValue = charClass.BaseAtt;
                nudEnd.MinValue = charClass.BaseEnd;
                nudStr.MinValue = charClass.BaseStr;
                nudDex.MinValue = charClass.BaseDex;
                nudRes.MinValue = charClass.BaseRes;
                nudInt.MinValue = charClass.BaseInt;
                nudFai.MinValue = charClass.BaseFai;
            }
        }

        private void RecalculateStats()
        {
            var vitality = nudVit.Value;
            var attunement = nudAtt.Value;
            var endurance = nudEnd.Value;
            var strength = nudStr.Value;
            var dexterity = nudDex.Value;
            var resistance = nudRes.Value;
            var intelligence = nudInt.Value;
            var faith = nudFai.Value;
            var sl = CalculateSL(vitality, attunement, endurance, strength, dexterity, resistance, intelligence, faith);

            txtSoulLevel.Text = sl.ToString();

        }

        private int CalculateSL(int vitality, int attunement, int endurance, int strength, int dexterity, int resistance, int intelligence, int faith)
        {
            var charClass = cmbClass.SelectedItem as DS1Class;

            if (charClass == null)
                return 0;

            int sl = charClass.SoulLevel;
            sl += vitality - charClass.BaseVit;
            sl += attunement - charClass.BaseAtt;
            sl += endurance - charClass.BaseEnd;
            sl += strength - charClass.BaseStr;
            sl += dexterity - charClass.BaseDex;
            sl += resistance - charClass.BaseRes;
            sl += intelligence - charClass.BaseInt;
            sl += faith - charClass.BaseFai;
            return sl;

        }

        private void nud_ValueChanged(object sender, EventArgs e)
        {
            RecalculateStats();

            CalculatAR();
        }

        private void cmbInfusion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DS1Infusion infusion = cmbInfusion.SelectedItem as DS1Infusion;

            if (infusion == null)
                return;

            nudUpgrade.MaxValue = infusion.MaxUpgrade;

            CalculatAR();
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            Initialize(ExePath);
        }
    }
}
