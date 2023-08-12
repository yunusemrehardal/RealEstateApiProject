using Microsoft.AspNetCore.Mvc;

namespace Uı.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
