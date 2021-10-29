using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    public class DS1Spell
    {
        public enum Type
        {
            Sorcery,
            Miracle,
            Pyromancy
        }

        public string Name { get; set; }
        public int ID { get; set; }
        public short IconID { get; set; }

        public Type SpellType { get; set; }

        public short Casts { get; set; }

        public byte Slots { get; set; }

        public byte IntRequired { get; set; }
        public byte FaiRequired { get; set; }

        public byte OverDexterity { get; set; }
        public byte AnalogDexiterityMin { get; set; }
        public byte AnalogDexiterityMax { get; set; }

        public DS1Spell(PARAM.Row spellParam, byte spellType)
        {
            Name = spellParam.Name;
            ID = spellParam.ID;

            IconID = (short)spellParam.Cells[6].Value;

            SpellType = (Type)Enum.Parse(typeof(Type) ,spellType.ToString());

            Casts = (short)spellParam.Cells[10].Value;

            Slots = (byte)spellParam.Cells[14].Value;

            IntRequired = (byte)spellParam.Cells[15].Value;
            FaiRequired = (byte)spellParam.Cells[16].Value;

            OverDexterity = (byte)spellParam.Cells[12].Value;
            AnalogDexiterityMin = (byte)spellParam.Cells[17].Value;
            AnalogDexiterityMax = (byte)spellParam.Cells[18].Value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
