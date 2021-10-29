using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    
    public class DS1Armor
    {
        public enum Slot
        {
            None,
            Head,
            Body,
            Legs,
            Arms
        }

        public enum Upgrade
        {
            None = 0,
            Unique = 1,
            Armor = 2,
        }

        public static List<DS1Armor> ArmorHead = new List<DS1Armor>();
        public static List<DS1Armor> ArmorBody = new List<DS1Armor>();
        public static List<DS1Armor> ArmorArms = new List<DS1Armor>();
        public static List<DS1Armor> ArmorLegs = new List<DS1Armor>();

        public Slot ArmorSlot { get; set; }
        public Upgrade UpgradePath { get; set; }

        public string Name { get; set; }
        public int ID { get; set; }

        public float Weight { get; set; }
        public short Poise { get; set; }
        public float PoiseRecover { get; set; }

        public int EquipSpecialEffect1 { get; set; }
        public int EquipSpecialEffect2 { get; set; }
        public int EquipSpecialEffect3 { get; set; }

        public int UpgradeMaterial { get; set; }
        public short ReinforcementType { get; set; }

        public ushort IconIdM { get; set; }
        public ushort IconIdF { get; set; }

        public ushort PhysicalDefense { get; set; }
        public ushort MagicDefense { get; set; }
        public ushort FireDefense { get; set; }
        public ushort LightningDefense { get; set; }

        public short SlashDefense { get; set; }
        public short BlowDefense { get; set; }
        public short ThrustDefense { get; set; }

        public ushort PoisonResist { get; set; }
        public ushort ToxicResist { get; set; }
        public ushort BleedResist { get; set; }
        public ushort CurseResist { get; set; }

        public byte Gender { get; set; }
        public int MaxUpgrade { get; set; }

        public DS1Armor(PARAM.Row armorParam)
        {
            ArmorSlot = GetArmorSlot(armorParam);

            Name = armorParam.Name;
            ID = armorParam.ID;

            Weight = (float)armorParam.Cells[8].Value;
            Poise = (short)armorParam.Cells[47].Value;
            PoiseRecover = (float)armorParam.Cells[14].Value;

            EquipSpecialEffect1 = (int)armorParam.Cells[9].Value;
            EquipSpecialEffect2 = (int)armorParam.Cells[10].Value;
            EquipSpecialEffect3 = (int)armorParam.Cells[11].Value;

            UpgradeMaterial = (int)armorParam.Cells[12].Value;
            ReinforcementType = (short)armorParam.Cells[60].Value;

            IconIdM = (ushort)armorParam.Cells[41].Value;
            IconIdF = (ushort)armorParam.Cells[42].Value;

            PhysicalDefense = (ushort)armorParam.Cells[49].Value;
            MagicDefense = (ushort)armorParam.Cells[50].Value;
            FireDefense = (ushort)armorParam.Cells[51].Value;
            LightningDefense = (ushort)armorParam.Cells[52].Value;

            SlashDefense = (short)armorParam.Cells[53].Value;
            BlowDefense = (short)armorParam.Cells[54].Value;
            ThrustDefense = (short)armorParam.Cells[55].Value;

            PoisonResist = (ushort)armorParam.Cells[56].Value;
            ToxicResist = (ushort)armorParam.Cells[57].Value;
            BleedResist = (ushort)armorParam.Cells[58].Value;
            CurseResist = (ushort)armorParam.Cells[59].Value;

            for (int i = 16; i < 30; i++)
            {
                if ((int)armorParam.Cells[i].Value > -1)
                    MaxUpgrade++;
            }

            if (MaxUpgrade > 5)
                UpgradePath = Upgrade.Armor;
            else if (MaxUpgrade == 0)
                UpgradePath = Upgrade.None;
            else 
                UpgradePath = Upgrade.Unique;



            Gender = (byte)armorParam.Cells[66].Value;

            switch (ArmorSlot)
            {
                case Slot.Head:
                    ArmorHead.Add(this);
                    break;
                case Slot.Body:
                    ArmorBody.Add(this);
                    break;
                case Slot.Legs:
                    ArmorLegs.Add(this);
                    break;
                case Slot.Arms:
                    ArmorArms.Add(this);
                    break;
                default:
                    break;
            }
        }

        private Slot GetArmorSlot(PARAM.Row armorParam)
        {
            if ((byte)armorParam.Cells[74].Value == 0x1)
                return Slot.Head;
            else if ((byte)armorParam.Cells[75].Value == 0x1)
                return Slot.Body;
            else if ((byte)armorParam.Cells[76].Value == 0x1)
                return Slot.Arms;
            else if ((byte)armorParam.Cells[77].Value == 0x1)
                return Slot.Legs;

            return Slot.None;
        }
        
        public override string ToString()
        {
            return Name;
        }

    }
}
