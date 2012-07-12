﻿using System.Web.Mvc;
using RealTimeWeb.Models;

namespace RealTimeWeb.Controllers
{
    public class NewsFeedController : Controller
    {
        public ActionResult Index(string format = null, int? delay = null)
        {
            var topStories = new NewsService().GetTopStories(delay);

            if (format == "partial" || Request.IsAjaxRequest())
                return PartialView("Stories", topStories);

            if (format == "json" || Request.ContentType.StartsWith("application/json"))
                return Json(topStories, JsonRequestBehavior.AllowGet);

            ViewBag.Title = "Top Stories";
            return View("Stories", topStories);
        }
    }
}
