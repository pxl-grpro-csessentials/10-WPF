using Login.Models;
using Login.Services;
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

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager _userMgr = new UserManager();

        public MainWindow()
        {
            InitializeComponent();
            infoTextBlock.Text = string.Empty;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_userMgr.TryLogin(new Registration(userNameTextBox.Text, userPasswordBox.Password)))
            {
                ShowInfo("Login geslaagd", Brushes.Green);
            }
            else
            {
                ShowInfo($"Ongeldige gebruikersnaam of wachtwoord (nog {_userMgr.AttemptsRemaining} pogingen te gaan)", Brushes.Red);
            }
            loginButton.IsEnabled = _userMgr.AttemptsRemaining > 0;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void userNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userNameTextBox.Text))
            {
                ShowInfo("Geef je gebruikersnaam", Brushes.Gray);
            }
        }

        private void userPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userPasswordBox.Password))
            {
                ShowInfo("Geef je wachtwoord", Brushes.Gray);
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            if (_userMgr.Register(userNameTextBox.Text, userPasswordBox.Password))
            {
                ShowInfo("Nieuwe gebruiker is geregistreerd", Brushes.Gray);
            }
            else
            {
                ShowInfo("Registratie mislukt!", Brushes.Gray);
            }
            ClearScreen(true);
        }

        private void ShowInfo(string message, Brush color)
        {
            infoTextBlock.Text = message;
            infoTextBlock.Foreground = color;
        }

        private void ClearScreen(bool resetCounter = false)
        {
            userNameTextBox.Clear();
            userPasswordBox.Clear();
            if (resetCounter)
            {
                _userMgr.Reset();
                loginButton.IsEnabled = true;
            }
            userNameTextBox.Focus();
        }
    }
}