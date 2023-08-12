using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _commentService.TGetList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            _commentService.TInsert(comment);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(string id)
        {
            var value = _commentService.TGetByID(id);
            _commentService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(string id)
        {
            var value = _commentService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateComment(Comment comment)
        {
            _commentService.TUpdate(comment);
            return Ok();
        }
    }
}
