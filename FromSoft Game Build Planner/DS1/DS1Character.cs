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

        public DS1Weapon RHandWeapon1 { get; set; }
        public DS1Infusion RHandInfusion1 { get; set; }

        public DS1Weapon RHandWeapon2 { get; set; }
        public DS1Infusion RHandInfusion2 { get; set; }

        public DS1Weapon LHandWeapon1 { get; set; }
        public DS1Infusion LHandInfusion1 { get; set; }

        public DS1Weapon LHandWeapon2 { get; set; }
        public DS1Infusion LHandInfusion2 { get; set; }

        public DS1Armor Head { get; set; }
        public DS1Armor Body { get; set; }
        public DS1Armor Arms { get; set; }
        public DS1Armor Legs { get; set; }

        public DS1Ring Ring1 { get; set; }
        public DS1Ring Ring2 { get; set; }

        public DS1Item Item1 { get; set; }
        public DS1Item Item2 { get; set; }
        public DS1Item Item3 { get; set; }
        public DS1Item Item4 { get; set; }
        public DS1Item Item5 { get; set; }

        public DS1Spell Spell1 { get; set; }
        public DS1Spell Spell2 { get; set; }
        public DS1Spell Spell3 { get; set; }
        public DS1Spell Spell4 { get; set; }
        public DS1Spell Spell5 { get; set; }
        public DS1Spell Spell6 { get; set; }
        public DS1Spell Spell7 { get; set; }
        public DS1Spell Spell8 { get; set; }
        public DS1Spell Spell9 { get; set; }
        public DS1Spell Spell10 { get; set; }
        public DS1Spell Spell11 { get; set; }
        public DS1Spell Spell12 { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
