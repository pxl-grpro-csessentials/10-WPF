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

namespace EventPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] _eventTypes = { "Festival", "Orkest", "Opera" };
        struct _eventObject
        {
            public string eventType;
            public string name;
            public int visitors;
        }
        readonly List<_eventObject> _events = new List<_eventObject>();

        public MainWindow()
        {
            InitializeComponent();
            FillTypes();
        }

        private void FillTypes()
        {
            foreach (string eventType in _eventTypes)
            {
                eventTypeComboBox.Items.Add(eventType);
            }
        }

        private void delete_MenuItem(object sender, RoutedEventArgs e)
        {
            itemsListBox.Items.Clear();
        }

        private void default_MenuItem(object sender, RoutedEventArgs e)
        {
            AddEvent("Orkest", "Jaarlijks optreden", 100);
        }

        private void close_MenuItem(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (itemsListBox.SelectedIndex > -1)
            {
                _events.RemoveAt(itemsListBox.SelectedIndex);
                itemsListBox.Items.RemoveAt(itemsListBox.SelectedIndex);
            }

            FillList();
        }

        private void AddEvent(string eventType, string naam, int aantalBezoekers)
        {
            _eventObject newEvent = new _eventObject();
            newEvent.eventType = eventType;
            newEvent.name = naam;
            newEvent.visitors = aantalBezoekers;
            _events.Add(newEvent);

            FillList();
        }

        private void FillList()
        {
            itemsListBox.Items.Clear();
            foreach (_eventObject evObject in _events)
            {
                itemsListBox.Items.Add($"{evObject.eventType} - {evObject.name}: {evObject.visitors}");
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddEvent(eventTypeComboBox.SelectedValue.ToString(), nameEventTextBox.Text, int.Parse(numberOfVisitorsTextBox.Text));
        }
    }
}