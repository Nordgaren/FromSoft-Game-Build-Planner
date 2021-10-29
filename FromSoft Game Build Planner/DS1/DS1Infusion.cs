using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    public class DS1Infusion
    {
        public string Name;
        public int Value;
        public int MaxUpgrade;
        public bool Restricted;

        private DS1Infusion(string name, int value, int maxUpgrade, bool restricted)
        {
            Name = name;
            Value = value;
            MaxUpgrade = maxUpgrade;
            Restricted = restricted;
        }

        public override string ToString()
        {
            return Name;
        }

        public static List<DS1Infusion> All = new List<DS1Infusion>()
        {
            new DS1Infusion("Normal", 000, 15, false),
            new DS1Infusion("Chaos", 900, 5, true),
            new DS1Infusion("Crystal", 100, 5, false),
            new DS1Infusion("Divine", 600, 10, false),
            new DS1Infusion("Enchanted", 500, 5, true),
            new DS1Infusion("Fire", 800, 10, false),
            new DS1Infusion("Lightning", 200, 5, false),
            new DS1Infusion("Magic", 400, 10, false),
            new DS1Infusion("Occult", 700, 5, true),
            new DS1Infusion("Raw", 300, 5, true),
        };
    }
}
