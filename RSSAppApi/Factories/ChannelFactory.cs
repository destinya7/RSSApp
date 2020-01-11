using RSSAppApi.ChannelService;
using System.Collections.Generic;
using System.Linq;

namespace RSSAppApi.Factories
{
    public class ChannelFactory : IChannelFactory
    {
        private readonly IArticleFactory _articleFactory;

        public ChannelFactory(IArticleFactory articleFactory)
        {
            this._articleFactory = articleFactory;
        }

        public Channel CreateChannel(Models.Channel channel)
        {
            return new Channel
            {
                Title = channel.Title,
                RSS_URL = channel.RSS_URL,
                Link = channel.Link,
                Description = channel.Description,
                ChannelImage = new Channel.Image
                {
                    URL = channel.ChannelImage?.URL,
                    Title = channel.ChannelImage?.Title
                },
                Articles = (channel.Articles != null)
                    ? channel.Articles.Select(
                        a => _articleFactory.CreateArticle(a)).ToArray()
                    : new Article[] {}
            };
        }

        public Models.Channel CreateChannel(Channel channel)
        {
            return new Models.Channel
            {
                Title = channel.Title,
                RSS_URL = channel.RSS_URL,
                Link = channel.Link,
                Description = channel.Description,
                ChannelImage = new Models.Channel.Image
                {
                    URL = channel.ChannelImage?.URL,
                    Title = channel.ChannelImage?.Title
                },
                Articles = (channel.Articles != null)
                    ? channel.Articles.Select(
                        a => _articleFactory.CreateArticle(a)).ToList()
                    : new List<Models.Article>()
            };
        }
    }
}