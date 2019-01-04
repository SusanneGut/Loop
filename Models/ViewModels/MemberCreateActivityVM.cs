using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
    //Vymodell för Activity
    public class MemberCreateActivityVM
    {
        [Display(Name = "Activity name")]
        [Required(ErrorMessage = "Please write the name of activity.")]
        public string ActivityName { get; set; }
		public int ActivityId { get; set; }
	}
}
