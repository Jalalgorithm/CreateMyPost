using CreateMyPost.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Domain.Repository
{
     public interface IPostRepo
    {
        Task<List<Post>> GetAllPostAsync();
    }
}
