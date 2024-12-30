using asw.Model;
using Microsoft.AspNetCore.Http.Timeouts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asw.Model
{
    public class User
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="يجب عليك ادخل الاسم الرباعي ")]
        [StringLength(maximumLength:50,MinimumLength =12 ,ErrorMessage = "يجب ادخال اسمك بكامل و يجب ان يكون ما بين 12 الى50")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  كلمة السر ")]
       
        [RegularExpression(@"^(?=.*\d)(?=.*[\W_])(?=.*[a-zA-Z]).{8,}$",ErrorMessage ="كلمة السر يجب ان تحتوي على حروف و ارقام و رموز ")]
        public string Password { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل الصلاحيات ")]
        [Range(1,4 ,ErrorMessage ="صلاحة المستخدم يجب ان يكون بين 1الى4")]
        public string Role { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  رقم الموبايل ")]
        [DataType(DataType.PhoneNumber)]
      

        public string Phone { get; set; }

        [Required(ErrorMessage = "يجب عليك ادخل  العنوان بشكل كامل ")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "يجب ادخال يجب عليك ادخل  العنوان بشكل كامل")]


        public string Address { get; set; }

        [Required(ErrorMessage = "يجب عليك ادخل  الجنس ")]
        public string Gender { get; set; }

        pu[RegularExpression(@"\w+@\w+.\w+",ErrorMessage = "الايميل غير صحيح")]
        public string Email { get; set; }

        [Required(ErrorMessage = "يجب عليك ادخل  الايميل ")]
        [DataType(DataType.EmailAddress,ErrorMessage ="الايميل غير صحيح ")]
        
        public DateTime CreatedAt { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  تاريخ الانشاء ")]


        public int? DepartlID  { get; set; }
        [ForeignKey("DepartlID")]
        public Departl? departl { get; set; }


    }
}
