using CreateMyPost.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Query.GetCurrentUser
{
    public class GetCurrentUserQuery :IRequest<UserResponse>
    {
    }
}
