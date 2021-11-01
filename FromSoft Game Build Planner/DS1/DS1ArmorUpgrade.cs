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

        public float PhysicalMutliplier { get; set; }
        public float MagicMutliplier { get; set; }
        public float FireMutliplier { get; set; }
        public float LightningMutliplier { get; set; }

        public float PoisonMultiplier { get; set; }
        public float ToxicMultiplier { get; set; }
        public float BleedMultiplier { get; set; }
        public float CurseMultiplier { get; set; }

        public DS1ArmorUpgrade(PARAM.Row armorReinforceParam)
        {
            Name = armorReinforceParam.Name;
            ID = armorReinforceParam.ID;

            PhysicalMutliplier = (float)armorReinforceParam.Cells[0].Value;
            MagicMutliplier = (float)armorReinforceParam.Cells[1].Value;
            FireMutliplier = (float)armorReinforceParam.Cells[2].Value;
            LightningMutliplier = (float)armorReinforceParam.Cells[3].Value;

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
