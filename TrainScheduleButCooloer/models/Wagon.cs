using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainScheduleButCooloer.models
{
    public class Wagon
    {
        [Key]
        public int WagonID { get; set; }
        public string WagonType { get; set; }
        public decimal LoadCapacity { get; set; }
        public string CurrentStatus { get; set; }
        public int? AssignedTrainID { get; set; }

        [ForeignKey("AssignedTrainID")]
        public Train AssignedTrain { get; set; }
    }
}