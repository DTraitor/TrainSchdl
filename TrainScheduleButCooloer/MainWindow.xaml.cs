using System.Windows;
using TrainScheduleButCooloer.Interface;
using TrainScheduleButCooloer.models;

namespace TrainScheduleButCooloer
{
    public partial class MainWindow : Window
    {
        private FreightContext db = new();

        public MainWindow()
        {
            InitializeComponent();
            ClientsTab.DbContext = db;
            TrainsTab.DbContext = db;
            CargosTab.DbContext = db;
            StationsTab.DbContext = db;
            WagonsTab.DbContext = db;
            RoutesTab.DbContext = db;
            SchedulesTab.DbContext = db;
            OrdersTab.DbContext = db;
            ShipmentsTab.DbContext = db;
        }
    }
}