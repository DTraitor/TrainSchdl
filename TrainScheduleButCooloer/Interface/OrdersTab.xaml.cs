using System.Windows;
using System.Windows.Controls;
using TrainScheduleButCooloer.models;

namespace TrainScheduleButCooloer.Interface
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
                OrdersDataGrid.ItemsSource = db.Orders;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            db.SaveChangesOrders();
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Order order)
            {
                db.DeleteOrder(order);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            var filteredOrders = db.SearchOrders(searchTerm);
            OrdersDataGrid.ItemsSource = filteredOrders;
        }
    }
}