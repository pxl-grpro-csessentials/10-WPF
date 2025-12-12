using Registratie.Models;
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

namespace Registratie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Student> _students = new List<Student>();
        private List<Olod> _olods = new List<Olod>();

        public MainWindow()
        {
            InitializeComponent();

            LoadOlodsInListBox();
            InitScreen();
        }

        private List<Olod> CreateOlodList()
        {
            return new List<Olod>()
            {
                new Olod() {
                    Name = "C# Essentials",
                    Credits = 7
                },
                new Olod() {
                    Name = "C# Advanced",
                    Credits = 6
                },
                new Olod() {
                    Name = "C# Web1",
                    Credits = 7
                },
                new Olod() {
                    Name = "C# Mobile",
                    Credits = 6
                },
                new Olod() {
                    Name = "C# Web2",
                    Credits = 4
                },
            };
        }

        private void LoadOlodsInListBox()
        {
            olodListBox.Items.Clear();
            foreach(Olod olod in CreateOlodList())
            {
                olodListBox.Items.Add(olod);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(!secundaryCheckBox.IsChecked.Value && !higherCheckBox.IsChecked.Value)
            {
                MessageBox.Show("Selecteer huidig diploma.");
                return;
            }

            if (olodListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecteer minstens 1 olod.");
                return;
            }

            Student newStudent = new Student()
            {
                Name = nameTextBox.Text,
                BirthDate = birthDatePicker.SelectedDate.Value,
            };

            if (mRadioButton.IsChecked.Value)
            {
                newStudent.Sex = 'M';
            }
            else if (vRadioButton.IsChecked.Value)
            {
                newStudent.Sex = 'F';
            }
            else if (xRadioButton.IsChecked.Value)
            {
                newStudent.Sex = 'X';
            }

            foreach(Olod olod in olodListBox.SelectedItems)
            {
                newStudent.SubscribeTo(olod);
            }

            studentComboxBox.Items.Add(newStudent);

            InitScreen();   
        }

        private void InitScreen()
        {
            nameTextBox.Clear();
            birthDatePicker.SelectedDate = null;
            mRadioButton.IsChecked = false;
            vRadioButton.IsChecked = false;
            xRadioButton.IsChecked = false;
            olodListBox.SelectedIndex = -1;
            secundaryCheckBox.IsChecked = false;
            higherCheckBox.IsChecked = false;

            nameTextBox.Focus();
        }

        private void studentComboxBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student student = studentComboxBox.SelectedItem as Student;

            if(student is not null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Naam: {student.Name}");
                sb.AppendLine($"Geboortedatum: {student.BirthDate.ToLongDateString()}");
                sb.AppendLine($"Sex: {student.Sex}");
                sb.AppendLine($"Olods: ");
                sb.AppendLine($"{student.GetOlodSummary()}");

                studentTextBlock.Text = sb.ToString();
            }
            else
            {
                studentTextBlock.Text = "";
            }
        }

        private void olodListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int total = 0;
            foreach (Olod olod in olodListBox.SelectedItems)
            {
                total += olod.Credits;
            }
            creditsTextBlock.Text = total.ToString();   
        }
    }
}