using System.Windows;
using System.Windows.Controls;
using CourseWorkBDV.models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkBDV.Interface
{
    public partial class StationsTab : UserControl
    {
        private FreightContext db;

        public StationsTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                StationsDataGrid.ItemsSource = db.Stations.Local.ToBindingList();
                db.Stations.Load();
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in StationsDataGrid.Items)
            {
                if (item is Station station && db.Entry(station).State == EntityState.Added)
                {
                    db.Stations.Add(station);
                }
            }
            db.SaveChanges();
        }

        private void DeleteStation(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Station station)
            {
                db.Stations.Remove(station);
                db.SaveChanges();
            }
        }
    }
}