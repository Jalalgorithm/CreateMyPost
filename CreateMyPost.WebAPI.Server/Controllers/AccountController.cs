using CreateMyPost.Application.User.Commands.AddUser;
using CreateMyPost.Application.User.Commands.LogInUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreateMyPost.WebAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;
        public AccountController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand userCommand)
        {
            var response = await _sender.Send(userCommand);
            return Ok(response);    
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LogInUserCommand userLogin)
        {
            var response = await _sender.Send(userLogin);
            return Ok(response);
        }

        

        
    }
}
