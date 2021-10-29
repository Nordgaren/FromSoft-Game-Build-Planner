using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    public class DS1Ring
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public ushort IconID { get; set; }

        public int SpecialEffect { get; set; }

        public float Weight { get; set; }

        public byte Gender { get; set; }

        public DS1Ring(PARAM.Row ringParam)
        {
            Name = ringParam.Name;
            ID = ringParam.ID;

            SpecialEffect = (int)ringParam.Cells[0].Value;

            IconID = (ushort)ringParam.Cells[9].Value;

            Weight = (float)ringParam.Cells[2].Value;

            Gender = (byte)ringParam.Cells[14].Value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
