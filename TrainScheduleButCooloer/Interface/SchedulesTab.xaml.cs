using System.Windows;
using System.Windows.Controls;
using TrainScheduleButCooloer.models;

namespace TrainScheduleButCooloer.Interface
{
    public partial class SchedulesTab : UserControl
    {
        private FreightContext db;

        public SchedulesTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                SchedulesDataGrid.ItemsSource = db.Schedules;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            db.SaveChangesSchedules();
        }

        private void DeleteSchedule(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Schedule schedule)
            {
                db.DeleteSchedule(schedule);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            var filteredSchedules = db.SearchSchedules(searchTerm);
            SchedulesDataGrid.ItemsSource = filteredSchedules;
        }
    }
}