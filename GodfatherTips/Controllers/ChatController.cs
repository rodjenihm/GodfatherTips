using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodfatherTips.Data;
using GodfatherTips.Data.Models;
using GodfatherTips.Models;
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

        public async Task<IActionResult> CreatePost(CreatePostViewModel post)
        {
            if (ModelState.IsValid)
            {
                var sender = await _userManager.GetUserAsync(User);
                var newPost = new Post
                {
                    UserName = User.Identity.Name,
                    Text = post.Text,
                    AuthorId = sender.Id
                };
                await _context.Posts.AddAsync(newPost);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return RedirectToAction("Error", "Home");
        }
    }
}