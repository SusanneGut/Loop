using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models.Entities;
using Loop.Models.ViewModels;

namespace Loop.Models
{
    public class MembersService
    {
        LoopContext context;

        public MembersService(LoopContext context)
        {
            this.context = context;
        }

        public async void AddActivity(MemberCreateVM activity)
        {
            await context
                .Activity
                .AddAsync(new Activity
                {
                    ActivityName = activity.ActivityName
                });
            await context.SaveChangesAsync();

        }

    }
}
