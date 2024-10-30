using System.Windows;
using System.Windows.Controls;
using CourseWorkBDV.models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkBDV.Interface
{
    public partial class SchedulesTab : UserControl
    {
        private FreightContext db;

        public SchedulesTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                db.Schedules.Load();
                db.Trains.Load();
                db.Routes.Load();
                SchedulesDataGrid.ItemsSource = db.Schedules.Local.ToBindingList();
            }
        }

        public IEnumerable<Train> Trains => db.Trains.Local.ToBindingList();
        public IEnumerable<Route> Routes => db.Routes.Local.ToBindingList();

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in SchedulesDataGrid.Items)
            {
                if (item is Schedule schedule && db.Entry(schedule).State == EntityState.Added)
                {
                    db.Schedules.Add(schedule);
                }
            }
            db.SaveChanges();
        }

        private void DeleteSchedule(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Schedule schedule)
            {
                db.Schedules.Remove(schedule);
                db.SaveChanges();
            }
        }
    }
}