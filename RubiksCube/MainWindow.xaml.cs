using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RubiksCube
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Brush[] _cubeColors = { Brushes.Blue, Brushes.Green, Brushes.Yellow, Brushes.Red, Brushes.White, Brushes.Orange };

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            foreach (Label childLabel in rubriksGrid.Children)
            {
                Brush background = childLabel.Background;

                int index = Array.IndexOf(_cubeColors, background) + 1;

                if (index == _cubeColors.Length)
                    index = 0;

                childLabel.Background = _cubeColors[index];
            }
        }
    }
}