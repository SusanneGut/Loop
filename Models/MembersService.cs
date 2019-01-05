using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models.Entities;
using Loop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Loop.Models
{
    public class MembersService
    {
        LoopContext context;
		UserManager<IdentityUser> userManager;


		public MembersService(LoopContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
			this.userManager = userManager;
        }

        public async Task AddActivity(MemberCreateActivityVM activity)
        {
            await context
                .Activity
                .AddAsync(new Activity
                {
                    ActivityName = activity.ActivityName,
                });

            await context.SaveChangesAsync();
        }

		public async Task<MemberActivitiesVM[]> GetAllActivities()
		{
			return await context
				.Activity
				.Select(o => new MemberActivitiesVM
				{
					Id = o.Id,
					ActivityName = o.ActivityName,
				})
				.OrderBy(p => p.ActivityName)
				.ToArrayAsync();
		}



		public async Task<MemberActivitiesVM> GetActivityByIdAsync(int Id)
		{
			return await context
				.Activity
				.Select(o => new MemberActivitiesVM
				{
					ActivityName = o.ActivityName,
					ActivityId = o.Id,
					
				})
				.SingleOrDefaultAsync(e => e.ActivityId == Id);
		}

		public async Task<MemberEditActivityVM> GetActivityEditAsync(int id)
		{
			return await context
				.Activity
				.Select(o => new MemberEditActivityVM
				{
					ActivityName = o.ActivityName,
					Id = o.Id,

				})
				.SingleOrDefaultAsync(e => e.Id == id);
		}


		public async Task EditActivityAsync(MemberEditActivityVM input)
		{
			var a = await context.Activity.FindAsync(input.Id);

			a.ActivityName = input.ActivityName;

			await context.SaveChangesAsync();
		}
    }
}

