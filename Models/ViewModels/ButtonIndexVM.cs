﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
    public class ButtonIndexVM
    {
        [Display(Name = "Starttid")]
        public string Start { get; set; }

        [Display(Name = "Stopptid")]
        public string Stop { get; set; }
    }
}