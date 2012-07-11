using System.Threading.Tasks;
using System.Web.Mvc;
using RealTimeWeb.Models;

namespace RealTimeWeb.Controllers
{
    // http://msdn.microsoft.com/en-us/library/ee728598.aspx
    public class NewsFeedController : AsyncController
    {
        public ActionResult TopStories()
        {
            return GetTopStoriesResult();
        }

        public async Task<ActionResult> TopStoriesWithAsync()
        {
            var service = new NewsService();
            await service.GetTopStoriesAsync();
        }

        private ActionResult GetTopStoriesResult()
        {
            var service = new NewsService();
            var topStories = await service.GetTopStoriesAsync();

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
