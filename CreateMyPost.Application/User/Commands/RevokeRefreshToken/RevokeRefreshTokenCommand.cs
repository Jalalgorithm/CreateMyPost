using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommand : IRequest<RevokeRefreshTokenDto>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
