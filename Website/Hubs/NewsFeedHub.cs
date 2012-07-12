using System.Collections.Generic;
using System.Threading.Tasks;
using SignalR.Hubs;

namespace RealTimeWeb.Hubs
{
    public class NewsFeedHub : Hub
    {
    }

/*
    // With connection notifications!
    public class NewsFeedHub : Hub, IConnected
    {
        public Task Connect()
        {
            return Clients.connect(Context.ConnectionId);
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            return Clients.reconnect(Context.ConnectionId);
        }
    }
 */
}