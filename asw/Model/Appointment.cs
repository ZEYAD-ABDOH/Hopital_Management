
using asw.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asw.Model
{
    public class Appointment
    {
        public int ID { get; set; }
        public  DateTime AppointmentDate  { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل الصلاحية   ")]
        [Range(1,3, ErrorMessage = " صلاحية يجب  ان تكون بين 1 الى 2")]
        public  string Status { get; set; }
        [Required(ErrorMessage = "يجب ادخال ملاحظة عن المريض  او الموعد   ")]
        [StringLength(maximumLength: 100, MinimumLength = 50, ErrorMessage = "يجب  ادخال كتابة ملاحظة   ")]

        public string Notes { get; set; }
        public int DoctorID { get; set; }
        [ForeignKey("DoctorID")]
        public Doctor? doctor { get; set; }

        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public Patient? patient { get; set; }

    }
}
