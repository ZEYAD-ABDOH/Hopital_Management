﻿using System.ComponentModel.DataAnnotations;

namespace asw.Model
{
    public class Patient
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل الاسم الرباعي ")]
        [StringLength(maximumLength: 50, MinimumLength = 12, ErrorMessage = "يجب ادخال اسمك بكامل و يجب ان يكون ما بين 12 الى50")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  الجنس ")]

        public string Gender { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  رقم الموبايل ")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string MedicalHistory { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  العنوان بشكل كامل ")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "يجب ادخال يجب عليك ادخل  العنوان بشكل كامل")]

        public string Address { get; set; }
        [Required(ErrorMessage = "يجب عليك ادخل  تاريخ الميلاد ")]
        [DataType(DataType.Date)]

        public DateTime DateOfBirth { get; set; }

    }
}
