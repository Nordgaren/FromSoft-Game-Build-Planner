using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FromSoft_Game_Build_Planner
{
    /// <summary>
    /// Interaction logic for DefenseControl.xaml
    /// </summary>
    public partial class DefenseControl : UserControl
    {
        public DefenseControl()
        {
            InitializeComponent();
        }

        public DS1DefenseModel DefenseModel
        {
            get { return (DS1DefenseModel)GetValue(DefenseModelProperty); }
            set { SetValue(DefenseModelProperty, value); }
        }

        public static readonly DependencyProperty DefenseModelProperty =
            DependencyProperty.Register("DefenseModel", typeof(DS1DefenseModel), typeof(DefenseControl), new PropertyMetadata(default(DS1DefenseModel)));



        public DS1SpecialDefenseModel SpecialDefenseModel
        {
            get { return (DS1SpecialDefenseModel)GetValue(SpecialDefenseModelProperty); }
            set { SetValue(SpecialDefenseModelProperty, value); }
        }

        public static readonly DependencyProperty SpecialDefenseModelProperty =
            DependencyProperty.Register("SpecialDefenseModel", typeof(DS1SpecialDefenseModel), typeof(DefenseControl), new PropertyMetadata(default(DS1SpecialDefenseModel)));


    }
}
