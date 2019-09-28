using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GodfatherTips.Data.Models
{
    public class Tip
    {
        public int Id { get; set; }

        [Display(Name = "Nadimak")]
        public string Nickname { get; set; }

        [Display(Name = "Tip")]
        public string Text { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        [Display(Name = "Datum objavljivanja")]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
