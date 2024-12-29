using CreateMyPost.Application.Helpers;
using CreateMyPost.Domain.Entities;
using CreateMyPost.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Commands.LogInUser
{
    public class LogInUserCommandHandler : IRequestHandler<LogInUserCommand, UserResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService tokenService;

        public LogInUserCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            this.tokenService = tokenService;
        }
        public async Task<UserResponse> Handle(LogInUserCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));   

            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user is null || await _userManager.CheckPasswordAsync(user , request.Password))
            {
                throw new InvalidOperationException("Invalid email or password");
            }

             var token = await tokenService.GenerateToken(user);

            var refreshToken = tokenService.GenerateRefreshToken();

            using var sha256 = SHA256.Create();
            var refreshtoken = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
            user.RefreshToken = Convert.ToBase64String(refreshtoken);
            user.RefreshTokenExpires = DateTime.Now.AddDays(7);

            var result = await _userManager.UpdateAsync(user);  
            if(!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to update user");
            }

            var userResponse = new UserResponse
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? "",
                Token = token,
                RefreshToken = refreshToken,
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,

            };

            return userResponse;

        }
    }
}
