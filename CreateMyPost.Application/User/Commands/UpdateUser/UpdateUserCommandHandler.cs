using CreateMyPost.Application.Helpers;
using CreateMyPost.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());

            if(user is null)
            {
                throw new InvalidOperationException("User not found");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            
           await userManager.UpdateAsync(user);

            var response = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.LastName
            };

            return response;
        }
    }
}
