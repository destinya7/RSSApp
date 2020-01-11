using System;

namespace RSSAppApi.Models
{
    public class Article
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public DateTime PubDate { get; set; }

        public int ChannelId { get; set; }
    }
}