﻿using System.ComponentModel.DataAnnotations;

namespace TrainScheduleButCooloer.models
{
    public class Station
    {
        [Key]
        public int StationID { get; set; }
        public string StationName { get; set; }
        public string Location { get; set; }
    }
}