using System.Windows;
using System.Windows.Controls;
using TrainScheduleButCooloer.models;

namespace TrainScheduleButCooloer.Interface
{
    public partial class ShipmentsTab : UserControl
    {
        private FreightContext db;

        public ShipmentsTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                ShipmentsDataGrid.ItemsSource = db.Shipments;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            db.SaveChangesShipments();
        }

        private void DeleteShipment(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Shipment shipment)
            {
                db.DeleteShipment(shipment);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            var filteredShipments = db.SearchShipments(searchTerm);
            ShipmentsDataGrid.ItemsSource = filteredShipments;
        }
    }
}