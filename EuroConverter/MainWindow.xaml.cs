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

namespace EuroConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const decimal ExchangeRate = 40.3399M;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            euroTextBox.SelectAll();
            euroTextBox.Focus();
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            string input = euroTextBox.Text;
            decimal euro = decimal.Parse(input);
            decimal frank = euro * ExchangeRate;
            frankTextBox.Text = frank.ToString("F2");

            euroTextBox.SelectAll();
            euroTextBox.Focus();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            euroTextBox.Clear();
            frankTextBox.Clear();
            euroTextBox.Focus();
        }

        private void euroTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!decimal.TryParse(euroTextBox.Text, out _))
            {
                euroTextBox.Foreground = Brushes.Red;
            }
            else
            {
                euroTextBox.Foreground = Brushes.Black;
            }
        }
    }
}