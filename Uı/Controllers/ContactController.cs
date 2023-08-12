using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Uı.Models;
using Uı.Models.Comment;

namespace Uı.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.username = HttpContext.Session.GetString("Username");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateCommentViewModel createCommentViewModel)
        {
            var user = HttpContext.Session.GetString("Username");
            createCommentViewModel.UserName = user;
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(createCommentViewModel);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7241/api/Comment", content);
            return View();
        }
    }
}
