﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loop.Controllers
{
    public class ButtonController : Controller
    {
        ButtonService service;

        public ButtonController(ButtonService service)
        {
            this.service = service;
        }

        [Route("/Button")]
        public async Task<IActionResult> Index()
        {
            return View(await service.GetAllTimes());
        }

        public async Task<IActionResult> SetStart()
        {
            await service.SetStart(DateTime.Now.ToUniversalTime().ToString());
            //await service.SetStart(DateTime.Now.ToString());
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SetStop()
        {
            await service.SetStop(DateTime.Now.ToUniversalTime().ToString());
            //await service.SetStart(DateTime.Now.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}