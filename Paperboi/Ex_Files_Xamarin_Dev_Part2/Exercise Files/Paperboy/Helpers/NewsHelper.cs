using Newtonsoft.Json;
using Paperboy.News;
using Paperboy.News.Trending;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Paperboy.Helpers
{
    public static class NewsHelper
    {
        public static async Task<List<NewsInformation>> GetByCategoryAsync(NewsCategoryType category)
        {
            List<NewsInformation> results = new List<NewsInformation>();

            string searchUrl = $"https://api.cognitive.microsoft.com/bing/v5.0/news/?Category={category}";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.NewsSearchApiKey);

            var uri = new Uri(searchUrl);
            var result = await client.GetStringAsync(uri);
            var newsResult = JsonConvert.DeserializeObject<NewsResult>(result);

            results = (from item in newsResult.value
                       select new NewsInformation()
                       {
                           Title = item.name,
                           Description = item.description,
                           CreatedDate = item.datePublished,
                           ImageUrl = item.image?.thumbnail?.contentUrl,

                       }).ToList();

            return results.Where(w => !string.IsNullOrEmpty(w.ImageUrl)).Take(10).ToList();
        }

        public static async Task<List<NewsInformation>> GetAsync(string searchQuery)
        {
            List<NewsInformation> results = new List<NewsInformation>();

            string searchUrl = $"https://api.cognitive.microsoft.com/bing/v5.0/news/search?q={searchQuery}&count=10&offset=0&mkt=en-us&safeSearch=Moderate";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.NewsSearchApiKey);

            var uri = new Uri(searchUrl);
            var result = await client.GetStringAsync(uri);
            var newsResult = JsonConvert.DeserializeObject<NewsResult>(result);

            results = (from item in newsResult.value
                       select new NewsInformation()
                       {
                           Title = item.name,
                           Description = item.description,
                           CreatedDate = item.datePublished,
                           ImageUrl = item.image?.thumbnail?.contentUrl,

                       }).ToList();

            return results.Where(w => !string.IsNullOrEmpty(w.ImageUrl)).Take(10).ToList();
        }

        public async static Task<List<NewsInformation>> GetTrendingAsync()
        {
            List<NewsInformation> results = new List<NewsInformation>();

            string searchUrl = $"https://api.cognitive.microsoft.com/bing/v5.0/news/trendingtopics";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.NewsSearchApiKey);

            var uri = new Uri(searchUrl);
            var result = await client.GetStringAsync(uri);
            var newsResult = JsonConvert.DeserializeObject<TrendingNewsResult>(result);

            results = (from item in newsResult.value
                       select new NewsInformation()
                       {
                           Title = item.name,
                           Description = item.query.text,
                           CreatedDate = DateTime.Now,
                           ImageUrl = item.image.url,

                       }).ToList();

            return results.Where(w => !string.IsNullOrEmpty(w.ImageUrl)).Take(10).ToList();

        }
    }
}
