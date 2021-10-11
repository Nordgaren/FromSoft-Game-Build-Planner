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
            ReadParams(ExePath);
        }

        //PARAM EquipProParam;
        //PARAM EquipWepParam;
        //PARAM MagicParam;
        //PARAM GoodsParam;
        //PARAM AccessoryParam; //Rings
        //PARAM CharInitParam;
        //PARAM ReinforceParamProt;
        //PARAM ReinforceParamWeap;

        List<FMG> ItemFMGS = new List<FMG>();
        List<FMG> MenuFMGS = new List<FMG>();

        //Armor
        List<DS1Armor> ArmorHead = new List<DS1Armor>();
        List<DS1Armor> ArmorBody = new List<DS1Armor>();
        List<DS1Armor> ArmorArms = new List<DS1Armor>();
        List<DS1Armor> ArmorLegs = new List<DS1Armor>();
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
        List<CategorizedItem> WeaponsList = new List<CategorizedItem>();
        Dictionary<int, DS1Weapon> Weapons = new Dictionary<int, DS1Weapon>();
        //Spells
        List<DS1Spell> Sorceries = new List<DS1Spell>();
        List<DS1Spell> Miracles = new List<DS1Spell>();
        List<DS1Spell> Pyromancies = new List<DS1Spell>();
        //Items
        List<DS1Item> Items = new List<DS1Item>();
        List<DS1Item> Consumables = new List<DS1Item>();
        //Rings
        List<DS1Ring> Rings = new List<DS1Ring>();
        //Classes
        List<DS1Class> Classes = new List<DS1Class>();
        //Weapon Upgrades
        Dictionary<int, DS1WeaponUpgrade> WeaponUpgrades;
        Dictionary<int, DS1ArmorUpgrade> ArmorUpgrades;
        Dictionary<int, DS1CalcCorrect> CalcCorrectGraph;

        private void ReadParams(string exePath)
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
            foreach (var item in itemFMGBND.Files)
            {
                ItemFMGS.Add(FMG.Read(item.Bytes));
            }

            foreach (var item in menuFMGBND.Files)
            {
                MenuFMGS.Add(FMG.Read(item.Bytes));
            }

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

            CalculatAR();
            //Debug.Write("Name\tID\t");
            //foreach (var param in DebugParam.Rows[0].Cells)
            //{
            //    Debug.Write($"{param.Def}\t");
            //}
        }

        PARAM DebugParam;

        private void SortArmors(PARAM equipProParam)
        {
            var armorNames = ItemFMGS[2].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            foreach (var item in MenuFMGS[31].Entries)
            {
                if (!armorNames.ContainsKey(item.ID))
                    armorNames.Add(item.ID, item.Text);
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
            var weaponNames = ItemFMGS[1].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);


            foreach (var item in MenuFMGS[29].Entries)
            {
                if (!weaponNames.ContainsKey(item.ID))
                    weaponNames.Add(item.ID, item.Text);
            }

            

            //Debug.WriteLine($"Name\tMaterial\tReinforce");
            foreach (var weapon in equipWepParam.Rows)
            {
                if (weaponNames.ContainsKey(weapon.ID))
                    weapon.Name = weaponNames[weapon.ID];
                else
                    continue;

                //Debug.WriteLine($"{weapon.Name}\t{weapon.Cells[22].Value}\t{weapon.Cells[61].Value}");

                if (string.IsNullOrWhiteSpace(weapon.Name))
                    continue;

                var dsWeapon = new DS1Weapon(weapon);

                Weapons.Add(dsWeapon.ID, dsWeapon);

                if (dsWeapon.Name.Contains("+"))
                    continue;

                if (dsWeapon.UpgradePath == DS1Weapon.Upgrade.Infused)
                    continue;

                switch (dsWeapon.WeaponType)
                {
                    case DS1Weapon.Type.Dagger:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Dagger" });
                        break;
                    case DS1Weapon.Type.Sword:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Sword" });
                        break;
                    case DS1Weapon.Type.Rapier:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, Category = "Rapier" });
                        break;
                    case DS1Weapon.Type.Curved:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Curved Sword" });
                        break;
                    case DS1Weapon.Type.Axe:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Axe" });
                        break;
                    case DS1Weapon.Type.Blunt:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Blunt" });
                        break;
                    case DS1Weapon.Type.Spear:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Spear" });
                        break;
                    case DS1Weapon.Type.Halberd:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Halberd" });
                        break;
                    case DS1Weapon.Type.SpellTool:
                    case DS1Weapon.Type.PyroFlame:
                    case DS1Weapon.Type.PyroFlameAscended:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Spell Tool" });
                        break;
                    case DS1Weapon.Type.Fist:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Fist" });
                        break;
                    case DS1Weapon.Type.Bow:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Bow" });
                        break;
                    case DS1Weapon.Type.Crossbow:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Crossbow" });
                        break;
                    case DS1Weapon.Type.Shield:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Shield" });
                        break;
                    case DS1Weapon.Type.Arrow:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Arrow" });
                        break;
                    case DS1Weapon.Type.Bolt:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Bolt" });
                        break;
                    case DS1Weapon.Type.Whip:
                        WeaponsList.Add(new CategorizedItem() { Name = dsWeapon.Name, ID = dsWeapon.ID, Category = "Whip" });
                        break;
                    default:
                        break;
                }
            }

            WeaponsList = WeaponsList.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            ListCollectionView lcv = new ListCollectionView(WeaponsList);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("Category"));

            cmbWeapon.ItemsSource = lcv;
        }

        private void SortItems(PARAM goodsParam)
        {

            var itemNames = ItemFMGS[0].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            foreach (var item in MenuFMGS[25].Entries)
            {
                if (!itemNames.ContainsKey(item.ID))
                    itemNames.Add(item.ID, item.Text);
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
            var spellNames = ItemFMGS[4].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            foreach (var item in MenuFMGS[25].Entries)
            {
                if (!spellNames.ContainsKey(item.ID))
                    spellNames.Add(item.ID, item.Text);
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
            var ringNames = ItemFMGS[3].Entries.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => x.Text);

            foreach (var item in MenuFMGS[27].Entries)
            {
                if (!ringNames.ContainsKey(item.ID))
                    ringNames.Add(item.ID, item.Text);
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
            var classNames = MenuFMGS[6].Entries;

            foreach (var param in charInitParam.Rows)
            {
                switch (param.ID)
                {
                    case 3000:
                        param.Name = classNames.FirstOrDefault(x => x.ID == 132020).Text;
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3001:
                        param.Name = classNames.FirstOrDefault(x => x.ID == 132021).Text;
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3002:
                        param.Name = classNames.FirstOrDefault(x => x.ID == 132022).Text;
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3003:
                        param.Name = classNames.FirstOrDefault(x => x.ID == 132023).Text;
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3004:
                        param.Name = classNames.FirstOrDefault(x => x.ID == 132024).Text;
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3005:
                        param.Name = classNames.FirstOrDefault(x => x.ID == 132025).Text;
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3006:
                        param.Name = classNames.FirstOrDefault(x => x.ID == 132026).Text;
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3007:
                        param.Name = classNames.FirstOrDefault(x => x.ID == 132027).Text;
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3008:
                        param.Name = classNames.FirstOrDefault(x => x.ID == 132028).Text;
                        Classes.Add(new DS1Class(param));
                        break;
                    case 3009:
                        param.Name = classNames.FirstOrDefault(x => x.ID == 132029).Text;
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

        private void CalculatAR()
        {
            var infusion = cmbInfusion.SelectedItem as DS1Infusion;

            var infusionID = 000;

            if (infusion != null)
                infusionID = infusion.Value;

            var ds1Weapon = Weapons[((CategorizedItem)cmbWeapon.SelectedItem).ID + infusionID];

            if (ds1Weapon.WeaponType == DS1Weapon.Type.PyroFlame || ds1Weapon.WeaponType == DS1Weapon.Type.PyroFlameAscended)
                ds1Weapon = Weapons[ds1Weapon.ID + (nudUpgrade.Value * 100)];

            var reqList = new List<Tuple<int, int>>();

            var statsRequired = (nudStr.Value >= ds1Weapon.StrRequired) && (nudDex.Value >= ds1Weapon.DexRequired) && 
                (nudInt.Value >= ds1Weapon.IntRequired) && (nudFai.Value >= ds1Weapon.FaiRequired);


            if (statsRequired)
            {
                var Stat = 0;
                var StatRequired = 0;
                var Multiplier = WeaponUpgrades[infusionID + nudUpgrade.Value];
                var weaponAttack = ds1Weapon.PhysicalAttack * Multiplier.PhysicalMutliplier;
                var strScaling = ds1Weapon.StrScaling * Multiplier.StrMultiplier;
                var dexScaling = ds1Weapon.DexScaling * Multiplier.DexMultiplier;

                var ARBoost = 0f;

                /*
                 * ARBoost=(BaseAttack*(StatCorrect/100)*(stgMaxValGrow/100)-BaseAttack*(StatCorrect/100)*(stgMaxValGrowBelow/100))/(stgMaxVal-stgMaxValBelow)+(BaseAttack*(StatCorrect/100)*(stgMaxValGrowBelow/100))
                 */

                var ScalingFactor = weaponAttack * (ds1Weapon.StrScaling / ds1Weapon.DexScaling);

                var StatTypeDmg = 0f;

                if (Stat >= StatRequired)
                {
                    if (Stat > CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal0 && Stat <= CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal1)
                    {
                        StatTypeDmg = (ScalingFactor * (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow1 / 100) - ScalingFactor * (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow0 / 100)) / (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal1 - CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal0) * (Stat - CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal0) + (ScalingFactor * CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow0 / 100);
                    }
                    if (Stat > CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal1 && Stat <= CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal2)
                    {
                        StatTypeDmg = (ScalingFactor * (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow2 / 100) - ScalingFactor * (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow1 / 100)) / (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal2 - CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal1) * (Stat - CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal1) + (ScalingFactor * CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow1 / 100);
                    }
                    if (Stat > CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal2 && Stat <= CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal3)
                    {
                        StatTypeDmg = (ScalingFactor * (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow3 / 100) - ScalingFactor * (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow2 / 100)) / (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal3 - CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal2) * (Stat - CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal2) + (ScalingFactor * CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow2 / 100);
                    }
                    if (Stat > CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal3 && Stat <= CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal4)
                    {
                        StatTypeDmg = (ScalingFactor * (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow4 / 100) - ScalingFactor * (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow3 / 100)) / (CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal4 - CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal3) * (Stat - CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxVal3) + (ScalingFactor * CalcCorrectGraph[ds1Weapon.CorrectType].stgMaxValGrow3 / 100);
                    }

                    txtAR.Text = "Usable";
                }
                
            }
            else
                txtAR.Text = "Unusable";
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

            if (CalcCorrectGraph != null)
                CalculatAR();

        }

        private void cmbInfusion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DS1Infusion infusion = cmbInfusion.SelectedItem as DS1Infusion;

            if (infusion == null)
                return;

            nudUpgrade.MaxValue = infusion.MaxUpgrade;

            CalculatAR();
            //Checks if maxUpgrade is checked and sets the value to max value
        }


    }
}
