using System;
using RealTimeWeb.Models;

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
        }
    }
}