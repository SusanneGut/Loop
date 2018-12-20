using Loop.Models.Entities;
using Loop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models
{
	public class MembersService
	{
		LoopContext context;

		public MembersService(LoopContext context)
		{
			this.context = context;
		}
		public async Task AddMemberAsync(MemberCreateVM person)
		{
			context.AspNetUsers.Add(new AspNetUsers
			{
				UserName = person.Name,
				Email = person.Email
			});
			await context.SaveChangesAsync();
		}
	}
}
