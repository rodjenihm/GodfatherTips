using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodfatherTips.Data;
using GodfatherTips.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GodfatherTips.Controllers
{
    [Authorize(Roles = "Admin, Vip")]
    public class ChatController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> _userManager;

        public ChatController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUserName = currentUser.UserName;
            var posts = await _context.Posts.ToListAsync();
            return View(posts);
        }

        public async Task<IActionResult> CreatePost(Post post)
        {
            if (ModelState.IsValid)
            {
                post.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                post.AuthorId = sender.Id;
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return RedirectToAction("Error", "Home");
        }
    }
}