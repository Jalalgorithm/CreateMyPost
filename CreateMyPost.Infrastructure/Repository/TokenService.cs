using CreateMyPost.Domain.Entities;
using CreateMyPost.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Infrastructure.Repository
{
    public class TokenService : ITokenService
    {


        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateToken(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
