using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Uı.Models.User;

namespace Uı.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserResultViewModel resultUserDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7241/api/User");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UserResultViewModel>>(jsonData);
                var result = values.FirstOrDefault(x => x.UserName == resultUserDTO.UserName && x.Password == resultUserDTO.Password);
                if (result != null)
                {
                    var user = result.UserName;
                    HttpContext.Session.SetString("Username", result.UserName);
                    return RedirectToAction("Index", "Default");
                }
            }
            return View();

        }

    }
}

