using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using test2.Models;

namespace test2.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private NewsManager manager = new NewsManager(new WebsiteDbContext());
        //
        // GET: /News/
        [AllowAnonymous]
        public ActionResult Index(int id = 0)
        {
            return View(manager.GetNewsList(null, 10));
        }

        //
        // GET: /News/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            if (manager.HasNews(id))
                return View(manager.GetDetail(id));
            return RedirectToAction("Index");
        }

        //
        // GET: /News/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /News/Create
        [HttpPost]
        public ActionResult Create(InsertNewsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                    manager.CreateNews(model, User.Identity.GetUserId());
                else
                    return View();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /News/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /News/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /News/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /News/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
