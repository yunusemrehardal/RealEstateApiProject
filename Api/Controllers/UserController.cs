using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult UserList()
        {
            var values = _userService.TGetList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _userService.TInsert(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            var value = _userService.TGetByID(id);
            _userService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var value = _userService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            _userService.TUpdate(user);
            return Ok();
        }
    }
}
