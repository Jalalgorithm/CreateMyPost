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

namespace CreateMyPost.Application.User.Query.GetCurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserResponse>
    {
        private readonly ICurrentService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetCurrentUserQueryHandler(UserManager<ApplicationUser> userManager, ICurrentService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }
        public async Task<UserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_currentUserService.UserId());

            if(user is null)
            {
                throw new InvalidOperationException("User not found");
            }

            var response = new UserResponse
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? "",
                FirstName = user.FirstName,
                LastName = user.LastName,

            };

            return response;
        }
    }
}
