using System.ComponentModel.DataAnnotations;

namespace NDBBlesson12.Models
{
    public class Major
    {
        public int MajorID { get; set; }

        [Display(Name = "Ngành")]
        public string MajorName { get; set; } = string.Empty;

        public virtual ICollection<Learner> Learners { get; set; } = new List<Learner>();
    }
}
