using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using RealTimeWeb.Models;

namespace RealTimeWeb.Controllers
{
    public class NewsFeedAsyncController : AsyncController
    {
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

    }
}
