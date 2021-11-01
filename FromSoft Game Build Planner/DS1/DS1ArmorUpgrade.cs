using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    public class DS1ArmorUpgrade
    {
        public static Dictionary<int, DS1ArmorUpgrade> ArmorUpgrades;

        public string Name { get; set; }
        public int ID { get; set; }

        public float PhysicalMultiplier { get; set; }
        public float MagicMultiplier { get; set; }
        public float FireMultiplier { get; set; }
        public float LightningMultiplier { get; set; }

        public float SlashMultiplier { get; set; }
        public float BlowMultiplier { get; set; }
        public float ThrustMultiplier { get; set; }


        public float PoisonMultiplier { get; set; }
        public float ToxicMultiplier { get; set; }
        public float BleedMultiplier { get; set; }
        public float CurseMultiplier { get; set; }

        public DS1ArmorUpgrade(PARAM.Row armorReinforceParam)
        {
            Name = armorReinforceParam.Name;
            ID = armorReinforceParam.ID;

            PhysicalMultiplier = (float)armorReinforceParam.Cells[0].Value;
            MagicMultiplier = (float)armorReinforceParam.Cells[1].Value;
            FireMultiplier = (float)armorReinforceParam.Cells[2].Value;
            LightningMultiplier = (float)armorReinforceParam.Cells[3].Value;

            SlashMultiplier = (float)armorReinforceParam.Cells[4].Value;
            BlowMultiplier = (float)armorReinforceParam.Cells[5].Value;
            ThrustMultiplier = (float)armorReinforceParam.Cells[6].Value;

            PoisonMultiplier = (float)armorReinforceParam.Cells[7].Value;
            ToxicMultiplier = (float)armorReinforceParam.Cells[8].Value;
            BleedMultiplier = (float)armorReinforceParam.Cells[9].Value;
            CurseMultiplier = (float)armorReinforceParam.Cells[10].Value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
