using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Uı.Models.User;

namespace Uı.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserCreatViewModel userCreatViewModel)
        {
            if (userCreatViewModel.Name != null && userCreatViewModel.Surname != null && userCreatViewModel.Email != null && userCreatViewModel.Phone != null && userCreatViewModel.Password != null)
            {
                var client = _httpClientFactory.CreateClient();
                var JsonData = JsonConvert.SerializeObject(userCreatViewModel);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7241/api/User", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Login");
                }
                return View();
            }
            else
            {
                ViewBag.mesaj = "Lütfen tüm alanları doldurun !";
                return View();
            }
        }
    }
}
