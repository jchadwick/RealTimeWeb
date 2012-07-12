using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace RealTimeWeb.Models
{
    public class NewsService
    {
        public volatile static string LocalCachedFilepath;
        public volatile static string FeedSourceUri;

        public IEnumerable<NewsStory> GetTopStories(int? delay = null)
        {
            var stories =
                from element in GetRssFeed().Descendants("item")
                select new NewsStory
                           {
                               Title = element.Element("title").Value,
                               Link = element.Element("link").Value,
                               Description = element.Element("description").Value
                           };

            // Delay the response and make it look like
            // we took a lot longer to get the data
            Thread.Sleep(TimeSpan.FromSeconds(delay.GetValueOrDefault()));

            return stories;
        }


        public IEnumerable<NewsStory> GetTopStories(string delay)
        {
            int parsed = 0;
            if (!string.IsNullOrEmpty(delay) && int.TryParse(delay, out parsed))
                return GetTopStories(parsed);

            return GetTopStories();
        }

        private XDocument GetRssFeed()
        {
            if (!File.Exists(LocalCachedFilepath))
            {
                RefreshData();
            }

            return XDocument.Load(LocalCachedFilepath);
        }

        public void RefreshData()
        {
            try
            {
                var feed = XDocument.Load(FeedSourceUri);
                // "Cache" a local copy
                feed.Save(LocalCachedFilepath);
                Trace.WriteLine("Saved RSS feed to " + LocalCachedFilepath);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error refreshing RSS Data: " + ex);
            }
        }
    }
}