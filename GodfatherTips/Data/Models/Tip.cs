using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GodfatherTips.Data.Models
{
    public class Tip
    {
        public int Id { get; set; }

        public string Nickname { get; set; }

        public string Text { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
