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
        [Display(Prompt = "Activity name")]
        [Required(ErrorMessage = "Please write a name of the activity.")]
        public string ActivityName { get; set; }
		public string ActivityId { get; set; }
	}
}
