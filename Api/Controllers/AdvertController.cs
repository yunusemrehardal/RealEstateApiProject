using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public AdvertController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        public IActionResult HomeList()
        {
            var values = _homeService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddHome(Home home)
        {
            _homeService.TInsert(home);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHome(string id)
        {
            var value = _homeService.TGetByID(id);
            _homeService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetHome(string id)
        {
            var value = _homeService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateHome(Home home)
        {
            _homeService.TUpdate(home);
            return Ok();
        }
    }
}
