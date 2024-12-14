using System.Windows;
using System.Windows.Controls;
using TrainScheduleButCooloer.models;

namespace TrainScheduleButCooloer.Interface
{
    public partial class WagonsTab : UserControl
    {
        private FreightContext db;

        public WagonsTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                WagonsDataGrid.ItemsSource = db.Wagons;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            db.SaveChangesWagons();
        }

        private void DeleteWagon(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Wagon wagon)
            {
                db.DeleteWagon(wagon);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            var filteredWagons = db.SearchWagons(searchTerm);
            WagonsDataGrid.ItemsSource = filteredWagons;
        }
    }
}