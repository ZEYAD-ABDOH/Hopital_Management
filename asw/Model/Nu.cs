using asw.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asw.Model
{
    public class Nu
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل الاسم الرباعي ")]
        [StringLength(maximumLength: 50, MinimumLength = 12, ErrorMessage = "يجب ادخال اسمك بكامل و يجب ان يكون ما بين 12 الى50")]
        public string Name { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  رقم الموبايل ")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  العنوان بشكل كامل ")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "يجب ادخال يجب عليك ادخل  العنوان بشكل كامل")]

        public string Address { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  الجنس ")]

        public string Gender { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  الايميل ")]
        [DataType(DataType.EmailAddress, ErrorMessage = "الايميل غير صحيح ")]
        [RegularExpression(@"\w+@\w+.\w+", ErrorMessage = "الايميل غير صحيح")]
        public string Email { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  تاريخ الانشاء ")]
        public DateTime CreatedAt { get; set; }
        public int DepartlID { get; set; }
        [ForeignKey("DepartlID")]
        public Departl? departl { get; set; }
    }
}
