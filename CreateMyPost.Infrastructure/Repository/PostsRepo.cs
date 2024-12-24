using CreateMyPost.Domain.Entities;
using CreateMyPost.Domain.Repository;
using CreateMyPost.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Infrastructure.Repository
{
    public class PostRepo : IPostRepo
    {
        private readonly ApplicationDbContext dbContext;

        public PostRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Post>> GetAllPostAsync()
        {
            return await dbContext.Posts.ToListAsync();
        }
    }
}
