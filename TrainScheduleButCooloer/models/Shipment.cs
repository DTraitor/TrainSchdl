using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainScheduleButCooloer.models
{
    public class Shipment
    {
        [Key]
        public int ShipmentID { get; set; }
        public int OrderID { get; set; }
        public int TrainID { get; set; }
        public int WagonID { get; set; }
        public int StartStationID { get; set; }
        public int EndStationID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Status { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }
        [ForeignKey("TrainID")]
        public Train Train { get; set; }
        [ForeignKey("WagonID")]
        public Wagon Wagon { get; set; }
        [ForeignKey("StartStationID")]
        public Station StartStation { get; set; }
        [ForeignKey("EndStationID")]
        public Station EndStation { get; set; }
    }
}