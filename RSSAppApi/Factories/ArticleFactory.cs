namespace RSSAppApi.Factories
{
    public class ArticleFactory : IArticleFactory
    {
        public ChannelService.Article CreateArticle(Models.Article article)
        {
            return new ChannelService.Article
            {
                Title = article.Title,
                Description = article.Description,
                Link = article.Link,
                PubDate = article.PubDate,
                ChannelId = article.ChannelId
            };
        }

        public Models.Article CreateArticle(ChannelService.Article article)
        {
            return new Models.Article
            {
                Title = article.Title,
                Description = article.Description,
                Link = article.Link,
                PubDate = article.PubDate,
                ChannelId = article.ChannelId
            };
        }
    }
}