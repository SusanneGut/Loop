﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models;
using Loop.Models.ViewModels;
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

        public IActionResult Index()
        {
            return View();
        }

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
        [Route("logIn")]
        public async Task<IActionResult> Login(AccountLoginVM viewModel)
        {
            if(!ModelState.IsValid)
                return View(viewModel);

            // Check if credentials is valid (and set auth cookie)
            if(!await service.TryLoginAsync(viewModel))
            {
                // Show login error
                ModelState.AddModelError(nameof(AccountLoginVM.Username), "Invalid credentials");
                return View(viewModel);
            }

            // Redirect user
            //if (string.IsNullOrWhiteSpace(viewModel.ReturnUrl))
            //	return RedirectToAction(nameof(MembersController.Index), "members");
            //else
            return Redirect(viewModel.ReturnUrl);
        }
    }
}