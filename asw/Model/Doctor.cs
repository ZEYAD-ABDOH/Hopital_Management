using asw.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asw.Model
{
    public class Doctor
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل الاسم الرباعي ")]
        [StringLength(maximumLength: 50, MinimumLength = 12, ErrorMessage = "يجب ادخال اسمك بكامل و يجب ان يكون ما بين 12 الى50")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل سنوات الخبرة   ")]
        [Range(3, int.MaxValue, ErrorMessage = "يجب  ان تكون سنوات الخبر بكثر من 3")]
        public string Specialization  { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  رقم الموبايل ")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  ساعات العمل  ")]

        public string WorkHours { get; set; }
        public int DepartlID { get; set; }
        [ForeignKey("DepartlID")]
        public Departl? departl { get; set; }

    }
}
