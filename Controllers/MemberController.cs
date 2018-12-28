﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models;
using Loop.Models.Entities;
using Loop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("Member")]
        public IActionResult Index()
        {
            if(!ModelState.IsValid)
            {
                return View(nameof(Index));
            }
            return View(new MemberIndexVM { Username = User.Identity.Name });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MemberCreateVM activity)
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
		public async Task<IActionResult> Activity(int Id)
		{
			return View(await service.GetActivityByIdAsync(Id));
		}

		[HttpGet]
		[Route("/member/edit/{name}")]

		public async Task<IActionResult> Edit(string User)
		{
			return View(await service.GetUserByNameAsync(User));
		}

		[HttpPost]
		public async Task<IActionResult> Edit(MemberEditVM User)
		{
			await service.EditAsync(User);
			return RedirectToAction(nameof(Edit));
		}
    }
}