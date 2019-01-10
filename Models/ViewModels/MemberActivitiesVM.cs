using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models.Entities;

namespace Loop.Models.ViewModels
{
    public class MemberActivitiesVM
    {
		public string UserId { get; set; }
		public MemberActivityVM[] Activities { get; set; }
        public MemberCreateActivityVM CreateActivity { get; set; }
    }
}
