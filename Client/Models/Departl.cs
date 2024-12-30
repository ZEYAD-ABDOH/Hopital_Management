using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Model
{
    public class Departl
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  اسم القسم  ")]
      
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "يجب  ادخال  القسم اكبر من 3 احرف   ")]
        public string Name_depart { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  موقع  القسم  ")]

        //[StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "يجب  ادخال  القسم اكبر من 3 احرف   ")]
        public int Location  { get; set; }
      

    }


}
