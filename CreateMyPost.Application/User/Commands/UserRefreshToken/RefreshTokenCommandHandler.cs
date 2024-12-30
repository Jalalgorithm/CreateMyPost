using CreateMyPost.Application.Helpers;
using CreateMyPost.Domain.Entities;
using CreateMyPost.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Commands.UserRefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, UserResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        public RefreshTokenCommandHandler(ITokenService tokenService, UserManager<ApplicationUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }
        public async Task<UserResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            using var sha256 = SHA256.Create();
            var refreshToken = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.RefreshToken));
            var hashedRefreshToken = Convert.ToBase64String(refreshToken);


            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == hashedRefreshToken);

            if(user is null)
            {
                throw new InvalidOperationException("User is not found");
            }

            if(user.RefreshTokenExpires < DateTime.Now)
            {
                throw new InvalidOperationException("Refresh token expired");
            }

            var newAccessToken = await _tokenService.GenerateToken(user);

            var response = new UserResponse
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = newAccessToken,

            };

            return response;
        }
    }
}
