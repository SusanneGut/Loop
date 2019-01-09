using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models;
using Loop.Models.Entities;
using Loop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Loop.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly MembersService service;
		UserManager<IdentityUser> userManager;

		public MemberController(MembersService service, UserManager<IdentityUser> userManager)
        {
            this.service = service;
			this.userManager = userManager;
        }

        //[HttpGet]
        //[Route("Member")]
        //public IActionResult Index()
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View(nameof(Index));
        //    }
        //    return View(new MemberIndexVM { Username = User.Identity.Name });
        //}

        [HttpGet]
        public IActionResult CreateActivity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(MemberCreateActivityVM activity)
        {
            if(!ModelState.IsValid)
            {
                return View(activity);
            }
			var user = await userManager.GetUserAsync(HttpContext.User);
			var id = user.Id;
			await service.AddActivity(activity, id);
            return RedirectToAction(nameof(Activities));
        }

        [HttpGet]
        public async Task<IActionResult> Activities()
        {
			var user = await userManager.GetUserAsync(HttpContext.User);
			var id = user.Id;
			return View(await service.GetAllActivities(id));
        }

        [HttpGet]
        [Route("member/activity/{Id}")]
        public async Task<IActionResult> Activity(int id)
        {
            return View(await service.GetActivityById(id));
        }

        [HttpGet]
        [Route("/member/editactivity/{Id}")]

        public async Task<IActionResult> EditActivity(int id)
        {
            return View(await service.GetActivityEditAsync(id));
        }

        [HttpPost]
        [Route("/member/editactivity/{Id}")]
        public async Task<IActionResult> EditActivity(MemberEditActivityVM activity)
        {
            await service.EditActivityAsync(activity);
            return RedirectToAction(nameof(Activities));
        }

        //[HttpGet]
        //[Route("/member/edit/{name}")]
        //public async Task<IActionResult> Edit(string name)
        //{
        //    return View(await service.GetUserByNameAsync(name));
        //}

        //[HttpPost]
        //[Route("/member/edit/{name}")]
        //public async Task<IActionResult> Edit(AccountEditUserVM User)
        //{
        //    await service.EditAsync(User);
        //    return RedirectToAction(nameof(Activities));
        //}




        //[Route("Members/SetStart/{id}")]
        //public async Task<IActionResult> SetStart(int id)
        //{
        //    await service.SetStart(DateTime.UtcNow.ToString(), id);
        //    return RedirectToAction(nameof(Activity));
        //}




        [HttpGet]
        public IActionResult SetStart()
        {
            return RedirectToAction(nameof(SetStart));
        }

        [HttpPost]
        public async Task<IActionResult> SetStart(int id)
        {
            await service.SetStart(DateTime.Now.ToUniversalTime().ToString(), id);
            return RedirectToAction(nameof(Activity));
        }

        [HttpPost]
        public async Task<IActionResult> SetStop(int id)
        {
            await service.SetStop(DateTime.Now.ToUniversalTime().ToString(), id);
            return RedirectToAction(nameof(Activity));
        }
    }
}