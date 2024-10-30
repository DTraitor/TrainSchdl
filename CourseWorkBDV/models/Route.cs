using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWorkBDV.models
{
    public class Route
    {
        [Key]
        public int RouteID { get; set; }
        public int OriginStationId { get; set; }
        public int DestinationStationId { get; set; }
        public decimal Distance { get; set; }
        public string EstimatedTime { get; set; }

        [ForeignKey("OriginStationId")]
        public Station OriginStation { get; set; }
        [ForeignKey("DestinationStationId")]
        public Station DestinationStation { get; set; }

        [NotMapped]
        public string RouteName => $"{RouteID} | {OriginStation.StationName} - {DestinationStation.StationName}";
    }
}