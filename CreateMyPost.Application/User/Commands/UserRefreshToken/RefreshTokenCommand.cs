using CreateMyPost.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Commands.UserRefreshToken
{
    public class RefreshTokenCommand : IRequest<UserResponse>
    {
        public string RefreshToken { get; set; }

    }
}
