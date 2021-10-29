using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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


        public int BoundValue
        {
            get { return (int)GetValue(BoundValueProperty); }
            set { _numValue = value; SetValue(BoundValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BoundValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoundValueProperty =
            DependencyProperty.Register("BoundValue", typeof(int), typeof(IntegerUpDown), new PropertyMetadata(0));



        private int _numValue = 0;

        public int Value
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
            set { _maxValue = value; txtNum_TextChanged(null, null); }
        }


        public IntegerUpDown()
        {
            InitializeComponent();
            txtNum.Text = _numValue.ToString();
            Timer.Interval = 500;
            Timer.AutoReset = false;
            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                UpdateValue();

            });
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            Value++;
            OnValueChanged(EventArgs.Empty);
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            Value--;
            OnValueChanged(EventArgs.Empty);
        }

        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        System.Timers.Timer Timer = new System.Timers.Timer();

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {

            //if (sender != null && ((Control)sender).IsFocused)
            //{
            //    Timer.Start();
            //    return;
            //}
            if (sender != null && ((Control)sender).IsFocused)
                return;

            UpdateValue();
        }

        private void txtNum_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            int number = 0;
            if (txtNum.Text != "")
            {
                if (!int.TryParse(txtNum.Text, out number))
                {
                    txtNum.Text = _numValue.ToString();
                    return;
                }
                if (number > _maxValue) { _numValue = _maxValue; txtNum.Text = _numValue.ToString(); OnValueChanged(EventArgs.Empty); return; }
                if (number < _minValue) { _numValue = _minValue; txtNum.Text = _numValue.ToString(); OnValueChanged(EventArgs.Empty); return; }
                var lastNum = _numValue;
                _numValue = number;

                if (lastNum != _numValue)
                {
                    txtNum.Text = _numValue.ToString();
                    OnValueChanged(EventArgs.Empty);
                }

            }
        }
    }
}
