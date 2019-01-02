using Loop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
    public class MemberActivitiesVM
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public int ActivityId { get; set; }
        public string Start { get; set; }
        public string Stop { get; set; }

        //public virtual ICollection<Timestamp> Timestamps { get; set; }
        public ButtonIndexVM MyProperty { get; set; }
    }
}
