using System.Windows;
using System.Windows.Controls;
using CourseWorkBDV.models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkBDV.Interface
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
                db.Shipments.Load();
                db.Orders.Load();
                db.Trains.Load();
                db.Wagons.Load();
                db.Stations.Load();
                ShipmentsDataGrid.ItemsSource = db.Shipments.Local.ToBindingList();
            }
        }

        public IEnumerable<Order> Orders => db.Orders.Local.ToBindingList();
        public IEnumerable<Train> Trains => db.Trains.Local.ToBindingList();
        public IEnumerable<Wagon> Wagons => db.Wagons.Local.ToBindingList();
        public IEnumerable<Station> Stations => db.Stations.Local.ToBindingList();

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in ShipmentsDataGrid.Items)
            {
                if (item is Shipment shipment && db.Entry(shipment).State == EntityState.Added)
                {
                    db.Shipments.Add(shipment);
                }
            }
            db.SaveChanges();
        }

        private void DeleteShipment(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Shipment shipment)
            {
                db.Shipments.Remove(shipment);
                db.SaveChanges();
            }
        }
    }
}