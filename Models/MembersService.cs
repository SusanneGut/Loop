using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models.Entities;
using Loop.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Loop.Models
{
    public class MembersService
    {
        LoopContext context;

        public MembersService(LoopContext context)
        {
            this.context = context;
        }

        public async Task AddActivity(MemberCreateVM activity)
        {
            await context
                .Activity
                .AddAsync(new Activity
                {
                    ActivityName = activity.ActivityName
                });
            await context.SaveChangesAsync();
        }

        public async Task<MemberActivitiesVM[]> GetAllActivities()
        {
            return await context
                .Activity
                .Select(o => new MemberActivitiesVM
                {
                    ActivityName = o.ActivityName
                })
                .ToArrayAsync();
        }
    }
}
