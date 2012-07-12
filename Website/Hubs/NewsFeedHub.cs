using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalR.Hubs;

namespace RealTimeWeb.Hubs
{
    public class NewsFeedHub : Hub
    {
    }

    // With connection notifications!
/*
    public class NewsFeedHub : Hub, IConnected
    {
        public Task Connect()
        {
            // Send the initial batch of stories
            var stories = new Models.NewsService().GetTopStories();
            Caller.onNewStories(stories.OrderBy(x => x.Id));

            return Clients.connect(Context.ConnectionId);
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            return Clients.reconnect(Context.ConnectionId);
        }
    }
*/
}