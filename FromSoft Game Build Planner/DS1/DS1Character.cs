using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    public class DS1Character : ObservableObject
    {
        public static int[] StaminaArray = new[] { 80, 80, 81, 82, 83, 84, 86, 87, 88, 90, 91, 93, 95, 97, 98, 100, 102, 104, 106, 108, 110, 112, 115, 117, 119, 121, 124, 126, 129, 131, 133, 136, 139, 141, 144, 146, 149, 152, 154, 157, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160 };

        public string Name { get; set; } = "DS1 Character Name";

        public int SoulLevel { get { return CalculateSL(); } }

        private int _humanity;

        public int Humanity
        {
            get { return _humanity; }
            set 
            { 
                _humanity = value;
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

        public int Health { get { 
                var lol = Calculate(100, Vitality);
                return (int)lol; } }

        public int Stamina {
            get
            {
                if (Endurance > 99)
                    return StaminaArray[99];

                if (Endurance < 0)
                    return StaminaArray[0];

                return StaminaArray[Endurance];
            }
        }
        
        public float EquipLoad { get { return GetEquipLoad(); } }

        private float GetEquipLoad()
        {
            var RH1Weapon = DS1Weapon.GetWeapon(RHandWeapon1, RHandInfusion1, RHandUpgrade1);
            var RH2Weapon = DS1Weapon.GetWeapon(RHandWeapon2, RHandInfusion2, RHandUpgrade2);
            var LH1Weapon = DS1Weapon.GetWeapon(LHandWeapon1, LHandInfusion1, LHandUpgrade1);
            var LH2Weapon = DS1Weapon.GetWeapon(LHandWeapon2, LHandInfusion2, LHandUpgrade2);

            if (RH1Weapon == null || RH2Weapon == null || LH1Weapon == null || LH2Weapon == null || Head == null || Body == null || Arms == null || Legs == null)
                return 0;

            return RH1Weapon.Weight + RH2Weapon.Weight + LH1Weapon.Weight + LH2Weapon.Weight + Head.Weight + Body.Weight + Arms.Weight + Legs.Weight;
        }

        public int MaxEquip { get { return 40 + Endurance; } }

        public string EquipPercent { get { return (EquipLoad / MaxEquip * 100).ToString("N2"); } }

        public int AttunementSlots { get; set; }

        private int _vitality;
        public int Vitality { get => _vitality;
            set
            {
                _vitality = value; 
                OnPropertyChanged(nameof(Health));
                OnPropertyChanged(nameof(SoulLevel));
                OnPropertyChanged(nameof(DefenseModel));
            }
        }
        
        private int _attunement;
        public int Attunement { get => _attunement;
            set
            {
                _attunement = value;
            }
        }

        private int _endurance;
        public int Endurance { get => _endurance;
            set
            {
                _endurance = value;
                OnPropertyChanged(nameof(MaxEquip));
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(Stamina));
                OnPropertyChanged(nameof(SoulLevel));
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

        private int _strength;
        public int Strength { get => _strength;
            set
            {
                _strength = value;
                CheckWeaponsStr();
                OnPropertyChanged(nameof(SoulLevel));
                OnPropertyChanged(nameof(DefenseModel));
            }
        }

        private void CheckWeaponsStr()
        {
            var RH1Weapon = DS1Weapon.GetWeapon(RHandWeapon1, RHandInfusion1, RHandUpgrade1);
            var RH2Weapon = DS1Weapon.GetWeapon(RHandWeapon2, RHandInfusion2, RHandUpgrade2);
            var LH1Weapon = DS1Weapon.GetWeapon(LHandWeapon1, LHandInfusion1, LHandUpgrade1);
            var LH2Weapon = DS1Weapon.GetWeapon(LHandWeapon2, LHandInfusion2, LHandUpgrade2);

            if (RH1Weapon != null)
                if (RH1Weapon.StrScaling > 0)
                    OnPropertyChanged(nameof(RHandDamage1));

            if (RH2Weapon != null)
                if (RH2Weapon.StrScaling > 0)
                    OnPropertyChanged(nameof(RHandDamage2));

            if (LH1Weapon != null)
                if (LH1Weapon.StrScaling > 0)
                    OnPropertyChanged(nameof(LHandDamage1));

            if (LH2Weapon != null)
                if (LH2Weapon.StrScaling > 0)
                    OnPropertyChanged(nameof(LHandDamage2));
        }

        private int _dexterity;
        public int Dexterity { get => _dexterity;
            set
            {
                _dexterity = value;
                CheckWeaponsDex();
                OnPropertyChanged(nameof(SoulLevel));
                OnPropertyChanged(nameof(DefenseModel));
            }
        }

        private void CheckWeaponsDex()
        {
            var RH1Weapon = DS1Weapon.GetWeapon(RHandWeapon1, RHandInfusion1, RHandUpgrade1);
            var RH2Weapon = DS1Weapon.GetWeapon(RHandWeapon2, RHandInfusion2, RHandUpgrade2);
            var LH1Weapon = DS1Weapon.GetWeapon(LHandWeapon1, LHandInfusion1, LHandUpgrade1);
            var LH2Weapon = DS1Weapon.GetWeapon(LHandWeapon2, LHandInfusion2, LHandUpgrade2);

            if (RH1Weapon != null)
                if (RH1Weapon.DexScaling > 0)
                    OnPropertyChanged(nameof(RHandDamage1));

            if (RH2Weapon != null)
                if (RH2Weapon.DexScaling > 0)
                    OnPropertyChanged(nameof(RHandDamage2));

            if (LH1Weapon != null)
                if (LH1Weapon.DexScaling > 0)
                    OnPropertyChanged(nameof(LHandDamage1));

            if (LH2Weapon != null)
                if (LH2Weapon.DexScaling > 0)
                    OnPropertyChanged(nameof(LHandDamage2));
        }

        private int _resistance;
        public int Resistance { get => _resistance;
            set
            {
                _resistance = value;
                OnPropertyChanged(nameof(SoulLevel));
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

        private int _intelligence;
        public int Intelligence { get => _intelligence;
            set
            {
                _intelligence = value;
                CheckWeaponsInt();
                OnPropertyChanged(nameof(SoulLevel));
                OnPropertyChanged(nameof(DefenseModel));
            }
        }

        private void CheckWeaponsInt()
        {
            var RH1Weapon = DS1Weapon.GetWeapon(RHandWeapon1, RHandInfusion1, RHandUpgrade1);
            var RH2Weapon = DS1Weapon.GetWeapon(RHandWeapon2, RHandInfusion2, RHandUpgrade2);
            var LH1Weapon = DS1Weapon.GetWeapon(LHandWeapon1, LHandInfusion1, LHandUpgrade1);
            var LH2Weapon = DS1Weapon.GetWeapon(LHandWeapon2, LHandInfusion2, LHandUpgrade2);

            if (RH1Weapon != null)
                if (RH1Weapon.IntScaling > 0)
                    OnPropertyChanged(nameof(RHandDamage1));

            if (RH2Weapon != null)
                if (RH2Weapon.IntScaling > 0)
                    OnPropertyChanged(nameof(RHandDamage2));

            if (LH1Weapon != null)
                if (LH1Weapon.IntScaling > 0)
                    OnPropertyChanged(nameof(LHandDamage1));

            if (LH2Weapon != null)
                if (LH2Weapon.IntScaling > 0)
                    OnPropertyChanged(nameof(LHandDamage2));
        }

        private int _faith;
        public int Faith { get => _faith; set
            {
                _faith = value;
                CheckWeaponsFai();
                OnPropertyChanged(nameof(SoulLevel));
                OnPropertyChanged(nameof(DefenseModel));
            }
        }

        private void CheckWeaponsFai()
        {
            var RH1Weapon = DS1Weapon.GetWeapon(RHandWeapon1, RHandInfusion1, RHandUpgrade1);
            var RH2Weapon = DS1Weapon.GetWeapon(RHandWeapon2, RHandInfusion2, RHandUpgrade2);
            var LH1Weapon = DS1Weapon.GetWeapon(LHandWeapon1, LHandInfusion1, LHandUpgrade1);
            var LH2Weapon = DS1Weapon.GetWeapon(LHandWeapon2, LHandInfusion2, LHandUpgrade2);

            if (RH1Weapon != null)
                if (RH1Weapon.FaiScaling > 0)
                    OnPropertyChanged(nameof(RHandDamage1));

            if (RH2Weapon != null)
                if (RH2Weapon.FaiScaling > 0)
                    OnPropertyChanged(nameof(RHandDamage2));

            if (LH1Weapon != null)
                if (LH1Weapon.FaiScaling > 0)
                    OnPropertyChanged(nameof(LHandDamage1));

            if (LH2Weapon != null)
                if (LH2Weapon.FaiScaling > 0)
                    OnPropertyChanged(nameof(LHandDamage2));
        }

        private DS1Class _class;

        public DS1Class Class
        {
            get { return _class; }
            set 
            { 
                _class = value;
                Vitality = Class.BaseVit;
                Attunement = Class.BaseAtt;
                Endurance = Class.BaseEnd;
                Strength = Class.BaseStr;
                Dexterity = Class.BaseDex;
                Resistance = Class.BaseRes;
                Intelligence = Class.BaseInt;
                Faith = Class.BaseFai;
                OnPropertyChanged(nameof(Class));
            }
        }

        #region RHWeapons
        public DS1DamageModel RHandDamage1
        {
            get { return CalculatAR(RHandWeapon1, RHandInfusion1, RHandUpgrade1, RHand2H_1); }
        }

        private DS1Weapon _rHandWeapon1;
        public DS1Weapon RHandWeapon1
        {
            get { return _rHandWeapon1; }
            set 
            { 
                _rHandWeapon1 = value;
                RHandInfusion1 = DS1Infusion.All[0];
                OnPropertyChanged(nameof(RHandDamage1));
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private DS1Infusion _rHandInfusion1;
        public DS1Infusion RHandInfusion1
        {
            get { return _rHandInfusion1; }
            set 
            { 
                _rHandInfusion1 = value;
                OnPropertyChanged(nameof(RHandDamage1));
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(SpecialDefenseModel));

            }
        }
        private int _rHandUpgrade1;
        public int RHandUpgrade1
        {
            get { return _rHandUpgrade1; }
            set
            {
                _rHandUpgrade1 = value;
                OnPropertyChanged(nameof(RHandDamage1));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private bool _rHand2H_1;
        public bool RHand2H_1
        {
            get { return _rHand2H_1; }
            set 
            { 
                _rHand2H_1 = value;
                OnPropertyChanged(nameof(RHandDamage1));
            }
        }

        public DS1DamageModel RHandDamage2
        {
            get { return CalculatAR(RHandWeapon2, RHandInfusion2, RHandUpgrade2, RHand2H_2); }
        }

        private DS1Weapon _rHandWeapon2;
        public DS1Weapon RHandWeapon2
        {
            get { return _rHandWeapon2; }
            set
            {
                _rHandWeapon2 = value;
                RHandInfusion2 = DS1Infusion.All[0];
                OnPropertyChanged(nameof(RHandDamage2));
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(RHandWeapon2));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private DS1Infusion _rHandInfusion2;
        public DS1Infusion RHandInfusion2
        {
            get { return _rHandInfusion2; }
            set
            {
                _rHandInfusion2 = value;
                OnPropertyChanged(nameof(RHandDamage2));
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private int _rHandUpgrade2;
        public int RHandUpgrade2
        {
            get { return _rHandUpgrade2; }
            set
            {
                _rHandUpgrade2 = value;
                OnPropertyChanged(nameof(RHandDamage2));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private bool _rHand2H_2;
        public bool RHand2H_2
        {
            get { return _rHand2H_2; }
            set
            {
                _rHand2H_2 = value;
                OnPropertyChanged(nameof(RHandDamage2));
            }
        }


        #endregion

        #region LHWeapons

        public DS1DamageModel LHandDamage1
        {
            get { return CalculatAR(LHandWeapon1, LHandInfusion1, LHandUpgrade1, LHand2H_1); }
        }

        private DS1Weapon _lHandWeapon1;
        public DS1Weapon LHandWeapon1
        {
            get { return _lHandWeapon1; }
            set
            {
                _lHandWeapon1 = value;
                LHandInfusion1 = DS1Infusion.All[0];
                OnPropertyChanged(nameof(LHandDamage1));
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private DS1Infusion _lHandInfusion1;
        public DS1Infusion LHandInfusion1
        {
            get { return _lHandInfusion1; }
            set
            {
                _lHandInfusion1 = value;
                OnPropertyChanged(nameof(LHandDamage1));
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private int _lHandUpgrade1;
        public int LHandUpgrade1
        {
            get { return _lHandUpgrade1; }
            set
            {
                _lHandUpgrade1 = value;
                OnPropertyChanged(nameof(LHandDamage1));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private bool _lHand2H_1;
        public bool LHand2H_1
        {
            get { return _lHand2H_1; }
            set
            {
                _lHand2H_1 = value;
                OnPropertyChanged(nameof(LHandDamage1));
            }
        }

        public DS1DamageModel LHandDamage2
        {
            get { return CalculatAR(LHandWeapon2, LHandInfusion2, LHandUpgrade2, LHand2H_2); }
        }

        private DS1Weapon _lHandWeapon2;
        public DS1Weapon LHandWeapon2
        {
            get { return _lHandWeapon2; }
            set
            {
                _lHandWeapon2 = value;
                LHandInfusion2 = DS1Infusion.All[0];
                OnPropertyChanged(nameof(LHandDamage2));
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private DS1Infusion _lHandInfusion2;
        public DS1Infusion LHandInfusion2
        {
            get { return _lHandInfusion2; }
            set
            {
                _lHandInfusion2 = value;
                OnPropertyChanged(nameof(LHandDamage2));
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private int _lHandUpgrade2;
        public int LHandUpgrade2
        {
            get { return _lHandUpgrade2; }
            set
            {
                _lHandUpgrade2 = value;
                OnPropertyChanged(nameof(LHandDamage2));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }
        private bool _lHand2H_2;
        public bool LHand2H_2
        {
            get { return _lHand2H_2; }
            set
            {
                _lHand2H_2 = value;
                OnPropertyChanged(nameof(LHandDamage2));
            }
        }
        #endregion

        public DS1DefenseModel DefenseModel { get { return GetDefense(); } }

        private DS1DefenseModel GetDefense()
        {
            var defModel = new DS1DefenseModel();

            if (Head == null || Body == null || Arms == null || Legs == null)
                return defModel;


            var headInfusion = Head.UpgradePath == DS1Armor.Upgrade.Unique ? 100 : 0;
            var headMultiplier = DS1ArmorUpgrade.ArmorUpgrades[headInfusion + HeadUpgrade];
            var bodyInfusion = Body.UpgradePath == DS1Armor.Upgrade.Unique ? 100 : 0;
            var bodyMultiplier = DS1ArmorUpgrade.ArmorUpgrades[bodyInfusion + BodyUpgrade];
            var armsInfusion = Arms.UpgradePath == DS1Armor.Upgrade.Unique ? 100 : 0;
            var armsMultiplier = DS1ArmorUpgrade.ArmorUpgrades[armsInfusion + ArmsUpgrade];
            var legsInfusion = Legs.UpgradePath == DS1Armor.Upgrade.Unique ? 100 : 0;
            var legsMultiplier = DS1ArmorUpgrade.ArmorUpgrades[legsInfusion + LegsUpgrade];

            var max = 800;
            var sum = Vitality + Attunement + Endurance  + Strength + Dexterity + Resistance + Intelligence + Faith + Humanity;
            var phys = .9f * sum + .9f * Resistance;
            phys = phys < max ? phys : max; 
            var fire = .85f * sum + 1.35f * Faith;
            fire = fire < max ? fire : max;
            var magic = .85f * sum + 1.35f * Resistance;
            magic = magic < max ? magic : max;
            var light = sum < max ? sum : max;
            defModel.PhysDefBase = (int)Math.Round(Calculate(102, phys));
            defModel.MagDefBase = (int)Math.Round(Calculate(102, fire));
            defModel.FireDefBase = (int)Math.Round(Calculate(102, magic));
            defModel.LightDefBase = (int)Math.Round(Calculate(102, light));

            defModel.PhysDef = (int)((Head.PhysicalDefense * headMultiplier.PhysicalMultiplier) + (Body.PhysicalDefense * bodyMultiplier.PhysicalMultiplier) + (Arms.PhysicalDefense * armsMultiplier.PhysicalMultiplier) + (Legs.PhysicalDefense * legsMultiplier.PhysicalMultiplier)) + defModel.PhysDefBase;
            defModel.Slash = (int)((Head.PhysicalDefense * (1 + Head.SlashDefense / 100f) * headMultiplier.PhysicalMultiplier * headMultiplier.SlashMultiplier) + (Body.PhysicalDefense * (1 + Body.SlashDefense / 100f) * bodyMultiplier.PhysicalMultiplier * bodyMultiplier.SlashMultiplier) + (Arms.PhysicalDefense * (1 + Arms.SlashDefense / 100f) * armsMultiplier.PhysicalMultiplier * armsMultiplier.SlashMultiplier) + (Legs.PhysicalDefense * (1 + Legs.SlashDefense / 100f) * legsMultiplier.PhysicalMultiplier * legsMultiplier.SlashMultiplier)) + defModel.PhysDefBase;
            defModel.Strike = (int)((Head.PhysicalDefense * (1 + Head.BlowDefense / 100f) * headMultiplier.PhysicalMultiplier * headMultiplier.BlowMultiplier) + (Body.PhysicalDefense * (1 + Body.BlowDefense / 100f) * bodyMultiplier.PhysicalMultiplier * bodyMultiplier.BlowMultiplier) + (Arms.PhysicalDefense * (1 + Arms.BlowDefense / 100f) * armsMultiplier.PhysicalMultiplier * armsMultiplier.BlowMultiplier) + (Legs.PhysicalDefense * (1 + Legs.BlowDefense / 100f) * legsMultiplier.PhysicalMultiplier * legsMultiplier.BlowMultiplier)) + defModel.PhysDefBase;
            defModel.Thrust = (int)((Head.PhysicalDefense * (1 + Head.ThrustDefense / 100f) * headMultiplier.PhysicalMultiplier * headMultiplier.ThrustMultiplier) + (Body.PhysicalDefense * (1 + Body.ThrustDefense / 100f) * bodyMultiplier.PhysicalMultiplier * bodyMultiplier.ThrustMultiplier) + (Arms.PhysicalDefense * (1 + Arms.ThrustDefense / 100f) * armsMultiplier.PhysicalMultiplier * armsMultiplier.ThrustMultiplier) + (Legs.PhysicalDefense * (1 + Legs.ThrustDefense / 100f) * legsMultiplier.PhysicalMultiplier * legsMultiplier.ThrustMultiplier)) + defModel.PhysDefBase;

            defModel.MagDef = (int)((Head.MagicDefense * headMultiplier.MagicMultiplier) + (Body.MagicDefense * bodyMultiplier.MagicMultiplier) + (Arms.MagicDefense * armsMultiplier.MagicMultiplier) + (Legs.MagicDefense * legsMultiplier.MagicMultiplier)) + defModel.MagDefBase;
            defModel.FireDef = (int)((Head.FireDefense * headMultiplier.FireMultiplier) + (Body.FireDefense * bodyMultiplier.FireMultiplier) + (Arms.FireDefense * armsMultiplier.FireMultiplier) + (Legs.FireDefense * legsMultiplier.FireMultiplier)) + defModel.FireDefBase;
            defModel.LightDef = (int)((Head.LightningDefense * headMultiplier.LightningMultiplier) + (Body.LightningDefense * bodyMultiplier.LightningMultiplier) + (Arms.LightningDefense * armsMultiplier.LightningMultiplier) + (Legs.LightningDefense * legsMultiplier.LightningMultiplier)) + defModel.LightDefBase;


            return defModel;
        }

        public DS1SpecialDefenseModel SpecialDefenseModel { get { return GetSpecialDefense(); } }

        private DS1SpecialDefenseModel GetSpecialDefense()
        {
            var specialDef = new DS1SpecialDefenseModel();

            if (RHandInfusion1 == null || RHandInfusion2 == null || LHandInfusion1 == null || LHandInfusion2 == null || Head == null || Body == null || Arms == null || Legs == null)
                return specialDef;

            var rh1Weapon = DS1Weapon.GetWeapon(RHandWeapon1, RHandInfusion1, RHandUpgrade1);
            var rh2Weapon = DS1Weapon.GetWeapon(RHandWeapon2, RHandInfusion2, RHandUpgrade2);
            var lh1Weapon = DS1Weapon.GetWeapon(LHandWeapon1, LHandInfusion1, LHandUpgrade1);
            var lh2Weapon = DS1Weapon.GetWeapon(LHandWeapon2, LHandInfusion2, LHandUpgrade2);

            var rh1Multiplier = DS1WeaponUpgrade.WeaponUpgrades[RHandInfusion1.Value + RHandUpgrade1];
            var rh2Multiplier = DS1WeaponUpgrade.WeaponUpgrades[RHandInfusion2.Value + LHandUpgrade2];
            var lh1Multiplier = DS1WeaponUpgrade.WeaponUpgrades[LHandInfusion1.Value + RHandUpgrade1];
            var lh2Multiplier = DS1WeaponUpgrade.WeaponUpgrades[LHandInfusion2.Value + LHandUpgrade2];

            var headInfusion = Head.UpgradePath == DS1Armor.Upgrade.Unique ? 100 : 0;
            var headMultiplier = DS1ArmorUpgrade.ArmorUpgrades[headInfusion + HeadUpgrade];
            var bodyInfusion = Body.UpgradePath == DS1Armor.Upgrade.Unique ? 100 : 0;
            var bodyMultiplier = DS1ArmorUpgrade.ArmorUpgrades[bodyInfusion + BodyUpgrade];
            var armsInfusion = Arms.UpgradePath == DS1Armor.Upgrade.Unique ? 100 : 0;
            var armsMultiplier = DS1ArmorUpgrade.ArmorUpgrades[armsInfusion + ArmsUpgrade];
            var legsInfusion = Legs.UpgradePath == DS1Armor.Upgrade.Unique ? 100 : 0;
            var legsMultiplier = DS1ArmorUpgrade.ArmorUpgrades[legsInfusion + LegsUpgrade];

            specialDef.BleedDefBase = (int)Math.Round(Calculate(112, Endurance));
            specialDef.PoisnDefBase = (int)Math.Round(Calculate(110, Resistance));
            specialDef.ToxicDefBase = (int)Math.Round(Calculate(111, Resistance));
            specialDef.CurseDefBase = (int)Math.Round(Calculate(113, Humanity < 30 ? Humanity : 30));

            specialDef.BleedDef = (int)((rh1Weapon.BleedResist * rh1Multiplier.BleedRes) + (rh2Weapon.BleedResist * rh2Multiplier.BleedRes) + (lh1Weapon.BleedResist * lh1Multiplier.BleedRes) + (lh2Weapon.BleedResist * lh2Multiplier.BleedRes) + (Head.BleedResist * headMultiplier.BleedMultiplier) + (Body.BleedResist * bodyMultiplier.BleedMultiplier) + (Arms.BleedResist * armsMultiplier.BleedMultiplier) + (Legs.BleedResist * legsMultiplier.BleedMultiplier)) + specialDef.BleedDefBase;
            specialDef.PoisnDef = (int)((rh1Weapon.PoisonResist * rh1Multiplier.PoisonRes) + (rh2Weapon.PoisonResist * rh2Multiplier.PoisonRes) + (lh1Weapon.PoisonResist * lh1Multiplier.PoisonRes) + (lh2Weapon.PoisonResist * lh2Multiplier.PoisonRes) + (Head.PoisonResist * headMultiplier.PoisonMultiplier) + (Body.PoisonResist * bodyMultiplier.PoisonMultiplier) + (Arms.PoisonResist * armsMultiplier.PoisonMultiplier) + (Legs.PoisonResist * legsMultiplier.PoisonMultiplier))+ specialDef.PoisnDefBase;
            specialDef.ToxicDef = (int)((rh1Weapon.ToxicResist * rh1Multiplier.ToxicRes) + (rh2Weapon.ToxicResist * rh2Multiplier.ToxicRes) + (lh1Weapon.ToxicResist * lh1Multiplier.ToxicRes) + (lh2Weapon.ToxicResist * lh2Multiplier.ToxicRes) + (Head.ToxicResist * headMultiplier.ToxicMultiplier) + (Body.ToxicResist * bodyMultiplier.ToxicMultiplier) + (Arms.ToxicResist * armsMultiplier.ToxicMultiplier) + (Legs.ToxicResist * legsMultiplier.ToxicMultiplier)) + specialDef.ToxicDefBase;
            specialDef.CurseDef = (int)((rh1Weapon.CurseResist * rh1Multiplier.CurseRes) + (rh2Weapon.CurseResist * rh2Multiplier.CurseRes) + (lh1Weapon.CurseResist * lh1Multiplier.CurseRes) + (lh2Weapon.CurseResist * lh2Multiplier.CurseRes) + (Head.CurseResist * headMultiplier.CurseMultiplier) + (Body.CurseResist * bodyMultiplier.CurseMultiplier) + (Arms.CurseResist * armsMultiplier.CurseMultiplier) + (Legs.CurseResist * legsMultiplier.CurseMultiplier)) + specialDef.CurseDefBase;

            return specialDef;
        }

        private DS1Armor _head;
        public DS1Armor Head { get { return _head; }
            set
            {
                _head = value;
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

        private int _headUpgrade;
        public int HeadUpgrade
        {
            get { return _headUpgrade; }
            set
            {
                _headUpgrade = value;
                OnPropertyChanged(nameof(RHandDamage1));
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

        private DS1Armor _body;
        public DS1Armor Body
        {
            get { return _body; }
            set
            {
                _body = value;
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

        private int _bodyUpgrade;
        public int BodyUpgrade
        {
            get { return _bodyUpgrade; }
            set
            {
                _bodyUpgrade = value;
                OnPropertyChanged(nameof(RHandDamage1));
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

        private DS1Armor _arms;
        public DS1Armor Arms
        {
            get { return _arms; }
            set
            {
                _arms = value;
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

        private int _armsUpgrade;
        public int ArmsUpgrade
        {
            get { return _armsUpgrade; }
            set
            {
                _armsUpgrade = value;
                OnPropertyChanged(nameof(RHandDamage1));
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

        private DS1Armor _legs;
        public DS1Armor Legs
        {
            get { return _legs; }
            set
            {
                _legs = value;
                OnPropertyChanged(nameof(EquipPercent));
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

        private int _legsUpgrade;
        public int LegsUpgrade
        {
            get { return _legsUpgrade; }
            set
            {
                _legsUpgrade = value;
                OnPropertyChanged(nameof(RHandDamage1));
                OnPropertyChanged(nameof(DefenseModel));
                OnPropertyChanged(nameof(SpecialDefenseModel));
            }
        }

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

        private int CalculateSL()
        {
            if (Class == null)
                return 0;

            int sl = Class.SoulLevel;
            sl += Vitality - Class.BaseVit;
            sl += Attunement - Class.BaseAtt;
            sl += Endurance - Class.BaseEnd;
            sl += Strength - Class.BaseStr;
            sl += Dexterity - Class.BaseDex;
            sl += Resistance - Class.BaseRes;
            sl += Intelligence - Class.BaseInt;
            sl += Faith - Class.BaseFai;
            return sl;
        }
        
        public DS1DamageModel CalculatAR(DS1Weapon weapon, DS1Infusion infusion, int upgrade, bool _2h)
        {
            weapon = DS1Weapon.GetWeapon(weapon, infusion, upgrade);

            if (weapon == null)
                return new DS1DamageModel();

            var damage = new DS1DamageModel();
            var strength = _2h ? Strength * 2 : Strength;

            if (strength < weapon.StrRequired || Dexterity < weapon.DexRequired || Intelligence < weapon.IntRequired || Faith < weapon.FaiRequired)
                return damage;

            damage.Useable = true;

            var infusionID = 000;
            if (infusion != null)
                infusionID = infusion.Value;

            var multiplier = DS1WeaponUpgrade.WeaponUpgrades[infusionID + upgrade];

            var physAttack = weapon.PhysicalAttack * multiplier.PhysicalMutliplier;
            var magicAttack = weapon.MagicAttack * multiplier.MagicMutliplier;
            var fireAttack = weapon.FireAttack * multiplier.FireMutliplier;
            var lightAttack = weapon.LightningAttack * multiplier.LightningMutliplier;

            damage.PhysAR = (int)physAttack;
            damage.MagAR = (int)magicAttack;
            damage.FireAR = (int)fireAttack;
            damage.LightAR = (int)lightAttack;
            damage.TotalAR = 0;
            damage.MagAdjust = 0;


            if (weapon.WeaponType == DS1Weapon.Type.SpellTool)
            {
                var magAdj = 100f;
                var faiScaling = weapon.FaiScaling * multiplier.FaiMultiplier;
                var faiAdj = GetStatDamage(Faith, weapon.FaiRequired, faiScaling, weapon.CorrectType, magAdj);
                var intScaling = weapon.IntScaling * multiplier.IntMultiplier;
                var intAdj = GetStatDamage(Intelligence, weapon.IntRequired, intScaling, weapon.CorrectType, magAdj);
                var humanityScaling = 0f;
                if (weapon.HumanityScaling)
                    humanityScaling = (float)GetHumanityDamage(faiAdj, intAdj, magicAttack); ;
                var scalingAdj = faiAdj.DSRound() + intAdj.DSRound();
                magAdj += scalingAdj + humanityScaling.DSRound();
                damage.MagAdjust = (int)magAdj;
                return damage;
            }

            if (weapon.StrScaling > 0 || weapon.DexScaling > 0)
            {
                //var str = 
                var strScaling = weapon.StrScaling * multiplier.StrMultiplier;
                var dexScaling = weapon.DexScaling * multiplier.DexMultiplier;
                var strDMG = GetStatDamage(strength, weapon.StrRequired, strScaling, weapon.CorrectType, physAttack);
                var dexDMG = GetStatDamage(Dexterity, weapon.DexRequired, dexScaling, weapon.CorrectType, physAttack);
                var humanityScaling = 0f;
                if (weapon.HumanityScaling)
                    humanityScaling = (float)GetHumanityDamage(strDMG, dexDMG, physAttack);
                var scalingDMG = strDMG.DSRound() + dexDMG.DSRound();
                physAttack += scalingDMG + humanityScaling.DSRound();
                damage.PhysAR = (int)physAttack;
            }

            if (weapon.IntScaling > 0 || weapon.FaiScaling > 0)
            {
                var intScaling = weapon.IntScaling * multiplier.IntMultiplier;
                var faiScaling = weapon.FaiScaling * multiplier.FaiMultiplier;
                var intDMG = GetStatDamage(Intelligence, weapon.IntRequired, intScaling, weapon.CorrectType, magicAttack);
                var faiDMG = GetStatDamage(Faith, weapon.FaiRequired, faiScaling, weapon.CorrectType, magicAttack);
                var humanityScaling = 0f;
                if (weapon.HumanityScaling)
                    humanityScaling = (float)GetHumanityDamage(intDMG, faiDMG, magicAttack);
                var scalingDMG = intDMG.DSRound() + faiDMG.DSRound();
                magicAttack += scalingDMG + humanityScaling.DSRound();
                damage.MagAR = (int)magicAttack;
            }

            if (weapon.WeaponType == DS1Weapon.Type.PyroFlame || weapon.WeaponType == DS1Weapon.Type.PyroFlameAscended)
            {
                var intScaling = weapon.IntScaling * multiplier.IntMultiplier;
                var intDMG = GetStatDamage(Intelligence, weapon.IntRequired, intScaling, weapon.CorrectType, fireAttack);
                var humanityScaling = 0f;
                if (weapon.HumanityScaling)
                    humanityScaling = (float)GetHumanityDamage(intDMG, intDMG, fireAttack);
                var scalingDMG = intDMG.DSRound();
                fireAttack += scalingDMG + humanityScaling.DSRound();
                var magAdjust = GetStatDamage(Intelligence, weapon.IntRequired, intScaling, weapon.CorrectType, fireAttack);
                damage.FireAR = (int)fireAttack.DSRound();
                damage.MagAdjust = (int)(100 + weapon.IntScaling).DSRound();
                return damage;
            }

            damage.TotalAR = (int)(physAttack + magicAttack + fireAttack + lightAttack);
            return damage;
        }

        float GetStatDamage(int stat, int statRequired, float statScaling, int correctType, float typeAttack)
        {
            if (stat >= statRequired)
            {
                var dS1CalcCorrect = DS1CalcCorrect.CalcCorrectGraph[correctType];

                if (stat <= dS1CalcCorrect.stgMaxVal0)
                {
                    return 0;
                }
                else if (stat > dS1CalcCorrect.stgMaxVal0 && stat <= dS1CalcCorrect.stgMaxVal1)
                {
                    return (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow1 / 100) - typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow0 / 100)) / (dS1CalcCorrect.stgMaxVal1 - dS1CalcCorrect.stgMaxVal0) * (stat - dS1CalcCorrect.stgMaxVal0) + (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow0 / 100));
                }
                else if (stat > dS1CalcCorrect.stgMaxVal1 && stat <= dS1CalcCorrect.stgMaxVal2)
                {
                    return (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow2 / 100) - typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow1 / 100)) / (dS1CalcCorrect.stgMaxVal2 - dS1CalcCorrect.stgMaxVal1) * (stat - dS1CalcCorrect.stgMaxVal1) + (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow1 / 100));
                }
                else if (stat > dS1CalcCorrect.stgMaxVal2 && stat <= dS1CalcCorrect.stgMaxVal3)
                {
                    return (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow3 / 100) - typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow2 / 100)) / (dS1CalcCorrect.stgMaxVal3 - dS1CalcCorrect.stgMaxVal2) * (stat - dS1CalcCorrect.stgMaxVal2) + (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow2 / 100));
                }
                else if (stat > dS1CalcCorrect.stgMaxVal3 && stat <= dS1CalcCorrect.stgMaxVal4)
                {
                    return (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow4 / 100) - typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow3 / 100)) / (dS1CalcCorrect.stgMaxVal4 - dS1CalcCorrect.stgMaxVal3) * (stat - dS1CalcCorrect.stgMaxVal3) + (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow3 / 100));
                }
                else if (stat > dS1CalcCorrect.stgMaxVal4)
                {
                    return (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow4 / 100) - typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow3 / 100)) / (dS1CalcCorrect.stgMaxVal4 - dS1CalcCorrect.stgMaxVal3) * (dS1CalcCorrect.stgMaxVal4 - dS1CalcCorrect.stgMaxVal3) + (typeAttack * (statScaling / 100) * (dS1CalcCorrect.stgMaxValGrow3 / 100));
                }
            }

            return 0;
        }

        float GetHumanityDamage(float statTypeDmg1, float statTypeDmg2, float typeAttack)
        {
            switch (Humanity)
            {
                case 0:
                    return 0;
                case 1:
                    return (typeAttack * 0.05f) + (statTypeDmg1 * 0.05f) + (statTypeDmg2 * 0.05f);
                case 2:
                    return (typeAttack * 0.075f) + (statTypeDmg1 * 0.075f) + (statTypeDmg2 * 0.075f);
                case 3:
                    return (typeAttack * 0.1f) + (statTypeDmg1 * 0.1f) + (statTypeDmg2 * 0.1f);
                case 4:
                    return (typeAttack * 0.1157f) + (statTypeDmg1 * 0.1157f) + (statTypeDmg2 * 0.1157f);
                case 5:
                    return (typeAttack * 0.1314f) + (statTypeDmg1 * 0.1314f) + (statTypeDmg2 * 0.1314f);
                case 6:
                    return (typeAttack * 0.1471f) + (statTypeDmg1 * 0.1471f) + (statTypeDmg2 * 0.1471f);
                case 7:
                    return (typeAttack * 0.1628f) + (statTypeDmg1 * 0.1628f) + (statTypeDmg2 * 0.1628f);
                case 8:
                    return (typeAttack * 0.1758f) + (statTypeDmg1 * 0.1758f) + (statTypeDmg2 * 0.1758f);
                case 9:
                    return (typeAttack * 0.1942f) + (statTypeDmg1 * 0.1942f) + (statTypeDmg2 * 0.1942f);
                default:
                    return (typeAttack * 0.21f) + (statTypeDmg1 * 0.21f) + (statTypeDmg2 * 0.21f);
            }
        }

        public float CalculateOutput(float stageMin, float stageMax, float valMin, float valMax, float inputVal, float multValMin, float multValMax)
        {
            float output;
            //v3 (perfect. no mults)
            /*
            decimal inputRatio = (inputVal - stageMin) / (stageMax - stageMin);
            output = valMin + ((valMax - valMin) * inputRatio); //standard output
            */

            //v11
            float inputRatio = (inputVal - stageMin) / (stageMax - stageMin);
            float growthVal;

            //calculate differently depending on if mult val is negative or positive
            if (multValMin > 0)
                growthVal = (float)Math.Pow(inputRatio, multValMin);
            else
                growthVal = 1 - (float)Math.Pow(1 - inputRatio, (float)Math.Abs(multValMin));

            output = valMin + ((valMax - valMin) * growthVal); //standard output

            //System.Diagnostics.Debug.WriteLine("raw output: " + output);

            return output;
        }

        private float Calculate(int correctType, float input)
        {

            var dS1CalcCorrect = DS1CalcCorrect.CalcCorrectGraph[correctType];

            float stageVal0 = dS1CalcCorrect.stgMaxVal0;
            float stageVal1 = dS1CalcCorrect.stgMaxVal1;
            float stageVal2 = dS1CalcCorrect.stgMaxVal2;
            float stageVal3 = dS1CalcCorrect.stgMaxVal3;
            float stageVal4 = dS1CalcCorrect.stgMaxVal4;
            float growVal0 = dS1CalcCorrect.stgMaxValGrow0;
            float growVal1 = dS1CalcCorrect.stgMaxValGrow1;
            float growVal2 = dS1CalcCorrect.stgMaxValGrow2;
            float growVal3 = dS1CalcCorrect.stgMaxValGrow3;
            float growVal4 = dS1CalcCorrect.stgMaxValGrow4;
            float multVal0 = dS1CalcCorrect.adjPt_MaxValGrow0;
            float multVal1 = dS1CalcCorrect.adjPt_MaxValGrow1;
            float multVal2 = dS1CalcCorrect.adjPt_MaxValGrow2;
            float multVal3 = dS1CalcCorrect.adjPt_MaxValGrow3;
            float multVal4 = dS1CalcCorrect.adjPt_MaxValGrow4;
            float inputVal = input;

            //error check. if stage max and min are the same then it will divide by zero
            if (stageVal0 >= stageVal1 || stageVal1 >= stageVal2 || stageVal2 >= stageVal3 || stageVal3 >= stageVal4)
            {
                //error: stage values not valid
                return 0f;
            }
            else if (inputVal < stageVal0)
            {
                //error: input is less than stage 0
                return 0f;
            }
            else if (inputVal <= stageVal1)
            {
                //stage 0-1
                return CalculateOutput(stageVal0, stageVal1, growVal0, growVal1, inputVal, multVal0, multVal1);
            }
            else if (inputVal <= stageVal2)
            {
                //stage 1-2
                return CalculateOutput(stageVal1, stageVal2, growVal1, growVal2, inputVal, multVal1, multVal2);
            }
            else if (inputVal <= stageVal3)
            {
                //stage 2-3
                return CalculateOutput(stageVal2, stageVal3, growVal2, growVal3, inputVal, multVal2, multVal3);
            }
            else if (inputVal <= stageVal4)
            {
                //stage 3-4 (and edge case beyond)
                return CalculateOutput(stageVal3, stageVal4, growVal3, growVal4, inputVal, multVal3, multVal4);
            }
            else
            {
                return 0f;
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}