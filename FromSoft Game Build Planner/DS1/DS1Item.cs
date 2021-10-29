using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    public class DS1Item
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public bool Equip { get; set; }

        public bool Consumable { get; set; }

        public DS1Item(PARAM.Row itemParam)
        {
            Name = itemParam.Name;
            ID = itemParam.ID;

            Equip = (byte)itemParam.Cells[49].Value == 1;

            Consumable = (byte)itemParam.Cells[50].Value == 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
