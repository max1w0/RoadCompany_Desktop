using RoadCompany.Classes;
using System;
using System.Windows;

namespace RoadCompany.UI.Views
{
    public partial class AddEventDialog : Window
    {
        public EventData EventData { get; private set; }
        private int _employeeId;

        public AddEventDialog(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            EventData = new EventData
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            DataContext = EventData;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EventData.EventType))
            {
                MessageBox.Show("Please select an event type", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (EventData.StartDate > EventData.EndDate)
            {
                MessageBox.Show("End date cannot be earlier than start date", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(EventData.Description))
            {
                EventData.Description = EventData.EventType;
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
