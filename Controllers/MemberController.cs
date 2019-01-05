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

        public MemberController(MembersService service)
        {
            this.service = service;
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
            await service.AddActivity(activity);
            return RedirectToAction(nameof(Activities));
        }

        [HttpGet]
        public async Task<IActionResult> Activities()
        {
            return View(await service.GetAllActivities());
        }

		[HttpGet]
		[Route("/member/activity/{Id}")]
		public async Task<IActionResult> Activity(int id)
		{
			return View(await service.GetActivityByIdAsync(id));
		}

		[HttpGet]
		[Route("/member/editactivity/{Id}")]

		public async Task <IActionResult> EditActivity(int id)
		{
			return View(await service.GetActivityEditAsync(id));
		}

		[HttpPost]
		[Route("/member/editactivity/{Id}")]
		public async Task <IActionResult> EditActivity(MemberEditActivityVM activity)
		{
			await service.EditActivityAsync(activity);
			return RedirectToAction(nameof(Activities));
		}

		
    }
}