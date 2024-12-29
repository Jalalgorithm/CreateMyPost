using CreateMyPost.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Query.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}
