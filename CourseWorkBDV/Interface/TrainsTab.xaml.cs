using System.Windows;
using System.Windows.Controls;
using CourseWorkBDV.models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkBDV.Interface
{
    public partial class TrainsTab : UserControl
    {
        private FreightContext db;

        public TrainsTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                TrainsDataGrid.ItemsSource = db.Trains.Local.ToBindingList();
                db.Trains.Load();
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in TrainsDataGrid.Items)
            {
                if (item is Train train && db.Entry(train).State == EntityState.Added)
                {
                    db.Trains.Add(train);
                }
            }
            db.SaveChanges();
        }

        private void DeleteTrain(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Train train)
            {
                db.Trains.Remove(train);
                db.SaveChanges();
            }
        }
    }
}