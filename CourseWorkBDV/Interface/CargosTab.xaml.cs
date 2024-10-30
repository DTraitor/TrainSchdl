using System.Windows;
using System.Windows.Controls;
using CourseWorkBDV.models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkBDV.Interface
{
    public partial class CargosTab : UserControl
    {
        private FreightContext db;

        public CargosTab()
        {
            InitializeComponent();
        }

        public FreightContext DbContext
        {
            get => db;
            set
            {
                db = value;
                CargosDataGrid.ItemsSource = db.Cargos.Local.ToBindingList();
                db.Cargos.Load();
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in CargosDataGrid.Items)
            {
                if (item is Cargo cargo && db.Entry(cargo).State == EntityState.Added)
                {
                    db.Cargos.Add(cargo);
                }
            }
            db.SaveChanges();
        }

        private void DeleteCargo(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Cargo cargo)
            {
                db.Cargos.Remove(cargo);
                db.SaveChanges();
            }
        }
    }
}