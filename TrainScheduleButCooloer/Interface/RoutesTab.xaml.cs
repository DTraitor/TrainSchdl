using System.Windows;
using System.Windows.Controls;
using TrainScheduleButCooloer.models;

namespace TrainScheduleButCooloer.Interface
{
    public partial class RoutesTab : UserControl
    {
        private FreightContext db;

        public RoutesTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                RoutesDataGrid.ItemsSource = db.Routes;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            db.SaveChangesRoutes();
        }

        private void DeleteRoute(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Route route)
            {
                db.DeleteRoute(route);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            var filteredRoutes = db.SearchRoutes(searchTerm);
            RoutesDataGrid.ItemsSource = filteredRoutes;
        }
    }
}