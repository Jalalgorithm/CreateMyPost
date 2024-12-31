using CreateMyPost.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserResponse>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }=string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public UpdateUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
