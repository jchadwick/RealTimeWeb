using System.Threading.Tasks;
using System.Web.Mvc;
using RealTimeWeb.Models;

namespace RealTimeWeb.Controllers
{
    // http://msdn.microsoft.com/en-us/library/ee728598.aspx
    public class NewsFeedController : AsyncController
    {
        public ActionResult Index()
        {
            return RedirectToAction("TopStories");
        }

        public ActionResult TopStories()
        {
            return GetTopStoriesResult();
        }

        private ActionResult GetTopStoriesResult()
        {
            var service = new NewsService();
            var topStories = service.GetTopStories();

            var format = Request["format"];

            if (Request.IsAjaxRequest() || format == "partial")
                return PartialView("Stories", topStories);

            if (Request.ContentType.StartsWith("application/json")
                || format == "json")
                return Json(topStories, JsonRequestBehavior.AllowGet);

            ViewBag.Title = "Top Stories";
            return View("Stories", topStories);
        }
    }
}
