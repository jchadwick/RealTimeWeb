using System.Threading.Tasks;
using System.Web.Mvc;
using RealTimeWeb.Models;

namespace RealTimeWeb.Controllers
{
    public class NewsFeedAsyncController : AsyncController
    {
        public Task<ActionResult> Index(int? delay = null)
        {
            return Task.Factory.StartNew<ActionResult>(() => {
                var topStories = new NewsService().GetTopStories(delay);
                return View("Stories", topStories);
            });
        }
    }
}
