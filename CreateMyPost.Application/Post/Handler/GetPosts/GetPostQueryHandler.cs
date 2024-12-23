using CreateMyPost.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Application.Post.Handler.GetPosts
{


    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, List<GetPostDto>>
    {
        private readonly IPostRepo postRepo;

        public GetPostQueryHandler(IPostRepo postRepo)
        {
            this.postRepo = postRepo;
        }
        public async Task<List<GetPostDto>> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var posts = await postRepo.GetAllPostAsync();

            var postList = posts.Select(p => new GetPostDto
            {
                Id = p.Id,
                Content = p.Content,
            }).ToList();

            return postList;
        }
    }
}
