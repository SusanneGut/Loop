using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loop.Models;
using Loop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loop.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
		MembersService service;
		public MemberController(MembersService service)
		{
			this.service = service;
		}

        [HttpGet]
        [Route("Member")]
        public IActionResult Index()
        {
            return View(new MemberIndexVM { Username = User.Identity.Name });
        }

		
	}
}