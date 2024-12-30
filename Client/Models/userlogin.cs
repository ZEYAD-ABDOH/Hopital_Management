using System.ComponentModel.DataAnnotations;

namespace Client.Model
{
    public class userlogin
    {
        [Required(ErrorMessage = "يجب ادخال الاسم الرباعي")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "يجب ادخل كلمة المرور")]
        public string Password { get; set; }
    }
}
