using RSSAppApi.ArticleService;
using RSSAppApi.Factories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace RSSAppApi.Controllers
{
    [Authorize]
    public class ArticlesController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            var articleService = new ArticleServiceClient();

            try
            {
                var result = await articleService.GetArticlesAsync();
                var articles = result.Select(a => a);
                return Ok(articles);
            }
            catch (Exception ex)
            {
                articleService.Close();
                return InternalServerError();
            }
            finally
            {
                articleService.Close();
            }
        }
    }
}
