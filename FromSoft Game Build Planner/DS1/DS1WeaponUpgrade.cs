using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    class DS1WeaponUpgrade
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public float PhysicalMutliplier { get; set; }
        public float MagicMutliplier { get; set; }
        public float FireMutliplier { get; set; }
        public float LightningMutliplier { get; set; }

        public float StaminaDamage { get; set; }

        public float StrMultiplier { get; set; }
        public float DexMultiplier { get; set; }
        public float IntMultiplier { get; set; }
        public float FaiMultiplier { get; set; }

        public DS1WeaponUpgrade(PARAM.Row weapReinforceParam)
        {
            Name = weapReinforceParam.Name;
            ID = weapReinforceParam.ID;

            PhysicalMutliplier = (float)weapReinforceParam.Cells[0].Value;
            MagicMutliplier = (float)weapReinforceParam.Cells[1].Value;
            FireMutliplier = (float)weapReinforceParam.Cells[2].Value;
            LightningMutliplier = (float)weapReinforceParam.Cells[3].Value;

            StaminaDamage = (float)weapReinforceParam.Cells[4].Value;

            StrMultiplier = (float)weapReinforceParam.Cells[7].Value;
            DexMultiplier = (float)weapReinforceParam.Cells[8].Value;
            IntMultiplier = (float)weapReinforceParam.Cells[9].Value;
            FaiMultiplier = (float)weapReinforceParam.Cells[10].Value;

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
