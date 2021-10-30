using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    class DS1ViewModel : ObservableObject
    {
        public DS1Character _chr;
        public DS1Character Chr { get {return _chr; } 
            set 
            {
                _chr = value;
                OnPropertyChanged(nameof(Chr));
            }
        }

        public ObservableCollection<DS1Class> Classes { get; set; }

        public ObservableCollection<DS1Armor> HeadList { get; set; }
        public ObservableCollection<DS1Armor> BodyList { get; set; }
        public ObservableCollection<DS1Armor> ArmsList { get; set; }
        public ObservableCollection<DS1Armor> LegsList { get; set; }

        public DS1ViewModel()
        {
            //if (UserSettings.LocalUserSettings.LastDS1Character != null)
            //    Chr = UserSettings.LocalUserSettings.LastDS1Character;
            //else
            Chr = new DS1Character();
            Classes = new(DS1Class.Classes);
            HeadList = new(DS1Armor.ArmorHead);
            BodyList = new(DS1Armor.ArmorBody);
            ArmsList = new(DS1Armor.ArmorArms);
            LegsList = new(DS1Armor.ArmorLegs);
        }


    }
}
