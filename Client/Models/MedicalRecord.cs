using asw.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Model
{
    public class MedicalRecord
    {
        public int ID { get; set; }

        [Required(ErrorMessage= "يجب عليك ادخال  تشخيص   المريض ")]
        [StringLength(maximumLength: 100, MinimumLength = 6, ErrorMessage = "يجب  ادخال  تشخيص   المريض")]

        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "يجب عليك ادخال الوصفه الطبية ")]
        [StringLength(maximumLength: 100, MinimumLength = 6, ErrorMessage = "يجب  ادخال  تشخيص   المريض")]
        public string Treatment { get; set; }
         
        [DataType(DataType.ImageUrl)]
        [RegularExpression(@"^.*\.(png|jpq| gif)$",ErrorMessage = " (png|jpq| gif) الامتاداد يجب ان يكون ")]
        public string Imag_ray { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  تاريخ زيارة ")]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }
        public int DoctorID { get; set; }
        [ForeignKey("DoctorID")]
        public Doctor? doctor { get; set; }

        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public Patient? patient { get; set; }
    }
}
