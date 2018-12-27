﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
	public class MemberEditVM
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Enter username")]
		public string Name { get; set; }

		[Display(Name = "E-mail")]
		[Required(ErrorMessage = "Enter E-mail")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}