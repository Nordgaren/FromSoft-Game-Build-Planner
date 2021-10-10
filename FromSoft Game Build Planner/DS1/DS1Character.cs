using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    class DS1Character
    {
        public string Name { get; set; }

        public int SoulLevel { get; set; }

        public int Humanity { get; set; }

        public int Health { get; set; }
        public int Stamina { get; set; }

        public int Vitality { get; set; }
        public int Attuntment { get; set; }
        public int Endurance { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Resistance { get; set; }
        public int Intelligence { get; set; }
        public int Faith { get; set; }

        public DS1Class Class { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
