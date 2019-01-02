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

        public async Task AddActivity(MemberCreateVM activity)
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



		public async Task<MemberEditVM> GetUserByNameAsync(string UserName)
		{

			return await context
			.Activity
				.Select(o => new MemberEditVM
				{
					Name = o.User.UserName,
					Email = o.User.Email
				})
			   .SingleOrDefaultAsync(e => e.Name == UserName);
		}

		public async Task EditAsync(MemberEditVM User)
        {
			var user = userManager.Users.FirstOrDefault(u => u.UserName == User.Name);

		//var user = await GetUserByNameAsync(User.Name);
			user.UserName = User.Name;
            user.Email = User.Email;
            await userManager.UpdateAsync(user);
			await context.SaveChangesAsync();

        }
    }
}
