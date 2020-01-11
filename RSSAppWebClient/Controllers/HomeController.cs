using Newtonsoft.Json;
using RSSAppWebClient.Helpers;
using RSSAppWebClient.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RSSAppWebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IApiHttpClientBuilder _httpClient;

        public HomeController(IApiHttpClientBuilder httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<ActionResult> Index()
        {
            var client = _httpClient.GetClient();
            var response = await client.GetAsync("api/articles");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var articles = JsonConvert
                    .DeserializeObject<IEnumerable<Article>>(content);
                return View(articles);
            }

            return Content("An error occurred");
        }

        public async Task<ActionResult> Subscriptions()
        {
            var client = _httpClient.GetClient();
            var response = await client.GetAsync("api/channels");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var channels = JsonConvert
                    .DeserializeObject<IEnumerable<Channel>>(content);
                return View(channels);
            }

            return Content("An error occurred");
        }

        public async Task<ActionResult> Detail(string url)
        {
            var client = _httpClient.GetClient();
            var response = await client.GetAsync("api/channels?url=" + url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var channel = JsonConvert
                    .DeserializeObject<Channel>(content);
                return View(channel);
            }

            return Content("An error occurred");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Subscription subscription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = _httpClient.GetClient();
                    var serializedSubscription =
                        JsonConvert.SerializeObject(subscription);
                    var response = await client.PostAsync("/api/subscriptions",
                        new StringContent(serializedSubscription,
                        Encoding.Unicode, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    return Content("An error occurred");
                }

                return View(subscription);
            }
            catch (Exception ex)
            {
                return Content("An error occurred");
            }
        }

        public async Task<ActionResult> Delete(string url)
        {
            try
            {
                var client = _httpClient.GetClient();

                var response = await client.DeleteAsync("api/channels?url=" + url);

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("Index");
                }
                else
                {
                    return Content("An error occurred");
                }
            }
            catch (Exception ex)
            {
                return Content("An error occurred");
            }
        }
    }
}