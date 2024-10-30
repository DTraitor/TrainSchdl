using System.Windows;
using System.Windows.Controls;
using CourseWorkBDV.models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkBDV.Interface
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
                db.Clients.Load();
                ClientsDataGrid.ItemsSource = db.Clients.Local.ToBindingList();
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in ClientsDataGrid.Items)
            {
                if (item is Client client && db.Entry(client).State == EntityState.Added)
                {
                    db.Clients.Add(client);
                }
            }
            db.SaveChanges();
        }

        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Client client)
            {
                db.Clients.Remove(client);
            }
            db.SaveChanges();
        }
    }
}