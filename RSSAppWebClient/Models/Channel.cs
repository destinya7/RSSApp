using System.Collections.Generic;

namespace RSSAppWebClient.Models
{
    public class Channel
    {
        public string Title { get; set; }

        public string RSS_URL { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public Image ChannelImage { get; set; }

        public List<Article> Articles { get; set; }

        public class Image
        {
            public string URL { get; set; }

            public string Title { get; set; }
        }
    }
}