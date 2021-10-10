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
using SoulsFormats;

namespace FromSoft_Game_Build_Planner
{
    /// <summary>
    /// Interaction logic for DarkSouls1.xaml
    /// </summary>
    public partial class DarkSouls1 : Window
    {
        public DarkSouls1()
        {
            InitializeComponent();
            var path = @"F:\Dark Souls Mod Stuff\Remastest 1.4 Beta\DATA";
            var vanillaPath = @"F:\Dark Souls Mod Stuff\Vanilla PTDE\DATA";
            ReadParams(vanillaPath);
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
        List<DS1Weapon> WeaponDagger = new List<DS1Weapon>();
        List<DS1Weapon> WeaponWhip = new List<DS1Weapon>();
        List<DS1Weapon> WeaponSword = new List<DS1Weapon>();
        List<DS1Weapon> WeaponRapier = new List<DS1Weapon>();
        List<DS1Weapon> WeaponCurved = new List<DS1Weapon>();
        List<DS1Weapon> WeaponAxe = new List<DS1Weapon>();
        List<DS1Weapon> WeaponBlunt = new List<DS1Weapon>();
        List<DS1Weapon> WeaponSpear = new List<DS1Weapon>();
        List<DS1Weapon> WeaponHalberd = new List<DS1Weapon>();
        List<DS1Weapon> WeaponSpellTool = new List<DS1Weapon>();
        List<DS1Weapon> WeaponFist = new List<DS1Weapon>();
        List<DS1Weapon> WeaponBow = new List<DS1Weapon>();
        List<DS1Weapon> WeaponCrossbow = new List<DS1Weapon>();
        List<DS1Weapon> WeaponShield = new List<DS1Weapon>();
        List<DS1Weapon> WeaponArrow = new List<DS1Weapon>();
        List<DS1Weapon> WeaponBolt = new List<DS1Weapon>();
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
                    case "EQUIP_PARAM_PROTECTOR_ST": { SortArmors(param); break; }
                    case "EQUIP_PARAM_WEAPON_ST": { SortWeapons(param); break; }
                    case "EQUIP_PARAM_GOODS_ST": { SortItems(param); break; }
                    case "MAGIC_PARAM_ST": { SortSpells(param); break; }
                    case "EQUIP_PARAM_ACCESSORY_ST": { SortRings(param); break; }
                    case "CHARACTER_INIT_PARAM": { SortClasses(param); break; }
                    case "REINFORCE_PARAM_WEAPON_ST": { WeaponUpgrades = param.Rows.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => new DS1WeaponUpgrade(x)); break; }
                    case "REINFORCE_PARAM_PROTECTOR_ST": { ArmorUpgrades = param.Rows.GroupBy(x => x.ID).Select(x => x.First()).ToDictionary(x => x.ID, x => new DS1ArmorUpgrade(x)); ; break; }
                }
            }

            //Debug.Write("Name\tID\t");
            //foreach (var param in DebugParam.Rows[0].Cells)
            //{
            //    Debug.Write($"{param.Def}\t");
            //}
            WeaponDagger = WeaponDagger.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponWhip = WeaponWhip.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponSword = WeaponSword.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponRapier = WeaponRapier.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponCurved = WeaponCurved.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponAxe = WeaponAxe.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponBlunt = WeaponBlunt.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponSpear = WeaponSpear.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponHalberd = WeaponHalberd.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponSpellTool = WeaponSpellTool.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponFist = WeaponFist.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponBow = WeaponBow.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponCrossbow = WeaponCrossbow.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponShield = WeaponShield.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponArrow = WeaponArrow.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            WeaponBolt = WeaponBolt.GroupBy(x => x.Name).Select(x => x.First()).ToList();

            foreach (var weapon in WeaponDagger)
            {
                cbxWeapon.Items.Add(weapon);
            }

            Console.ReadLine();
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

                if ((byte)armor.Cells[74].Value == 0x1) { ArmorHead.Add(new DS1Armor(armor, DS1Armor.Slot.Head)); }
                else if ((byte)armor.Cells[75].Value == 0x1) { ArmorBody.Add(new DS1Armor(armor, DS1Armor.Slot.Body)); }
                else if ((byte)armor.Cells[76].Value == 0x1) { ArmorArms.Add(new DS1Armor(armor, DS1Armor.Slot.Arms)); }
                else if ((byte)armor.Cells[77].Value == 0x1) { ArmorLegs.Add(new DS1Armor(armor, DS1Armor.Slot.Legs)); }
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

                if (weapon.Name.Contains("+"))
                    continue;

                var dsWeapon = new DS1Weapon(weapon);

                //if (dsWeapon.UpgradePath == DS1Weapon.Upgrade.Infused)
                //    continue;

                switch (dsWeapon.WeaponType)
                {
                    case DS1Weapon.Type.Dagger:
                        WeaponDagger.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Sword:
                            WeaponSword.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Rapier:
                        WeaponRapier.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Curved:
                        WeaponCurved.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Axe:
                        WeaponAxe.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Blunt:
                        WeaponBlunt.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Spear:
                        WeaponSpear.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Halberd:
                        WeaponHalberd.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.SpellTool:
                    case DS1Weapon.Type.PyroFlame:
                    case DS1Weapon.Type.PyroFlameAscended:
                        WeaponSpellTool.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Fist:
                        WeaponFist.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Bow:
                        WeaponBow.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Crossbow:
                        WeaponCrossbow.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Shield:
                        WeaponShield.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Arrow:
                        WeaponArrow.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Bolt:
                        WeaponBolt.Add(new DS1Weapon(weapon));
                        break;
                    case DS1Weapon.Type.Whip:
                        WeaponWhip.Add(new DS1Weapon(weapon));
                        break;
                    default:
                        break;
                }
            }
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
        }

        private void cbxWeapon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var weapon = cbxWeapon.SelectedItem as DS1Weapon;

            switch (weapon.UpgradePath)
            {
                case DS1Weapon.Upgrade.None:
                    cbxInfusion.IsEnabled = false;
                    cbxInfusion.Items.Clear();
                    //nudUpgrade.IsEnabled = false;
                    //nudUpgrade.Maximum = 0;
                    break;
                case DS1Weapon.Upgrade.Unique:
                    cbxInfusion.IsEnabled = false;
                    cbxInfusion.Items.Clear();
                    //nudUpgrade.Maximum = 5;
                    //nudUpgrade.IsEnabled = true;
                    break;
                case DS1Weapon.Upgrade.Infusable:
                    cbxInfusion.Items.Clear();
                    foreach (DS1Infusion infusion in DS1Infusion.All)
                        cbxInfusion.Items.Add(infusion);
                    cbxInfusion.SelectedIndex = 0;
                    cbxInfusion.IsEnabled = true;
                    //nudUpgrade.IsEnabled = true;
                    break;
                case DS1Weapon.Upgrade.InfusableRestricted:
                    cbxInfusion.Items.Clear();
                    foreach (DS1Infusion infusion in DS1Infusion.All)
                        if (!infusion.Restricted)
                            cbxInfusion.Items.Add(infusion);
                    cbxInfusion.SelectedIndex = 0;
                    cbxInfusion.IsEnabled = true;
                    //nudUpgrade.IsEnabled = true;
                    break;
                case DS1Weapon.Upgrade.PyroFlame:
                    cbxInfusion.IsEnabled = false;
                    cbxInfusion.Items.Clear();
                    //nudUpgrade.Maximum = 15;
                    //nudUpgrade.IsEnabled = true;
                    break;
                case DS1Weapon.Upgrade.PyroFlameAscended:
                    cbxInfusion.IsEnabled = false;
                    cbxInfusion.Items.Clear();
                    //nudUpgrade.Maximum = 5;
                    //nudUpgrade.IsEnabled = true;
                    break;
            }
        }
    }
}
