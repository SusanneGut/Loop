using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models;
using Loop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Loop.Controllers
{
    public class AccountController : Controller
    {
        AccountService service;

		public AccountController(AccountService service)
        {
            this.service = service;
        }

		//[HttpGet]
		//public async Task<string> CurrentUser()
		//{
		//	IdentityUser user = await GetCurrentUserAsync();
		//	return user?.UserName;
		//}
		//private Task<IdentityUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

		[HttpGet]
		public IActionResult Details()
		{
			if (!ModelState.IsValid)
			{
				return View(nameof(Login));
			}
			return View(new AccountDetailsVM { UserName = User.Identity.Name });
		}

		//public IActionResult Index()
  //      {
  //          return View();
  //      }

        [HttpGet]
        [Route("")]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            var model = new AccountLoginVM
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(AccountLoginVM viewModel)
        {
            if(!ModelState.IsValid)
                return View(viewModel);

			if (!await service.TryLoginAsync(viewModel))
            {
                ModelState.AddModelError(nameof(AccountLoginVM.UserName), "Wrong username or password");
                return View(viewModel);
            }

            if(string.IsNullOrWhiteSpace(viewModel.ReturnUrl))
                return RedirectToAction(nameof(MemberController.Activities), "member");
            else
                return Redirect(viewModel.ReturnUrl);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Create(AccountCreateVM viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			if (!await service.AddMemberAsync(viewModel))
			{
				ModelState.AddModelError(nameof(AccountCreateVM.UserName), "Oups, something went wrong, try another username!");
				return View(viewModel);
			}
			
			return RedirectToAction(nameof(MemberController.Activities), "member");

		}

		//      [HttpPost]
		//      public async Task<IActionResult> Create(AccountCreateVM member)
		//      {
		//          if(!ModelState.IsValid)
		//              return View(nameof(Login));

		//          await service.AddMemberAsync(member);

		//	return RedirectToAction(nameof(MemberController.Activities),"member");

		//}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await service.LogOut();
			return RedirectToAction(nameof(Login));
		}

		[HttpGet]
		[Route("/account/edituser/{name}")]
		public async Task<IActionResult> EditUser(string name)
		{
			return View(await service.GetUserByNameAsync(name));
		}

		[HttpPost]
		[Route("/account/edituser/{name}")]
		public async Task<IActionResult> EditUser(AccountEditUserVM User)
		{
			await service.EditAsync(User);

			return RedirectToAction(nameof(MemberController.Activities),"member");
		}
	}
};