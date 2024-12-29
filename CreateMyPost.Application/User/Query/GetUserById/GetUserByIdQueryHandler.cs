using CreateMyPost.Application.Helpers;
using CreateMyPost.Domain.Entities;
using CreateMyPost.Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Query.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentService _currentService;
        
        public GetUserByIdQueryHandler(ICurrentService currentService, UserManager<ApplicationUser> userManager)
        {
            _currentService = currentService;
            _userManager = userManager;
        }
        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if(user is null)
            {
                throw new InvalidOperationException("User not found");
            }

            var userResponse = new UserResponse
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber ?? "",
            };

            return userResponse;

        }
    }
}
