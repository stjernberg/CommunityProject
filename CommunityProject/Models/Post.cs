using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public Post()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
