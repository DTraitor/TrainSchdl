using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWorkBDV.models
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }
        public int TrainID { get; set; }
        public int RouteID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("TrainID")]
        public Train Train { get; set; }
        [ForeignKey("RouteID")]
        public Route Route { get; set; }
    }
}