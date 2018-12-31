using Loop.Models.Entities;
using Loop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loop.Models
{
	public class AccountService
	{
        private readonly IdentityDbContext identityContext;
        private readonly LoopContext loopContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountService(
            IdentityDbContext identityContext,
            LoopContext loopContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            this.identityContext = identityContext;
            this.loopContext = loopContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

		public async Task<bool> TryLoginAsync(AccountLoginVM viewModel)
		{
			//// Create DB schema (first time)
			//var createSchemaResult = await identityContext.Database.EnsureCreatedAsync();

			//// Create a hard coded user (first time)
			//var createResult = await userManager.CreateAsync(new IdentityUser("user"), "Password_123");

			var loginResult = await signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
			return loginResult.Succeeded;
		}

		public async Task AddMemberAsync(AccountCreateVM member)
		{
			var user = new IdentityUser { UserName = member.Name, Email = member.Email };

			await userManager.CreateAsync(user,member.Password);
			await loopContext.SaveChangesAsync();
		}
	}



}
