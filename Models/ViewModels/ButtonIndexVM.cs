using Loop.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
    public class ButtonIndexVM
    {
        public int Id { get; set; }

        [Display(Name = "Starttid")]
        public string Start { get; set; }

        [Display(Name = "Stopptid")]
        public string Stop { get; set; }

		public int ActivityId { get; set; }

		public string ActivityName { get; set; }

		public TimeSpan Span { get; set; }

	}
}
