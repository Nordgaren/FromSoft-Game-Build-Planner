using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    
    class DS1Armor
    {
        public enum Slot
        {
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

        public Slot ArmorSlot { get; set; }

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

        public DS1Armor(PARAM.Row armorParam, Slot slot)
        {
            ArmorSlot = slot;

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

            SlashDefense= (short)armorParam.Cells[53].Value;
            BlowDefense = (short)armorParam.Cells[54].Value;
            ThrustDefense = (short)armorParam.Cells[55].Value;

            PoisonResist = (ushort)armorParam.Cells[56].Value;
            ToxicResist = (ushort)armorParam.Cells[57].Value;
            BleedResist = (ushort)armorParam.Cells[58].Value;
            CurseResist = (ushort)armorParam.Cells[59].Value;

            Gender = (byte)armorParam.Cells[66].Value;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
