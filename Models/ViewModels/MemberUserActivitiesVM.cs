using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
	public class MemberUserActivitiesVM
	{
		public int ActivityId { get; set; }
		public string ActivityName { get; set; }
		public MemberActivityVM[] Activities { get; set; }

	}
}
