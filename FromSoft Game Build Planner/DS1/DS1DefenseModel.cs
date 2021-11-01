using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    public class DS1DefenseModel
    {
        public int PhysDefBase { get; set; }
        public int PhysDef { get; set; }
        public int Strike { get; set; }
        public int Slash { get; set; }
        public int Thrust { get; set; }

        public int MagDefBase { get; set; }
        public int MagDef { get; set; }

        public int FireDefBase { get; set; }
        public int FireDef { get; set; }

        public int LightDefBase { get; set; }
        public int LightDef { get; set; }
    }
}
