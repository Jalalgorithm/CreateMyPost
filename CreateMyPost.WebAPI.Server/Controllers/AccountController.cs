using CreateMyPost.Application.User.Commands.AddUser;
using CreateMyPost.Application.User.Commands.DeleteUser;
using CreateMyPost.Application.User.Commands.LogInUser;
using CreateMyPost.Application.User.Commands.RevokeRefreshToken;
using CreateMyPost.Application.User.Commands.UpdateUser;
using CreateMyPost.Application.User.Commands.UserRefreshToken;
using CreateMyPost.Application.User.Query.GetCurrentUser;
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

        [Authorize]
        [HttpGet("UserProfile")]
        public async Task<IActionResult> UserProfile()
        {
            var response = await _sender.Send(new GetCurrentUserQuery());
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateUser(Guid id ,UpdateUserCommand userUpdate)
        {
            userUpdate.Id = id; 
            var response = await _sender.Send(userUpdate);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id , DeleteUserCommand userDelete)
        {
            userDelete.Id = id;
            await _sender.Send(userDelete);
            return Ok("Account deleted successfully");
        }

        [HttpDelete("RevokeToken")]
        public async Task<IActionResult> RevokeToken(RevokeRefreshTokenCommand revokeToken)
        {
            var response = await _sender.Send(revokeToken);
            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> GenerateRefreshToken(RefreshTokenCommand refreshToken)
        {
            var response = await _sender.Send(refreshToken);
            return Ok();
        }
        
    }
}
