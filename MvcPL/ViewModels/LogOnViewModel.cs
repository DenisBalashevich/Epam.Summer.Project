using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.ViewModels
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "Enter your e-mail")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Uncorrect Email")]
        public string Email { get; set; }

        [MaxLength(50)]
        [Display(Name = "Enter your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Display(Name = "Remember password?")]
        public bool RememberMe { get; set; }
    }
}