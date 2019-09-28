using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GodfatherTips.Models
{
    public class CreateTipViewModel
    {
        [Required(ErrorMessage = "Tekst tipa je prazan")]
        [Display(Name = "Tip")]
        public string Text { get; set; }
    }
}
