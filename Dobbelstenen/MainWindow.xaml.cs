using System.Diagnostics.Metrics;
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

namespace Dobbelstenen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random _rng = new Random();
        DispatcherTimer _timer = new DispatcherTimer();
        int _counter = 0;
        Dictionary<int, int> _simulation = new Dictionary<int, int>();

        public MainWindow()
        {
            InitializeComponent();

            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += OnTimerTick;
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            if(_counter < (int)numberOfRollsLabel.Content)
            {
                int[] result = RollDices();
                DisplayDices(result);

                int sum = result[0] + result[1];

                if(_simulation.ContainsKey(sum))
                {
                    _simulation[sum]++;
                }
                else
                {
                    _simulation.Add(sum, 1);
                }

                _counter++;
            }
            else
            {
                _timer.Stop();
                ShowSimulationResult();
            }
        }

        private void OnRollOnceClicked(object sender, RoutedEventArgs e)
        {
            DisplayDices(RollDices());
        }

        private void OnRollSimuationClicked(object sender, RoutedEventArgs e)
        {
            _simulation.Clear();
            _counter = 0;
            _timer.Start();
        }

        private int[] RollDices()
        {
            int[] dices = new int[2];
            dices[0] = _rng.Next(1, 7);
            dices[1] = _rng.Next(1, 7);
            return dices;
        }

        private void DisplayDices(int[] dices)
        {
            firstDiceImage.Source = new BitmapImage(new Uri($"Images/dice-{dices[0]}.png", UriKind.Relative));
            secondDiceImage.Source = new BitmapImage(new Uri($"Images/dice-{dices[1]}.png", UriKind.Relative));
        }

        private void ShowSimulationResult()
        {
            result2TextBox.Text = _simulation.TryGetValue(2, out int value2) ? value2.ToString() : "0";
            result3TextBox.Text = _simulation.TryGetValue(3, out int value3) ? value3.ToString() : "0";
            result4TextBox.Text = _simulation.TryGetValue(4, out int value4) ? value4.ToString() : "0";
            result5TextBox.Text = _simulation.TryGetValue(5, out int value5) ? value5.ToString() : "0";
            result6TextBox.Text = _simulation.TryGetValue(6, out int value6) ? value6.ToString() : "0";
            result7TextBox.Text = _simulation.TryGetValue(7, out int value7) ? value7.ToString() : "0";
            result8TextBox.Text = _simulation.TryGetValue(8, out int value8) ? value8.ToString() : "0";
            result9TextBox.Text = _simulation.TryGetValue(9, out int value9) ? value9.ToString() : "0";
            result10TextBox.Text = _simulation.TryGetValue(10, out int value10) ? value10.ToString() : "0";
            result11TextBox.Text = _simulation.TryGetValue(11, out int value11) ? value11.ToString() : "0";
            result12TextBox.Text = _simulation.TryGetValue(12, out int value12) ? value12.ToString() : "0";
        }

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnSliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(numberOfRollsLabel is not null)
                numberOfRollsLabel.Content = (int)((Slider)sender).Value;
        }
    }
}