using System.Windows;

namespace FromSoft_Game_Build_Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var DS1 = new DarkSouls1();

            DS1.Show();
            Close();
        }
    }
}
