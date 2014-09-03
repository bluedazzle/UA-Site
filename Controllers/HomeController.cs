using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test2.Models;

namespace test2.Controllers
{
    public class HomeController : Controller
    {
        public WebsiteDbContext context = new WebsiteDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Navigator(string controller)
        {
            ViewBag.Controller = controller;
            //ViewBag.ContentTypes = context.ContentTypes.ToList();
            return PartialView("_NavigatorPartial");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}