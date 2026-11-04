using System.ComponentModel.DataAnnotations;

namespace NDBBlesson12.Models
{
    public class Learner
    {
        [Display(Name = "Mã")]
        public int LearnerID { get; set; }

        [Display(Name = "Họ")]
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string FirstMidName { get; set; } = string.Empty;

        [Display(Name = "Ngành")]
        [Required(ErrorMessage = "Vui lòng chọn ngành")]
        public int? MajorID { get; set; }

        [Display(Name = "Ngày Nhập Học")]
        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        public virtual Major? Major { get; set; }
    }
}
