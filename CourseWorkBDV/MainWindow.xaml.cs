using System.Windows;
using CourseWorkBDV.Interface;
using CourseWorkBDV.models;

namespace CourseWorkBDV
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