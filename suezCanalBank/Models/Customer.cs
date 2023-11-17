//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace suezCanalBank.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Nationality { get; set; }

        [Display(Name = "Birth Date")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Gender { get; set; }
        public string Mobile { get; set; }

        [Display(Name = "Birth Place")]
        public string BirthPlace { get; set; }

        [Required(ErrorMessage = "This field is required")]

        [Display(Name = "Resident Address")]
        public string ResidentAddress { get; set; }
        public string occupation { get; set; }




        [Display(Name = "Customer Type")]
        public string CustomerType { get; set; }

        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        [Display(Name = "National Id")]

        public string NationalId { get; set; }


        public bool suspiciousUsers { get; set; }
    }
}