using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CV_Project2022.Models
{
    public class CvModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Programming Skills")]
        public List<SelectListItem> Skills { get; set; } 

        [Required]
        [Display(Name = "Upload your photo")]
        public IFormFile Photo { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Compare("Email")]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        public int Grade { get; set; }

        [Required]
        //[Range(1,20, ErrorMessage = "First Nb : Only numbers between 1 and 20 are allowed")]
        public int FirstNb { get; set; }

        [Required]
        //[Range(20, 50, ErrorMessage = "First Nb : Only numbers between 20 and 50 are allowed")]
        public int SecondNb { get; set; }

        [Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Summation : Only positive numbers are allowed")]
        public int Summation { get; set; }
    }
}
