using System;

namespace GodfatherTips.Data.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}