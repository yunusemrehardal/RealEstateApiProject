using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using System.Text;
using Uı.Models.Home;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Uı.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateHomeViewModel createHomeViewModel)
        {
            var username = HttpContext.Session.GetString("Username");
            var client = _httpClientFactory.CreateClient();
            createHomeViewModel.Date = DateTime.Now;
            createHomeViewModel.UserName = username;
            createHomeViewModel.ImageUrl2 = "bos";
            var JsonData = JsonConvert.SerializeObject(createHomeViewModel);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7241/api/Home", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }

        public async Task<IActionResult> DeleteHome(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7241/api/Home/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
    }
}
