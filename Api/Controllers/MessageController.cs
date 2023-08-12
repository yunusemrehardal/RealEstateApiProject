using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IActionResult MessageList()
        {
            var values = _messageService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddMessage(Message message)
        {
            _messageService.TInsert(message);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(string id)
        {
            var value = _messageService.TGetByID(id);
            _messageService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetMessage(string id)
        {
            var value = _messageService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateMessage(Message message)
        {
            _messageService.TUpdate(message);
            return Ok();
        }
    }
}
