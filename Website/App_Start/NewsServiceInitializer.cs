using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RealTimeWeb.Hubs;
using RealTimeWeb.Models;
using SignalR;

namespace RealTimeWeb.App_Start
{
    public class NewsServiceInitializer
    {
        public static void Initialize(Func<string,string> resolveUrl)
        {
            // Prime the cache
            NewsService.LocalCachedFilepath = resolveUrl("~/App_Data/top-stories.rss");
            NewsService.FeedSourceUri = "http://rss.cnn.com/rss/cnn_topstories.rss";
            new NewsService().ReloadData();

            Task.Factory.StartNew(ListenForNewStories);
        }

        private static void ListenForNewStories()
        {
            var service = new NewsService();
            
            int lastStory = 0;

            // Check for new stories in an infinite loop
            while(true)
            {
                var stories = service.GetTopStories(lastStory).ToArray();

                if(stories.Any())
                {
                    lastStory = stories.Max(x => x.Id);

                    var hub = GlobalHost.ConnectionManager.GetHubContext<NewsFeedHub>();
                    hub.Clients.onNewStories(stories);
                }

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}