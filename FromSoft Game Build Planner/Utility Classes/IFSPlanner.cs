using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    interface IFSPlanner
    {
        public abstract void Reload();
        void ResetArmor();
        void ResetWeapons();
        void ResetStats();
        void ResetCharacter();
        void SaveCharacter();
        void LoadCharacter();
        void LoadLast();
    }
}
