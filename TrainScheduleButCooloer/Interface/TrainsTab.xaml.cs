using System.Windows;
using System.Windows.Controls;
using TrainScheduleButCooloer.models;

namespace TrainScheduleButCooloer.Interface
{
    public partial class TrainsTab : UserControl
    {
        private FreightContext db;

        public TrainsTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                TrainsDataGrid.ItemsSource = db.Trains;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            db.SaveChangesTrains();
        }

        private void DeleteTrain(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Train train)
            {
                db.DeleteTrain(train);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            var filteredTrains = db.SearchTrains(searchTerm);
            TrainsDataGrid.ItemsSource = filteredTrains;
        }
    }
}