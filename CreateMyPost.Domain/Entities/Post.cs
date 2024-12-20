using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMyPost.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required , MaxLength(255)]
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty ;   

    }
}
