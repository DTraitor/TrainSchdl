using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TrainScheduleButCooloer.models;

namespace TrainScheduleButCooloer.Interface
{
    public partial class ClientsTab : UserControl
    {
        private FreightContext db;

        public ClientsTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                ClientsDataGrid.ItemsSource = db.Clients;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            db.SaveChangesClients();
            if (ClientsDataGrid.ItemsSource != db.Clients)
            {
                string searchTerm = SearchTextBox.Text.ToLower();
                var filteredClients = db.SearchClients(searchTerm);
                ClientsDataGrid.ItemsSource = filteredClients;
            }
        }

        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Client client)
            {
                db.DeleteClient(client);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            var filteredClients = db.SearchClients(searchTerm);
            ClientsDataGrid.ItemsSource = filteredClients;
        }
    }
}