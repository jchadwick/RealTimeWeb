using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RealTimeWeb.Handlers
{
    public class ClockAsync : IHttpAsyncHandler
    {
        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
        }

        private static double UnixTicks(DateTime dt)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = dt.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return ts.TotalMilliseconds;
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            return Task.Factory.StartNew(() => {
                // Add a 50 ms delay to add latency
                Thread.Sleep(50);

                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.Write(UnixTicks(DateTime.Now));
            });
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            ((Task) result).Wait();
        }
    }
}