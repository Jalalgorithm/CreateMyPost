using CreateMyPost.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand , RevokeRefreshTokenDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public RevokeRefreshTokenCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RevokeRefreshTokenDto> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var sha256 = SHA256.Create();
                var refreshToken = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.RefreshToken));
                var hashedToken = Convert.ToBase64String(refreshToken);

                var user = await _userManager.Users.FirstOrDefaultAsync(u=>u.RefreshToken==hashedToken);    

                if(user is null)
                {
                    throw new InvalidOperationException("Invalid refresh token");
                }

                if(user.RefreshTokenExpires < DateTime.Now)
                {
                    throw new InvalidOperationException("Refreshtoken expired");
                }

                user.RefreshToken = null;
                user.RefreshTokenExpires = null;

                var result = await _userManager.UpdateAsync(user);

                if(!result.Succeeded)
                {
                    return new RevokeRefreshTokenDto
                    {
                        Message = "Failed to remove refresh token"
                    };
                }

                return new RevokeRefreshTokenDto
                {
                    Message = "Refresh token successfully removed."
                };


            } 
            catch (Exception)
            {

                throw new Exception("Failed to revoke refresh token");
            }
        }
    }
}
