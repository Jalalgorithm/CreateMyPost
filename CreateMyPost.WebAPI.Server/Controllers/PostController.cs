using CreateMyPost.Application.Post.Handler.GetPosts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateMyPost.WebAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public readonly ISender _sender;
        public PostController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPost() 
        {
            var posts = await _sender.Send(new GetPostQuery());

            return Ok(posts);
        }
    }
}
