
namespace RSSAppApi.Factories
{
    public interface IArticleFactory
    {
        ChannelService.Article CreateArticle(Models.Article article);
        Models.Article CreateArticle(ChannelService.Article article);
    }
}