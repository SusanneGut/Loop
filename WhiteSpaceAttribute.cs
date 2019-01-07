using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Loop
{
	public class WhiteSpaceAttribute: ValidationAttribute
	{
		Regex regex = new Regex(@"^\s");

		public override bool IsValid(object value)
		{
			return regex.IsMatch(value.ToString());

		}
	}
}
