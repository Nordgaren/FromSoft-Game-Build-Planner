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

        public int Humanity { get; set; }

        public int Health { get { return (int)GetStatDamage(Vitality, 0, 100, 100, 100); } }
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

        public string EquipPercent { get { return (EquipLoad / MaxEquip * 100).ToString(); } }

        public int AttunementSlots { get; set; }

        private int _vitality;
        public int Vitality { get => _vitality;
            set
            {
                _vitality = value; 
                OnPropertyChanged(nameof(Health));
                OnPropertyChanged(nameof(SoulLevel));
            }
        }
        
        private int _attunement;
        public int Attunement { get => _attunement;
            set
            {
                _attunement = value;
                OnPropertyChanged(nameof(SoulLevel));
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
            }
        }

        private int _strength;
        public int Strength { get => _strength;
            set
            {
                _strength = value;
                CheckWeaponsStr();
                OnPropertyChanged(nameof(SoulLevel)); 
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
            }
        }

        private int _intelligence;
        public int Intelligence { get => _intelligence;
            set
            {
                _intelligence = value;
                CheckWeaponsInt();
                OnPropertyChanged(nameof(SoulLevel));
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
                OnPropertyChanged(nameof(EquipLoad));
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
                OnPropertyChanged(nameof(EquipLoad));
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
                OnPropertyChanged(nameof(EquipLoad));
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
                OnPropertyChanged(nameof(EquipLoad));
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
                OnPropertyChanged(nameof(EquipLoad));
                OnPropertyChanged(nameof(EquipPercent));
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
                OnPropertyChanged(nameof(EquipLoad));
                OnPropertyChanged(nameof(EquipPercent));
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
                OnPropertyChanged(nameof(EquipLoad));
                OnPropertyChanged(nameof(EquipPercent));
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
                OnPropertyChanged(nameof(EquipLoad));
                OnPropertyChanged(nameof(EquipPercent));
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

        //public DS1Weapon LHandWeapon2 { get; set; }
        //public DS1Infusion LHandInfusion2 { get; set; }
        //public int LHandUpgrade2 { get; set; }
        //public bool LHand2H_2 { get; set; }

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

        public ObservableCollection<DS1Class> Classes { get; set; }

        public DS1Character()
        {
            Classes = new ObservableCollection<DS1Class>(DS1Class.Classes);
        }

        private int CalculateSL()
        {
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
            if (DarkSouls1.NotLoading)
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
                    magAdj += faiAdj + intAdj + humanityScaling;
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
                        humanityScaling = (float)GetHumanityDamage(strDMG, dexDMG, magicAttack);
                    var scalingDMG = strDMG + dexDMG;
                    physAttack += scalingDMG;
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
                    var scalingDMG = intDMG + faiDMG;
                    magicAttack += scalingDMG + humanityScaling;
                    damage.MagAR = (int)magicAttack;
                }

                if (weapon.WeaponType == DS1Weapon.Type.PyroFlame || weapon.WeaponType == DS1Weapon.Type.PyroFlameAscended)
                {
                    var intScaling = weapon.IntScaling * multiplier.IntMultiplier;
                    var intDMG = GetStatDamage(Intelligence, weapon.IntRequired, intScaling, weapon.CorrectType, fireAttack);
                    var humanityScaling = 0f;
                    if (weapon.HumanityScaling)
                        humanityScaling = (float)GetHumanityDamage(intDMG, intDMG, magicAttack);
                    var scalingDMG = intDMG;
                    fireAttack += scalingDMG + humanityScaling;
                    var magAdjust = GetStatDamage(Intelligence, weapon.IntRequired, intScaling, weapon.CorrectType, fireAttack);
                    damage.FireAR = (int)fireAttack;
                    damage.MagAdjust = (int)(100 + weapon.IntScaling);
                    return damage;
                }

                damage.TotalAR = (int)(physAttack + magicAttack + fireAttack + lightAttack);
                return damage;
            }

            return new DS1DamageModel();
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

        public override string ToString()
        {
            return Name;
        }
    }
}