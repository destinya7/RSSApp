using Microsoft.AspNet.SignalR;

namespace RSSAppApi.Hubs
{
    public class NotificationHub : Hub
    {
        public void NewArticle(string article)
        {
            Clients.All.newArticle(article);
        }
    }
}