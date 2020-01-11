using RSSAppApi.ChannelService;
using RSSAppApi.Factories;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace RSSAppApi.Controllers
{
    [Authorize]
    public class ChannelsController : ApiController
    {
        private readonly IChannelFactory _factory;

        public ChannelsController(IChannelFactory factory)
        {
            this._factory = factory;
        }

        public async Task<IHttpActionResult> Get()
        {
            var channelService = new ChannelServiceClient();

            try
            {
                var result = await channelService.GetChannelsAsync();
                var channels = result.Select(r => _factory.CreateChannel(r));
                return Ok(channels);
            }
            catch (Exception ex)
            {
                channelService.Close();
                return InternalServerError();
            }
            finally
            {
                channelService.Close();
            }
        }

        public async Task<IHttpActionResult> Get(string url)
        {
            var channelService = new ChannelServiceClient();

            try
            {
                var channel = await channelService.GetChannelAsync(url);

                if (channel == null)
                    return NotFound();
               
                return Ok(channel);
            }
            catch (Exception ex)
            {
                channelService.Close();
                return InternalServerError();
            }
            finally
            {
                channelService.Close();
            }
        }

        public async Task<IHttpActionResult> Delete(string url)
        {
            var channelService = new ChannelServiceClient();

            try
            {
                var result = await channelService.DeleteChannelAsync(url);

                if (result.Status == MessageStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (result.Status == MessageStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                channelService.Close();
                return InternalServerError();
            }
            finally
            {
                channelService.Close();
            }
        }
    }
}
