using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainScheduleButCooloer.models
{
    public class Cargo
    {
        [Key]
        public int CargoID { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public string Description { get; set; }
    }
}