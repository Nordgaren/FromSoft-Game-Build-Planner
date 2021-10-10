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
    /// Interaction logic for IntegerUpDown.xaml
    /// </summary>
    public partial class IntegerUpDown : UserControl
    {
        private int _numValue = 0;

        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                txtNum.Text = value.ToString();
            }
        }

        private int _minValue = 0;

        public int MinValue
        {
            get { return _minValue; }
            set { _minValue = value; txtNum_TextChanged(null, null); }
        }

        private int _maxValue = 99;

        public int MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }


        public IntegerUpDown()
        {
            InitializeComponent();
            txtNum.Text = _numValue.ToString();
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValue++;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            NumValue--;
        }

        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (txtNum.Text != "")
                if (!int.TryParse(txtNum.Text, out number)) txtNum.Text = _numValue.ToString();
            if (number > _maxValue) NumValue = _maxValue;
            if (number < _minValue) NumValue = _minValue;
            txtNum.SelectionStart = txtNum.Text.Length;
            OnValueChanged(EventArgs.Empty);
        }

    }
}
