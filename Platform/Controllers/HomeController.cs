using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis;
using MemberDatabase;
using Platform.Models;

namespace Platform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       
       
        public ActionResult Calendar()
        {
            ViewBag.Message = "Your Calendar page.";

            return View();
        }

     
    }
}