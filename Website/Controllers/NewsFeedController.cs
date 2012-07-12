using System.Web.Mvc;
using RealTimeWeb.Models;

namespace RealTimeWeb.Controllers
{
    public class NewsFeedController : Controller
    {
        public ActionResult Index(int? delay = null)
        {
            var topStories = new NewsService().GetTopStories(delay);
            return View("Stories", topStories);
        }
    }
}
