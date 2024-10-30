using System.Windows;
using System.Windows.Controls;
using CourseWorkBDV.models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkBDV.Interface
{
    public partial class OrdersTab : UserControl
    {
        private FreightContext db;

        public OrdersTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                db.Orders.Load();
                db.Clients.Load();
                db.Cargos.Load();
                db.Routes.Load();
                OrdersDataGrid.ItemsSource = db.Orders.Local.ToBindingList();
            }
        }

        public IEnumerable<Client> Clients => db.Clients.Local.ToBindingList();
        public IEnumerable<Cargo> Cargos => db.Cargos.Local.ToBindingList();
        public IEnumerable<Route> Routes => db.Routes.Local.ToBindingList();

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in OrdersDataGrid.Items)
            {
                if (item is Order order && db.Entry(order).State == EntityState.Added)
                {
                    db.Orders.Add(order);
                }
            }
            db.SaveChanges();
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Order order)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }
        }
    }
}