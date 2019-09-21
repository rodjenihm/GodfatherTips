using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GodfatherTips.Data.Models;
using GodfatherTips.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GodfatherTips.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string Username { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string Status { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Neuspešno učitavanje korisnika ID: '{_userManager.GetUserId(User)}'");
            }

            var userName = await _userManager.GetUserNameAsync(user);

            Username = userName;
            Nickname = user.Nickname;
            Email = user.Email;
            RegistrationDate = user.RegistrationDate;
            if (user.Role == Role.Member) Status = "Standardan korisnik";
            else if (user.Role == Role.Vip) Status = "VIP korisnik";
            else Status = "Admin";

            return Page();
        }
    }
}