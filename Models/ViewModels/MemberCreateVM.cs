using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
    //Vymodell för Activity
    public class MemberCreateVM
    {
        [Display(Name = "Activity name")]
        [Required(ErrorMessage = "Please write the name of activity.")]
        public string ActivityName { get; set; }
    }
}
