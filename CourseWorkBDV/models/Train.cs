using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWorkBDV.models
{
    public class Train
    {
        [Key]
        public int TrainID { get; set; }
        public string Name { get; set; }
        public decimal MaxCapacity { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}