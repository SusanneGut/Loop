using System;
using System.Collections.Generic;

namespace Loop.Models.Entities
{
    public partial class Timestamp
    {
        public int Id { get; set; }
        public string Start { get; set; }
        public string Stop { get; set; }
        public int? ActivityId { get; set; }
        public string TotalTime { get; set; }

        public virtual Activity Activity { get; set; }
    }
}
