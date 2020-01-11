using Microsoft.AspNet.SignalR;
using RSSAppApi.ChannelService;
using RSSAppApi.Factories;
using RSSAppApi.Hubs;
using RSSAppApi.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace RSSAppApi.Controllers
{
    //[Authorize]
    public class SubscriptionsController : ApiController
    {
        private readonly IWorkerQueuePublisher _publisher;
        private readonly IChannelFactory _factory;

        public SubscriptionsController(
            IWorkerQueuePublisher publisher,
            IChannelFactory factory
        )
        {
            this._publisher = publisher;
            this._factory = factory;
        }

        public IHttpActionResult Post([FromBody]Models.Subscription subscription)
        {
            if (subscription == null)
                return BadRequest();

            try
            {
                _publisher.PublishMessage(subscription.Url);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public async Task<IHttpActionResult> Delete([FromBody]Models.Subscription subscription)
        {
            if (subscription == null)
                return BadRequest();

            var channelService = new ChannelServiceClient();

            try
            {
                var channel = await channelService.DeleteChannelAsync(subscription.Url);
                return Ok(channel);
            }
            catch (Exception ex)
            {
                channelService.Close();
                return InternalServerError(ex);
            }
            finally
            {
                channelService.Close();
            }
        }
    }
}
