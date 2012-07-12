using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using RealTimeWeb.Models;

namespace RealTimeWeb.Controllers
{
    public class NewsFeedController : AsyncController
    {
/*
        // Non-Async Action
        public ActionResult Index(int last = 0, int? delay = null)
        {
            var topStories = new NewsService().GetTopStories(last, delay);
            return PartialView("Stories", topStories);
        }
*/

        // Async Action
        public Task<ActionResult> Index(int last = 0, int? delay = null)
        {
            return Task.Factory.StartNew<ActionResult>(() => {
                var topStories = new NewsService().GetTopStories(last, delay);
                return PartialView("Stories", topStories);
            });
        }

        public Task<ActionResult> LongPoll(int last = 0, int? delay = null)
        {
            return Task.Factory.StartNew<ActionResult>(() => {
                var service = new NewsService();
                IEnumerable<NewsStory> topStories;

                do
                {
                    topStories = service.GetTopStories(last, delay);
                } while (!topStories.Any());

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
