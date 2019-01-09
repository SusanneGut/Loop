using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models.ViewModels
{
    public class MemberActivityVM
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public bool IsActive { get; set; }
        public MemberEditActivityVM EditActivityVM { get; set; }
        public TimeSpan Span { get; set; }
    }
}
