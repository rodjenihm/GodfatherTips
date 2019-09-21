using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GodfatherTips.Data.Models;
using GodfatherTips.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GodfatherTips.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} je obavezan")]
            [StringLength(30, ErrorMessage = "{0} mora biti najmanje {2} a najviše {1} karaktera dugačak", MinimumLength = 2)]
            [Display(Name = "Nadimak")]
            public string Nickname { get; set; }

            [Required(ErrorMessage = "{0} adresa je obavezna")]
            [EmailAddress(ErrorMessage = "Email adresa nije validna")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "{0} je obavezna")]
            [StringLength(30, ErrorMessage = "{0} mora biti najmanje {2} a najviše {1} karaktera dugačka", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Šifra")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potvrdite šifru")]
            [Compare("Password", ErrorMessage = "Šifre se ne podudaraju")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Nickname = Input.Nickname,
                    IsVipMember = false,
                    Role = Role.Member,
                    RegistrationDate = DateTime.UtcNow
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "Member");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Potvrdite vaš nalog",
                        $"Molimo vas da potvrdite vaš nalog na sledećem linku <a href='{callbackUrl}'>clicking here</a>.");

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}