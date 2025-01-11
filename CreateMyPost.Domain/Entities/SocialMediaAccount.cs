using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Domain.Entities
{
    public class SocialMediaAccount
    {
        public int Id { get; set; }
        public string ApplicationUserId{ get; set; }
        public string Platform { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public DateTime TokenExpiry { get; set; }
    }
}
