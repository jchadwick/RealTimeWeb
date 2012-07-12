using System;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RealTimeWeb.Models;

namespace RealTimeWeb.Handlers
{
    public class NewsFeedAsync : IHttpAsyncHandler //, HttpTaskAsyncHandler -- New in .NET 4.5
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            return Task.Factory.StartNew(() => {
                var stories = new NewsService().GetTopStories();

                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.Write(JsonConvert.SerializeObject(stories));
            });
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            ((Task)result).Wait();
        }

        public void ProcessRequest(HttpContext context)
        {
            // This is never called
            throw new InvalidOperationException();
        }
    }
}