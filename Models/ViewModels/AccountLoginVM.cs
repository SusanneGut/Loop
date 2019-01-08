using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
    public class AccountLoginVM
    {
        [Display(Prompt = "Username")]
        [Required]
        public string Username { get; set; }

        [Display(Prompt = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
