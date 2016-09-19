using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class EditUserInformation
    {
        [Display(Name = "Enter your age")]
        [Range(0, 100, ErrorMessage = "Age must be positive, less than 100")]
        public int Age { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Enter your FirstName")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Enter your SecondName")]
        public string LastName { get; set; }
    }
}