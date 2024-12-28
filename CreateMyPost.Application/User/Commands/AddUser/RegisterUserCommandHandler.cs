using CreateMyPost.Application.Helpers;
using CreateMyPost.Domain.Entities;
using CreateMyPost.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Commands.AddUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand , UserResponse>

    {
        private readonly ITokenService _tokenService;
        private readonly ICurrentService _currentService;
        private readonly UserManager<ApplicationUser> userManager;


        public RegisterUserCommandHandler(ITokenService tokenService, ICurrentService currentService, UserManager<ApplicationUser> userManager)
        {
            _tokenService = tokenService;
            _currentService = currentService;
            this.userManager = userManager;
        }

        public async Task<UserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if(existingUser is null)
            {
                throw new InvalidOperationException("Email already exists");
            }

            var newUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            newUser.UserName = GenerateUsername(request.FirstName, request.LastName);
            var result = await userManager.CreateAsync(newUser, request.Password);

            if(!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to create user");
            }
            await _tokenService.GenerateToken(newUser);

            return new UserResponse
            {
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,


            };

        }

        private string GenerateUsername(string firstName , string lastName)
        {
            var baseUsername = $"{firstName}{lastName}".ToLower();

            var username = baseUsername;

            var count  = 1;

            while (userManager.Users.Any(u=>u.UserName==username)) 
            {
                username = $"{baseUsername}{count}";
                count++;
            }

            return username ;   
        }
    }
}
