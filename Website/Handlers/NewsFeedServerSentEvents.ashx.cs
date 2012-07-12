using System.Linq;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using RealTimeWeb.Models;

namespace RealTimeWeb.Handlers
{
    public class NewsFeedLongServerSentEvents : IHttpHandler
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
            context.Response.ContentType = "text/event-stream";

            var service = new NewsService();

            // Loop until the request is aborted!
            while(true)
            {
                var stories = service.GetTopStories(last);

                if(stories.Any())
                {
                    context.Response.Write(JsonConvert.SerializeObject(stories));
                    context.Response.Flush();
                }

                Thread.Sleep(100);
            }
        }
    }
}