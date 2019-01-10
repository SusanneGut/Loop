using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
	public class AccountEditUserVM
	{
		public int Id { get; set; }

		[Display(Prompt = "Username")]
		[Required(ErrorMessage = "Enter username")]
		public string UserName { get; set; }

		[Display(Prompt =  "E-mail")]
		[Required(ErrorMessage = "Enter E-mail")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		public string OldName { get; set; }
	}
}
