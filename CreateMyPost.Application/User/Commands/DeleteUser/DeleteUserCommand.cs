using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Commands.DeleteUser
{
    public class DeleteUserCommand:IRequest
    {
        public Guid Id { get; set; }

        public DeleteUserCommand( Guid id)
        {
            Id = id;
        }
    }
}
