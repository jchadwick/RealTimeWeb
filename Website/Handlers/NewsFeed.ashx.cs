using System.Web;
using Newtonsoft.Json;
using RealTimeWeb.Models;

namespace RealTimeWeb.Handlers
{
    public class NewsFeed : IHttpHandler
    {
        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            int last;
            int.TryParse(context.Request["last"], out last);

            var stories = new NewsService().GetTopStories(last, context.Request["delay"]);

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.Write(JsonConvert.SerializeObject(stories));
        }
    }
}