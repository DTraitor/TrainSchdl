using System.Windows;
using System.Windows.Controls;
using TrainScheduleButCooloer.models;

namespace TrainScheduleButCooloer.Interface
{
    public partial class StationsTab : UserControl
    {
        private FreightContext db;

        public StationsTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                StationsDataGrid.ItemsSource = db.Stations;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            db.SaveChangesStations();
        }

        private void DeleteStation(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Station station)
            {
                db.DeleteStation(station);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            var filteredStations = db.SearchStations(searchTerm);
            StationsDataGrid.ItemsSource = filteredStations;
        }
    }
}