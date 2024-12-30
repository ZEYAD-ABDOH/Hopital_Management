
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Model
{
    public class Invoice
    {

        public int ID { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "العلامة العشريية يجب ان تكون بين 1الى 2")]
        public double Total { get; set; }
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = " الارقام صحيحة فقط ")]

        public double Discount { get; set; }
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = " الارقام صحيحة فقط ")]

        public double Amount_paid { get; set; }

        [Range(0, 1000, ErrorMessage = "من 1 الى 1000")]
        public double Remain_amount { get; set; }
        [RegularExpression(@"^(أجل|مدفوع)$", ErrorMessage = " أجل | مدفوع")]


        public DateTime InvoiceDate { get; set; }
        public string PaymentStatus { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public Patient? patient { get; set; }
    }
}
