using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
    public class GuestIndexVM
    {
        public DateTime Time { get; set; }

        public DateTime Start { get; set; }

        public DateTime Stop { get; set; }
    }
}
