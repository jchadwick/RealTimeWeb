using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace RealTimeWeb.Models
{
    public class NewsService
    {
        private static string LocalFilePath = ConfigurationManager.AppSettings["LocalFilePath"];
        private const string RemoteFilePath = "http://rss.cnn.com/rss/cnn_topstories.rss";

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

        private static XDocument GetRssFeed()
        {
            // "Cache" a local copy
            if (!File.Exists(LocalFilePath))
            {
                var feed = XDocument.Load(RemoteFilePath);
                feed.Save(LocalFilePath);
                Trace.WriteLine("Saved RSS feed to " + LocalFilePath);
                return feed;
            }

            return XDocument.Load(LocalFilePath);
        }
    }
}