using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.Post.Handler.GetPosts
{
    public class GetPostQuery : IRequest<List<GetPostDto>>
    {
    }
}
