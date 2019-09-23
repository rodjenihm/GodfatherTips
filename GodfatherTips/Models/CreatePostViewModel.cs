using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GodfatherTips.Models
{
    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "Poruka je prazna")]
        public string Text { get; set; }
    }
}
