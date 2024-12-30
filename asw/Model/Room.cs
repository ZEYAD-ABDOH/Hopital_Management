using asw.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asw.Model
{
    public class Room
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل الاسم الغرفة  ")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب  ادخال رقم اكبر من صفر")]
        public int Number_room { get; set; }
        [Required(ErrorMessage = "يجب عليك تحديد حالة  الغرفة  ")]
        public string Statues_Room { get; set; }

        public int DepartlID { get; set; }
        [ForeignKey("DepartlID")]
        public Departl? departl { get; set; }
    }
}
