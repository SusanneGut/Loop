using System;
using System.Collections.Generic;

namespace Loop.Models.Entities
{
    public partial class Activity
    {
        public Activity()
        {
            Timestamp = new HashSet<Timestamp>();
        }

        public int Id { get; set; }
        public string ActivityName { get; set; }

        public virtual ICollection<Timestamp> Timestamp { get; set; }
	}
}
