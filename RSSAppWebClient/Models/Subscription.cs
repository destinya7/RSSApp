using System.ComponentModel.DataAnnotations;

namespace RSSAppWebClient.Models
{
    public class Subscription
    {
        [UrlAttribute]
        [Required]
        public string Url { get; set; }
    }
}