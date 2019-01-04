//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Loop.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Loop.Controllers
//{
//    public class GuestController : Controller
//    {
//        GuestsService service;

//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult Start()
//        {
//            return View();
//        }

//		[HttpPost]
//		public IActionResult Start(DateTime time)
//		{
//			service.Test();
//			return View(nameof(Start));
//		}

//		//return View(new GuestIndexVM{Start = Start.DateTime.Now})
//		//Update?;
//	}
//}