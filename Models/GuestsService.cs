using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models
{
    public class GuestsService
    {
        GuestsService service;
        List<DateTime> start = new List<DateTime>();

        public GuestsService(GuestsService service, List<DateTime> start)
        {
            this.service = service;
            this.start = start;
        }

        public void Start(DateTime time)
        {
            start.Add(time);
        }

    }
}
