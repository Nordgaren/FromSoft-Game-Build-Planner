using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    public class DS1Weapon
    {
        public enum Type
        {
            Dagger,
            Sword,
            Rapier,
            Curved,
            Axe,
            Blunt,
            Spear,
            Halberd,
            SpellTool,
            Fist,
            Bow,
            Crossbow,
            Shield,
            Arrow,
            Bolt,
            Whip,
            PyroFlame,
            PyroFlameAscended
        }

        public enum Upgrade
        {
            None,
            Unique,
            Infusable,
            InfusableRestricted,
            PyroFlame,
            PyroFlameAscended,
            Infused
        }

        public string Name { get; set; }
        public int ID { get; set; }
        public ushort IconID { get; set; }

        public byte CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int BehaviorID { get; set; }

        public float Weight { get; set; }
        public float SpeedClass { get; set; }

        public int AttackSpecialEffect1 { get; set; }
        public int AttackSpecialEffect2 { get; set; }
        public int AttackSpecialEffect3 { get; set; }
        public int EquipSpecialEffect1 { get; set; }
        public int EquipSpecialEffect2 { get; set; }
        public int EquipSpecialEffect3 { get; set; }

        public int UpgradeMaterial { get; set; }
        public short ReinforcementType { get; set; }

        public byte MaxUpgrade { get; set; }

        public float StrScaling { get; set; }
        public float DexScaling { get; set; }
        public float IntScaling { get; set; }
        public float FaiScaling { get; set; }

        public float PhysicalReduction { get; set; }
        public float MagicReduction { get; set; }
        public float FireReduction { get; set; }
        public float LightningReduction { get; set; }

        public sbyte SlashReduction { get; set; }
        public sbyte BlowReduction { get; set; }
        public sbyte ThrustReduction { get; set; }

        public sbyte PoisonResist { get; set; }
        public sbyte ToxicResist { get; set; }
        public sbyte BleedResist { get; set; }
        public sbyte CurseResist { get; set; }

        public ushort PhysicalAttack { get; set; }
        public ushort MagicAttack { get; set; }
        public ushort FireAttack { get; set; }
        public ushort LightningAttack { get; set; }

        public short Stability { get; set; }
        public short CritRate { get; set; }
        public byte CorrectType { get; set; }

        public byte StrRequired { get; set; }
        public byte DexRequired { get; set; }
        public byte IntRequired { get; set; }
        public byte FaiRequired { get; set; }

        public bool HumanityScaling { get; set; }

        public byte BaseChangeCategory { get; set; }

        public Upgrade UpgradePath { get; set; }

        public Type WeaponType { get; set; }
        public bool ShowID { get; private set; } = false;

        public DS1Weapon(PARAM.Row weaponParam, string categoryName)
        {
            Name = weaponParam.Name;

            if (Name.Contains("Rapier"))
                Console.WriteLine();
            ID = weaponParam.ID;
            IconID = (ushort)weaponParam.Cells[47].Value;

            CategoryID = (byte)weaponParam.Cells[68].Value;
            CategoryName = categoryName == "" ? "Misc" : categoryName;
            BehaviorID = (int)weaponParam.Cells[0].Value;

            Weight = (float)weaponParam.Cells[3].Value;
            SpeedClass = (float)weaponParam.Cells[4].Value;

            AttackSpecialEffect1 = (int)weaponParam.Cells[16].Value;
            AttackSpecialEffect2 = (int)weaponParam.Cells[17].Value;
            AttackSpecialEffect3 = (int)weaponParam.Cells[18].Value;
            EquipSpecialEffect1 = (int)weaponParam.Cells[19].Value;
            EquipSpecialEffect2 = (int)weaponParam.Cells[20].Value;
            EquipSpecialEffect3 = (int)weaponParam.Cells[21].Value;

            UpgradeMaterial = (int)weaponParam.Cells[22].Value;
            ReinforcementType = (short)weaponParam.Cells[61].Value;

            StrScaling = (float)weaponParam.Cells[8].Value;
            DexScaling = (float)weaponParam.Cells[9].Value;
            IntScaling = (float)weaponParam.Cells[10].Value;
            FaiScaling = (float)weaponParam.Cells[11].Value;

            PhysicalReduction = (float)weaponParam.Cells[12].Value;
            MagicReduction = (float)weaponParam.Cells[13].Value;
            FireReduction = (float)weaponParam.Cells[14].Value;
            LightningReduction = (float)weaponParam.Cells[15].Value;
            SlashReduction = (sbyte)weaponParam.Cells[90].Value;
            BlowReduction = (sbyte)weaponParam.Cells[91].Value;
            ThrustReduction = (sbyte)weaponParam.Cells[92].Value;

            PoisonResist = (sbyte)weaponParam.Cells[93].Value;
            ToxicResist = (sbyte)weaponParam.Cells[94].Value;
            BleedResist = (sbyte)weaponParam.Cells[95].Value;
            CurseResist = (sbyte)weaponParam.Cells[96].Value;

            PhysicalAttack = (ushort)weaponParam.Cells[52].Value;
            MagicAttack = (ushort)weaponParam.Cells[53].Value;
            FireAttack = (ushort)weaponParam.Cells[54].Value;
            LightningAttack = (ushort)weaponParam.Cells[55].Value;

            Stability = (short)weaponParam.Cells[60].Value;
            CritRate = (short)weaponParam.Cells[64].Value;
            CorrectType = (byte)weaponParam.Cells[74].Value;

            StrRequired = (byte)weaponParam.Cells[79].Value;
            DexRequired = (byte)weaponParam.Cells[80].Value;
            IntRequired = (byte)weaponParam.Cells[81].Value;
            FaiRequired = (byte)weaponParam.Cells[82].Value;

            HumanityScaling = (byte)weaponParam.Cells[114].Value > 0;

            BaseChangeCategory = (byte)weaponParam.Cells[122].Value;

            SetWeaponType();

            SetMaxUpgrade(weaponParam);

            SetUpgradePath();
        }

        private void SetUpgradePath()
        {
            if (WeaponType == Type.PyroFlame)
            {
                UpgradePath = Upgrade.PyroFlame;
                return;
            }

            if (WeaponType == Type.PyroFlameAscended)
            {
                UpgradePath = Upgrade.PyroFlameAscended;
                return;
            }

            if (WeaponType == Type.SpellTool)
                UpgradePath = Upgrade.None;

            if (WeaponType == Type.Shield)
            {
                switch (ReinforcementType)
                {
                    case 0:
                    case 5000:
                    case 6000:
                        UpgradePath = Upgrade.InfusableRestricted;
                        break;
                    case 1000:
                    case 1200:
                    case 1400:
                    case 1500:
                        if (UpgradeMaterial == -1 || UpgradeMaterial == 0)
                            UpgradePath = Upgrade.None;
                        else
                            UpgradePath = Upgrade.Unique;
                        break;
                    default:
                        UpgradePath = Upgrade.Infused;
                        break;
                }
                return;
            }

            switch (ReinforcementType)
            {
                case 0:
                    if (UpgradeMaterial == -1)
                        UpgradePath = Upgrade.None;
                    else
                        UpgradePath = Upgrade.Infusable;
                    break;
                case 1000:
                case 1100:
                case 1200:
                case 1500:
                    if (UpgradeMaterial == -1 || UpgradeMaterial == 0)
                        UpgradePath = Upgrade.None;
                    else
                        UpgradePath = Upgrade.Unique;
                    break;
                case 2000:
                    UpgradePath = Upgrade.Infusable;
                    break;
                case 8000:
                    UpgradePath = Upgrade.InfusableRestricted;
                    break;
                default:
                    UpgradePath = Upgrade.Infused;
                    break;
            }
        }

        private void SetMaxUpgrade(PARAM.Row weaponParam)
        {
            if (WeaponType == Type.PyroFlame)
            {
                MaxUpgrade = 15;
                return;
            }

            if (WeaponType == Type.PyroFlameAscended)
            {
                MaxUpgrade = 5;
                return;
            }

            for (int i = 24; i < 39; i++)
            {
                if ((int)weaponParam.Cells[i].Value > -1)
                    MaxUpgrade++;
            }
        }

        private void SetWeaponType()
        {
            switch (CategoryID)
            {
                case 0:
                    WeaponType = Type.Dagger;
                    break;
                case 1:
                    if (BehaviorID == 4300)
                        WeaponType = Type.Whip;
                    else
                        WeaponType = Type.Sword;
                    break;
                case 2:
                    WeaponType = Type.Rapier;
                    break;
                case 3:
                    WeaponType = Type.Curved;
                    break;
                case 4:
                    WeaponType = Type.Axe;
                    break;
                case 5:
                    WeaponType = Type.Blunt;
                    break;
                case 6:
                    WeaponType = Type.Spear;
                    break;
                case 7:
                    WeaponType = Type.Halberd;
                    break;
                case 8:
                    if (BaseChangeCategory == 1)
                        WeaponType = Type.PyroFlame;
                    else if (BaseChangeCategory == 2)
                        WeaponType = Type.PyroFlameAscended;
                    else
                        WeaponType = Type.SpellTool;
                    break;
                case 9:
                    WeaponType = Type.Fist;
                    break;
                case 10:
                    WeaponType = Type.Bow;
                    break;
                case 11:
                    WeaponType = Type.Crossbow;
                    break;
                case 12:
                    WeaponType = Type.Shield;
                    break;
                case 13:
                    WeaponType = Type.Arrow;
                    break;
                case 14:
                    WeaponType = Type.Bolt;
                    break;
                default:
                    break;
            }
        }

        public override string ToString()
        {
            if (ShowID)
                return $"{ID} {Name}";
            else
                return $"{Name}";
        }
    }
}
