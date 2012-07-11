using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace RealTimeWeb.Models
{
    public class NewsStory
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }

    public class NewsService
    {
        private const string RemoteFilePath = "http://rss.cnn.com/rss/cnn_topstories.rss";
        private static string LocalFilePath = HttpContext.Current.Server.MapPath("~/App_Data/top-stories.rss");

        public Task<IEnumerable<NewsStory>> GetTopStoriesAsync()
        {
            return Task.Factory.StartNew<IEnumerable<NewsStory>>(GetTopStories);
        }

        public IEnumerable<NewsStory> GetTopStories()
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
            Thread.Sleep(TimeSpan.FromSeconds(5));

            return stories;
        }

        private static XDocument GetRssFeed()
        {
            if (!File.Exists(LocalFilePath))
            {
                var feed = XDocument.Load(RemoteFilePath);
                feed.Save(LocalFilePath);
                return feed;
            }

            return XDocument.Load(RemoteFilePath);
        }
    }
}