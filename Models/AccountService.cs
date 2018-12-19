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
		IdentityDbContext identityContext;
		UserManager<IdentityUser> userManager;
		SignInManager<IdentityUser> signInManager;

		public AccountService(
			IdentityDbContext identityContext,
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager
			)
		{
			this.identityContext = identityContext;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public async Task<bool> TryLoginAsync(AccountLoginVM viewModel)
		{
			// Create DB schema (first time)
			var createSchemaResult = await identityContext.Database.EnsureCreatedAsync();

			// Create a hard coded user (first time)
			var createResult = await userManager.CreateAsync(new IdentityUser("user"), "Password_123");

			var loginResult = await signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
			return loginResult.Succeeded;
		}
	}
}
