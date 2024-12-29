using CreateMyPost.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Commands.LogInUser
{
    public record LogInUserCommand(string Email , string Password) : IRequest<UserResponse>;
    
}
