using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultiUserMVC.Models;
using Newtonsoft.Json;
using Polly;

namespace MultiUserMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {
            var model = await GetArticles();
            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<NewsResponse> GetArticles()
        {
            // Getting an instance of HttpClient from the factory registered in Startup.cs
            var client = _httpClientFactory.CreateClient("News");


            var result = await client.GetAsync(
                $"http://newsapi.org/v2/everything?q=business&apiKey={ApiKey.key}");

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<NewsResponse>(content);
            }
            return null;


        }

    }
}