using LuigisPizza.Models;
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

namespace LuigisPizza
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, float> _toppings = new Dictionary<string, float>()
        {
            {"Mozzarella", 0.75F },
            {"Salami", 1.25F },
            {"Ansjovis", 0.50F },
            {"Artisjok", 0.80F },
        };

        List<Pizza> _pizzas = new List<Pizza>()
        {
            new Pizza("quattroStagioni", "Quattro Stagioni", 12.5F),
            new Pizza("capricciosa", "Capricciosa", 19F),
            new Pizza("salami", "Salami", 13.95F),
            new Pizza("prosciutto", "Prosciutto", 15.7F),
            new Pizza("quattroFromaggi", "Quattro Fromaggi", 18.2F),
            new Pizza("hawai", "Hawai", 12.5F),
            new Pizza("margherita", "Margherita", 9F)
        };

        Dictionary<Pizza, byte> _order;
        //Objecten die dynamisch worden opgehaald
        TextBox _textbox;
        Slider _slider;
        Button _minusButton;
        Button _plusButton;
        Random _random = new Random();
        int _totalNumber = 0;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += Timer_Tick;
            timer.Start();
            foreach (var topping in _toppings)
            {
                CheckBox checkBox = new CheckBox() { Name = $"{topping.Key.ToLower()}CheckBox", Margin = new Thickness(5), Content = $"Extra {topping.Key.ToLower()} ({topping.Value:c2})" };
                toppingsStackPanel.Children.Add(checkBox);
                toppingsStackPanel.RegisterName(checkBox.Name, checkBox); //https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/how-to-find-an-element-by-its-name
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            pizzaImage.Source = new BitmapImage(new Uri($"/Images/Pizza{_random.Next(1, 4)}.jpg", UriKind.RelativeOrAbsolute));
        }

        private string AddPizzaToOrder(ref double totalPrice, Pizza pizza, byte orderedAmount)
        {
            if (orderedAmount > 0)
            {
                _order.Add(pizza, orderedAmount);
                totalPrice += orderedAmount * pizza.Price;
                _totalNumber += orderedAmount;
                return $"{orderedAmount} x €{pizza.Price} - {pizza.Description}\n";
            }
            return null;
        }

        private void PlaceOrder(object sender, RoutedEventArgs e)
        {
            _order = new Dictionary<Pizza, byte>();
            double totalPrice = 0; //Deze variabele wordt byref doorgegeven aan de method AddPizzaToOrder
            _totalNumber = 0;
            StringBuilder ticket = new StringBuilder();
            ticket.AppendLine($"Naam: {nameTextBox.Text}");
            ticket.AppendLine($"Telefoonnummer: {phoneTextBox.Text}");
            ticket.AppendLine($"E-mail: {mailTextBox.Text}");
            ticket.AppendLine($"Adres: {addressTextBox.Text}");
            ticket.AppendLine($"Woonplaats: {cityTextBox.Text} - {zipcodeTextBox.Text}");
            ticket.AppendLine();
            ticket.AppendLine("U heeft de volgende pizza's besteld");
            ticket.AppendLine("-----------------------------------");
            //Loop door elke pizza en haal de bestelde hoeveelheid op.
            string orderline;
            foreach (var pizza in _pizzas)
            {
                orderline = AddPizzaToOrder(ref totalPrice, pizza, byte.Parse(((TextBox)FindName($"{pizza.Code}TextBox")).Text));
                if (orderline != null)
                {
                    ticket.AppendLine(orderline);
                }
            }
            ticket.AppendLine();
            foreach (var topping in _toppings)
            {
                if (((CheckBox)FindName($"{topping.Key.ToLower()}CheckBox")).IsChecked == true)
                {
                    totalPrice += _totalNumber * topping.Value;
                    ticket.AppendLine($"{_totalNumber} x {topping.Value} - extra {topping.Key}");
                }
            }
            ticket.AppendLine($"Totaalbedrag = {totalPrice:c2}");
            answerTextBlock.Text = ticket.ToString();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GetObjects(sender);
            _textbox.Text = $"{_slider.Value}";
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            //int increment = btn.Content.ToString() == "+" ? 1 : -1;
            int increment;
            if (btn.Content.ToString() == "+")
            {
                increment = 1;
            }
            else
            {
                increment = -1;
            }
            if (btn.Name.StartsWith("quattroStagioni"))
                quattroStagioniSlider.Value += increment;
            else if (btn.Name.StartsWith("capricciosa"))
                capricciosaSlider.Value += increment;
            else if (btn.Name.StartsWith("salami"))
                salamiSlider.Value += increment;
            else if (btn.Name.StartsWith("prosciutto"))
                prosciuttoSlider.Value += increment;
            else if (btn.Name.StartsWith("quattroFromaggi"))
                quattroFromaggiSlider.Value += increment;
            else if (btn.Name.StartsWith("hawai"))
                hawaiSlider.Value += increment;
            else if (btn.Name.StartsWith("margherita"))
                margheritaSlider.Value += increment;
        }

        private void Tb_KeyDown(object sender, KeyEventArgs e)
        {
            //Enkel numerische input
            e.Handled = !
                (
                    (e.Key >= Key.D0 && e.Key <= Key.D9 && e.KeyboardDevice.Modifiers == ModifierKeys.Shift) ||
                    (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                );
        }

        private void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded)
            {
                EnableOrderButton();
                GetObjects(sender);

                //Waarde doorsturen naar slider => deze handelt maxvalue (10) af, maar geen blanco => testen
                if (string.IsNullOrEmpty(_textbox.Text))
                {
                    _textbox.Text = "0";
                }
                _slider.Value = double.Parse(_textbox.Text);
                //Check max value
                _plusButton.IsEnabled = double.Parse(_textbox.Text) < _slider.Maximum;
                _minusButton.IsEnabled = double.Parse(_textbox.Text) > _slider.Minimum;
                if (double.Parse(_textbox.Text) > _slider.Value)
                {
                    _textbox.Text = _slider.Value.ToString();
                }
            }
        }

        private void GetObjects(object sender)
        {
            if (sender is TextBox)
            {
                _textbox = (TextBox)sender;
                _slider = (Slider)FindName($"{_textbox.Name.Substring(0, _textbox.Name.IndexOf("TextBox"))}Slider");
            }
            if (sender is Slider)
            {
                _slider = (Slider)sender;
                _textbox = (TextBox)FindName($"{_slider.Name.Substring(0, _slider.Name.IndexOf("Slider"))}TextBox");
            }
            _minusButton = (Button)FindName($"{_slider.Name.Substring(0, _slider.Name.IndexOf("Slider"))}MinButton");
            _plusButton = (Button)FindName($"{_slider.Name.Substring(0, _slider.Name.IndexOf("Slider"))}PlusButton");
        }

        //Deze versie is misschien leesbaarder, maar minder flexibel
        //private void GetObjects(object sender)
        //{
        //    if (sender is TextBox)
        //    {
        //        if (_textbox.Name == nameof(quattroStagioniTextBox))
        //            _slider = quattroStagioniSlider;
        //        else if (_textbox.Name == nameof(capricciosaTextBox))
        //            _slider = capricciosaSlider;
        //        else if (_textbox.Name == nameof(salamiTextBox))
        //            _slider = salamiSlider;
        //        else if (_textbox.Name == nameof(prosciuttoTextBox))
        //            _slider = prosciuttoSlider;
        //        else if (_textbox.Name == nameof(quattroFromaggiTextBox))
        //            _slider = quattroFromaggiSlider;
        //        else if (_textbox.Name == nameof(hawaiTextBox))
        //            _slider = hawaiSlider;
        //        else if (_textbox.Name == nameof(margheritaTextBox))
        //            _slider = margheritaSlider;
        //    }
        //    if (sender is Slider)
        //    {
        //        if (_slider.Name == nameof(quattroStagioniSlider))
        //            _textbox = quattroStagioniTextBox;
        //        else if (_slider.Name == nameof(capricciosaSlider))
        //            _textbox = capricciosaTextBox;
        //        else if (_slider.Name == nameof(salamiSlider))
        //            _textbox = salamiTextBox;
        //        else if (_slider.Name == nameof(prosciuttoSlider))
        //            _textbox = prosciuttoTextBox;
        //        else if (_slider.Name == nameof(quattroFromaggiSlider))
        //            _textbox = quattroFromaggiTextBox;
        //        else if (_slider.Name == nameof(hawaiSlider))
        //            _textbox = hawaiTextBox;
        //        else if (_slider.Name == nameof(margheritaSlider))
        //            _textbox = margheritaTextBox;
        //    }
        //    switch (_slider.Name)
        //    {
        //        case nameof(quattroStagioniSlider):
        //            _minusButton = quattroStagioniMinButton;
        //            _plusButton = quattroStagioniPlusButton;
        //            break;
        //        case nameof(capricciosaSlider):
        //            _minusButton = capricciosaMinButton;
        //            _plusButton = capricciosaPlusButton;
        //            break;
        //        case nameof(salamiSlider):
        //            _minusButton = salamiMinButton;
        //            _plusButton = salamiPlusButton;
        //            break;
        //        case nameof(prosciuttoSlider):
        //            _minusButton = prosciuttoMinButton;
        //            _plusButton = prosciuttoPlusButton;
        //            break;
        //        case nameof(quattroFromaggiSlider):
        //            _minusButton = quattroFromaggiMinButton;
        //            _plusButton = quattroFromaggiPlusButton;
        //            break;
        //        case nameof(hawaiSlider):
        //            _minusButton = hawaiMinButton;
        //            _plusButton = hawaiPlusButton;
        //            break;
        //        case nameof(margheritaSlider):
        //            _minusButton = margheritaMinButton;
        //            _plusButton = margheritaPlusButton;
        //            break;
        //    }
        //}

        private void NameAndPhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableOrderButton();
        }

        private void EnableOrderButton()
        {
            bool pizzaOrdered = false;
            foreach (var pizza in _pizzas)
            {
                pizzaOrdered |= byte.Parse(((TextBox)FindName($"{pizza.Code}TextBox")).Text) != 0;
            }
            orderButton.IsEnabled = !string.IsNullOrWhiteSpace(nameTextBox.Text) && !string.IsNullOrWhiteSpace(phoneTextBox.Text) && pizzaOrdered;
        }
    }
}