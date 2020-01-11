
namespace RSSAppApi.Factories
{
    public interface IChannelFactory
    {
        ChannelService.Channel CreateChannel(Models.Channel channel);
        Models.Channel CreateChannel(ChannelService.Channel channel);
    }
}