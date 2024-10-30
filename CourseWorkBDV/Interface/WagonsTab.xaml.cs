using System.Windows;
using System.Windows.Controls;
using CourseWorkBDV.models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkBDV.Interface
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
                db.Wagons.Load();
                db.Trains.Load();
                WagonsDataGrid.ItemsSource = db.Wagons.Local.ToBindingList();
            }
        }

        public IEnumerable<Train> Trains => db.Trains.Local.ToBindingList();

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in WagonsDataGrid.Items)
            {
                if (item is Wagon wagon && db.Entry(wagon).State == EntityState.Added)
                {
                    db.Wagons.Add(wagon);
                }
            }
            db.SaveChanges();
        }

        private void DeleteWagon(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Wagon wagon)
            {
                db.Wagons.Remove(wagon);
                db.SaveChanges();
            }
        }
    }
}