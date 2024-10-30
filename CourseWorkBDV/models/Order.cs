using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWorkBDV.models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int ClientID { get; set; }
        public int CargoID { get; set; }
        public int RouteID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalCost { get; set; }

        [ForeignKey("ClientID")]
        public Client Client { get; set; }
        [ForeignKey("CargoID")]
        public Cargo Cargo { get; set; }
        [ForeignKey("RouteID")]
        public Route Route { get; set; }
    }
}