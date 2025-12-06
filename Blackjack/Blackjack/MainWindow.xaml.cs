using Blackjack.Models;
using System.ComponentModel.DataAnnotations;
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

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Card[] _deck = new Card[]
        {
            // CLUBS
            new Card { ImageUrl="/images/cards/clubs_A.png", Value=new[] {1,11}},
            new Card { ImageUrl="/images/cards/clubs_2.png", Value=new[] {2}},
            new Card { ImageUrl="/images/cards/clubs_3.png", Value=new[] {3}},
            new Card { ImageUrl="/images/cards/clubs_4.png", Value=new[] {4}},
            new Card { ImageUrl="/images/cards/clubs_5.png", Value=new[] {5}},
            new Card { ImageUrl="/images/cards/clubs_6.png", Value=new[] {6}},
            new Card { ImageUrl="/images/cards/clubs_7.png", Value=new[] {7}},
            new Card { ImageUrl="/images/cards/clubs_8.png", Value=new[] {8}},
            new Card { ImageUrl="/images/cards/clubs_9.png", Value=new[] {9}},
            new Card { ImageUrl="/images/cards/clubs_10.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/clubs_J.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/clubs_Q.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/clubs_K.png", Value=new[] {10}},

            // DIAMONDS
            new Card { ImageUrl="/images/cards/diamonds_A.png", Value=new[] {1,11}},
            new Card { ImageUrl="/images/cards/diamonds_2.png", Value=new[] {2}},
            new Card { ImageUrl="/images/cards/diamonds_3.png", Value=new[] {3}},
            new Card { ImageUrl="/images/cards/diamonds_4.png", Value=new[] {4}},
            new Card { ImageUrl="/images/cards/diamonds_5.png", Value=new[] {5}},
            new Card { ImageUrl="/images/cards/diamonds_6.png", Value=new[] {6}},
            new Card { ImageUrl="/images/cards/diamonds_7.png", Value=new[] {7}},
            new Card { ImageUrl="/images/cards/diamonds_8.png", Value=new[] {8}},
            new Card { ImageUrl="/images/cards/diamonds_9.png", Value=new[] {9}},
            new Card { ImageUrl="/images/cards/diamonds_10.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/diamonds_J.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/diamonds_Q.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/diamonds_K.png", Value=new[] {10}},

            // HEARTS
            new Card { ImageUrl="/images/cards/hearts_A.png", Value=new[] {1,11}},
            new Card { ImageUrl="/images/cards/hearts_2.png", Value=new[] {2}},
            new Card { ImageUrl="/images/cards/hearts_3.png", Value=new[] {3}},
            new Card { ImageUrl="/images/cards/hearts_4.png", Value=new[] {4}},
            new Card { ImageUrl="/images/cards/hearts_5.png", Value=new[] {5}},
            new Card { ImageUrl="/images/cards/hearts_6.png", Value=new[] {6}},
            new Card { ImageUrl="/images/cards/hearts_7.png", Value=new[] {7}},
            new Card { ImageUrl="/images/cards/hearts_8.png", Value=new[] {8}},
            new Card { ImageUrl="/images/cards/hearts_9.png", Value=new[] {9}},
            new Card { ImageUrl="/images/cards/hearts_10.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/hearts_J.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/hearts_Q.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/hearts_K.png", Value=new[] {10}},

            // SPADES
            new Card { ImageUrl="/images/cards/spades_A.png", Value=new[] {1,11}},
            new Card { ImageUrl="/images/cards/spades_2.png", Value=new[] {2}},
            new Card { ImageUrl="/images/cards/spades_3.png", Value=new[] {3}},
            new Card { ImageUrl="/images/cards/spades_4.png", Value=new[] {4}},
            new Card { ImageUrl="/images/cards/spades_5.png", Value=new[] {5}},
            new Card { ImageUrl="/images/cards/spades_6.png", Value=new[] {6}},
            new Card { ImageUrl="/images/cards/spades_7.png", Value=new[] {7}},
            new Card { ImageUrl="/images/cards/spades_8.png", Value=new[] {8}},
            new Card { ImageUrl="/images/cards/spades_9.png", Value=new[] {9}},
            new Card { ImageUrl="/images/cards/spades_10.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/spades_J.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/spades_Q.png", Value=new[] {10}},
            new Card { ImageUrl="/images/cards/spades_K.png", Value=new[] {10}},
        };

        private List<Card> _randomDeck;
        private int _credits = 500;
        private int _currentBet = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnNewGame_Clicked(object sender, RoutedEventArgs e)
        {
            StartNextGame();
        }

        private void StartNextGame()
        {
            List<Card> availableCards = new List<Card>(_deck);

            _randomDeck = new List<Card>();
            Random rng = new Random();

            do
            {
                int randomIndex = rng.Next(0, availableCards.Count);
                _randomDeck.Add(availableCards[randomIndex]);
                availableCards.RemoveAt(randomIndex);
            } while (availableCards.Count > 0);

            playerStack.Children.Clear();
            bankStack.Children.Clear();

            DealCardTo(playerStack);
            DealCardTo(playerStack, false);
            DealCardTo(bankStack);
            DealCardTo(bankStack, false);

            playerBetPanel.Visibility = Visibility.Visible;
            RefreshUI();
        }

        private void ShowAllImagesFromStackPanel(StackPanel panel)
        {
            //panel.Children is een lijst van objecten
            foreach (object control in panel.Children)
            {
                //Probeer elk object te casten naar een Image
                if (control is Image image)
                {
                    //De Tag-property van de Image bevat een Card-object
                    Card card = (Card)image.Tag;
                    card.IsVisible = true;
                    image.Source = new BitmapImage(new Uri(card.ImageUrl, UriKind.Relative));
                }
            }
        }

        /// <summary>
        /// Adds an image control to the panel which displays the given card
        /// </summary>
        /// <param name="panel">The control to which the image must be added</param>
        /// <param name="card">The card that should be displayed in the image</param>
        /// <param name="isVisible">A boolean that indicates if the card should be open or not</param>
        private void AddImageToStackPanel(StackPanel panel, Card card, bool isVisible)
        {
            card.IsVisible = isVisible;

            //Maak een nieuwe Image control
            Image image = new Image();
            image.Width = 120;
            image.Height = 170;
            image.Stretch = Stretch.Uniform;
            image.Margin = new Thickness(5, 0, 5, 0);
            //Bewaar het volledige Card-object in de Tag-property van de Image control
            image.Tag = card;
            image.Source = new BitmapImage(new Uri(card.ImageUrl, UriKind.Relative));

            //Voeg de Image control toe aan het StackPanel
            panel.Children.Add(image);
        }

        private void DealCardTo(StackPanel panel, bool isVisible = true)
        {
            AddImageToStackPanel(panel, _randomDeck[0], isVisible);
            _randomDeck.RemoveAt(0);
            RefreshUI();
        }

        private int GetTotalFromStack(StackPanel panel)
        {
            int total = 0;
            int aces = 0;

            //panel.Children is een lijst van objecten
            foreach (object control in panel.Children)
            {
                //Probeer elk object te casten naar een Image
                if (control is Image image)
                {
                    //De Tag-property van de Image bevat een Card-object
                    Card card = (Card)image.Tag;
                    if (card.IsVisible)
                    {
                        total += card.Value[0]; 

                        if(card.Value.Length > 1)
                        {
                            aces++;
                        }
                    }
                }
            }

            while(aces > 0 && total + 10 <= 21)
            {
                total += 10;
                aces--;
            }

            return total;
        }

        private void OnPlay_Clicked(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(playerBetTextBox.Text, out int result))
            {
                _currentBet = result;
                ShowAllImagesFromStackPanel(playerStack);
                actionsGrid.Visibility = Visibility.Visible;
                playerBetPanel.Visibility = Visibility.Hidden;
                RefreshUI();
            }
        }

        private void OnStop_Clicked(object sender, RoutedEventArgs e)
        {
            actionsGrid.Visibility = Visibility.Hidden;
            SimulateBank();
        }

        private void OnCard_Clicked(object sender, RoutedEventArgs e)
        {
            DealCardTo(playerStack);
            RefreshUI();

            if (GetTotalFromStack(playerStack) > 21)
            {
                actionsGrid.Visibility = Visibility.Hidden;
                SimulateBank();
            }
        }

        private void SimulateBank()
        {
            ShowAllImagesFromStackPanel(bankStack);

            while(GetTotalFromStack(bankStack) <= 16)
            {
                DealCardTo(bankStack);
            } 

            int playerTotal = GetTotalFromStack(playerStack);
            int bankTotal = GetTotalFromStack(bankStack);

            if ((playerTotal > 21 && bankTotal > 21)
                || (playerTotal == bankTotal))
            {
                //Nobody wins
                ShowDraw();
            }
            else if ((playerTotal > 21 && bankTotal <= 21) || (bankTotal > playerTotal && bankTotal <= 21))
            {
                //Bank wins
                _credits -= _currentBet;
                ShowDefeat();
            }
            else //(bankTotal > 21 || playerTotal > bankTotal)
            {
                //Player wins
                _credits += _currentBet;
                ShowVictory();
            }
        }

        private void RefreshUI()
        {
            creditsTextBlock.Text = _credits.ToString();
            playerPointsTextBlock.Text = GetTotalFromStack(playerStack).ToString();
            bankPointsTextBlock.Text = GetTotalFromStack(bankStack).ToString();
            remainingCardsTextBlock.Text = _randomDeck.Count.ToString();
        }

        private void OnNextGame_Clicked(object sender, RoutedEventArgs e)
        {
            resultGrid.Visibility = Visibility.Hidden;
            StartNextGame();
        }

        private void ShowVictory()
        {
            resultGrid.Visibility = Visibility.Visible;

            resultGrid.Background = new SolidColorBrush(Colors.DarkGreen)
            {
                Opacity = 0.5
            };
            resultTextBlock.Text = "You won!!!";
        }
    
        private void ShowDraw()
        {
            resultGrid.Visibility = Visibility.Visible;
            resultGrid.Background = new SolidColorBrush(Colors.DarkGray)
            {
                Opacity = 0.5
            };
            resultTextBlock.Text = "It's a draw...";
        }

        private void ShowDefeat()
        {
            resultGrid.Visibility = Visibility.Visible;
            resultGrid.Background = new SolidColorBrush(Colors.DarkRed)
            {
                Opacity = 0.5
            };
            resultTextBlock.Text = "You lost!!!";
        }

    }
}