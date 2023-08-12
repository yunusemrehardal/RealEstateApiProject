using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Uı.Models;
using Uı.Models.Comment;
using Uı.Models.Home;
using Uı.Models.User;

namespace Uı.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("Username");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7241/api/User");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UserResultViewModel>>(jsonData);
                var result = values.FirstOrDefault(x => x.UserName == username);
                if (result != null)
                {
                    return View(result);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateCommentViewModel updateCommentViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(updateCommentViewModel);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7241/api/User", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("Username", updateCommentViewModel.UserName);
                return RedirectToAction("Index", "Default");
            }
            return View();
        }

        public async Task<IActionResult> MyHome()
        {
            var username = HttpContext.Session.GetString("Username");
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7241/api/Home");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultHomeListViewModel>>(jsonData);
                var result = values.Where(x => x.UserName == username).ToList();
                return View(result);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHome(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7241/api/Home/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateHomeViewModel>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHome(UpdateHomeViewModel updateHomeViewModel)
        {
            var username = HttpContext.Session.GetString("Username");
            updateHomeViewModel.UserName = username;
            updateHomeViewModel.ImageUrl2 = "bos";
            updateHomeViewModel.Date = DateTime.Now;
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(updateHomeViewModel);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7241/api/Home", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MyHome", "User");
            }
            return View();
        }

        public async Task<IActionResult> MyComments()
        {
            var username = HttpContext.Session.GetString("Username");
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7241/api/Comment");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentViewModel>>(jsonData);
                var result = values.Where(x => x.UserName == username).ToList();
                return View(result);
            }
            return View();
        }

        public async Task<IActionResult> DeleteComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7241/api/Comment/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MyComments", "User");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7241/api/Comment/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCommentViewModel>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateCommentViewModel updateCommentViewModel)
        {
            var username = HttpContext.Session.GetString("Username");
            updateCommentViewModel.UserName = username;
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(updateCommentViewModel);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7241/api/Comment", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MyComments", "User");
            }
            return View();
        }
    }
}

