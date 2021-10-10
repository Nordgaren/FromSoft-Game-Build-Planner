using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    class DS1Class
    {
        public string Name { get; set; }

        public short SoulLevel { get; set; } 
     
        public byte BaseVit { get; set; }
        public byte BaseAtt { get; set; }
        public byte BaseEnd { get; set; }
        public byte BaseStr { get; set; }
        public byte BaseDex { get; set; }
        public byte BaseRes { get; set; }
        public byte BaseInt { get; set; }
        public byte BaseFai { get; set; }

        public byte StartingHumanity { get; set; }

        public DS1Class(PARAM.Row classParam)
        {
            Name = classParam.Name;

            SoulLevel = (short)classParam.Cells[53].Value;

            BaseVit = (byte)classParam.Cells[54].Value;
            BaseAtt = (byte)classParam.Cells[55].Value;
            BaseEnd = (byte)classParam.Cells[56].Value;
            BaseStr = (byte)classParam.Cells[57].Value;
            BaseDex = (byte)classParam.Cells[58].Value;
            BaseRes = (byte)classParam.Cells[63].Value;
            BaseInt = (byte)classParam.Cells[59].Value;
            BaseFai = (byte)classParam.Cells[60].Value;

            StartingHumanity = (byte)classParam.Cells[62].Value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
