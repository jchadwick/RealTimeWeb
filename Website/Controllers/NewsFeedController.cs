using System.Threading.Tasks;
using System.Web.Mvc;
using RealTimeWeb.Models;

namespace RealTimeWeb.Controllers
{
    public class NewsFeedController : AsyncController
    {
/*
        // Non-Async Action
        public ActionResult Index()
        {
            var topStories = new NewsService().GetTopStories(last, delay);
            return PartialView("Stories", topStories);
        }
*/

        // Async Action
        public Task<ActionResult> Index()
        {
            return Task.Factory.StartNew<ActionResult>(() => {

                var topStories = new NewsService().GetTopStories();
                return PartialView("Stories", topStories);

            });
        }


        public ActionResult Add(NewsStory story = null)
        {
            if (story == null || string.IsNullOrEmpty(story.Title))
                story = new NewsService().AddStory();
            else
                new NewsService().AddStory(story);

            return Json(story, JsonRequestBehavior.AllowGet);
        }
    }
}
