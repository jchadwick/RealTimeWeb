using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using RealTimeWeb.Models;

namespace RealTimeWeb.Handlers
{
    public class NewsFeedLongPolling : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            int last;
            int.TryParse(context.Request["last"], out last);

            context.Response.Clear();
            context.Response.ContentType = "application/json";

            var service = new NewsService();
            IEnumerable<NewsStory> stories;

            do
            {
                stories = service.GetTopStories(last);
                Thread.Sleep(100);
            } while (!stories.Any());

            context.Response.Write(JsonConvert.SerializeObject(stories));
        }
    }
}