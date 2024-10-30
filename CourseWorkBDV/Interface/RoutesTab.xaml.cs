using System.Windows;
using System.Windows.Controls;
using CourseWorkBDV.models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkBDV.Interface
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
                db.Routes.Load();
                db.Stations.Load();
                RoutesDataGrid.ItemsSource = db.Routes.Local.ToBindingList();
            }
        }

        public IEnumerable<Station> Stations => db.Stations.Local.ToBindingList();

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in RoutesDataGrid.Items)
            {
                if (item is Route route && db.Entry(route).State == EntityState.Added)
                {
                    db.Routes.Add(route);
                }
            }
            db.SaveChanges();
        }

        private void DeleteRoute(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Route route)
            {
                db.Routes.Remove(route);
                db.SaveChanges();
            }
        }
    }
}