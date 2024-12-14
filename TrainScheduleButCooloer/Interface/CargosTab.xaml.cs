using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TrainScheduleButCooloer.models;

namespace TrainScheduleButCooloer.Interface
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
                CargosDataGrid.ItemsSource = db.Cargos;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            db.SaveChangesCargo();
        }

        private void DeleteCargo(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Cargo cargo)
            {
                db.DeleteCargo(cargo);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            var filteredCargos = db.SearchCargos(searchTerm);
            CargosDataGrid.ItemsSource = filteredCargos;
        }
    }
}