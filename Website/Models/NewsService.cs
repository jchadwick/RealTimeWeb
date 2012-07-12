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
        private static readonly object fileLock = new object();

        public volatile static string LocalCachedFilepath;
        public volatile static string FeedSourceUri;

        public IEnumerable<NewsStory> GetTopStories(int lastStory = 0, int? delay = null)
        {
            var elements = GetRssFeed().Descendants("item");
            var index = 0;
            var stories =
                from element in elements.Reverse()
                select new NewsStory
                           {
                               Id = index += 1,
                               Title = element.Element("title").Value,
                               Link = element.Element("link").Value,
                               Description = element.Element("description").Value
                           };

            // Delay the response and make it look like
            // we took a lot longer to get the data
            Thread.Sleep(TimeSpan.FromSeconds(delay.GetValueOrDefault()));

            return stories.Where(x => x.Id > lastStory).OrderBy(x => x.Id);
        }


        public IEnumerable<NewsStory> GetTopStories(int lastStory, string delay)
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
                ReloadData();
            }

            lock (fileLock)
                return XDocument.Load(LocalCachedFilepath);
        }

        public void ReloadData()
        {
            try
            {
                // "Cache" a local copy
                var feed = XDocument.Load(FeedSourceUri);
                Save(feed);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error refreshing RSS Data: " + ex);
            }
        }

        private static void Save(XDocument feed)
        {
            feed.Save(LocalCachedFilepath);
            Trace.WriteLine("Saved RSS feed to " + LocalCachedFilepath);
        }

        public NewsStory AddStory(NewsStory story = null)
        {
            story = story ?? GenerateStory();

            var feed = GetRssFeed();
            var channel = feed.Descendants("channel").FirstOrDefault();

            channel.AddFirst(new XElement("item",
                new XElement("title", story.Title),
                new XElement("description", story.Description),
                new XElement("link", story.Link)
            ));

            // Keep a maximum of 100 stories
            foreach (var element in feed.Descendants("item").Skip(100))
            {
                element.Remove();
            }

            Save(feed);

            return story;
        }

        private NewsStory GenerateStory()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            return new NewsStory
            {
                Title = "Top News Story " + timestamp,
                Description = "Something important in the world that happened at " + timestamp
            };
        }

    }
}