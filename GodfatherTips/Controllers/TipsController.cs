using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GodfatherTips.Data;
using GodfatherTips.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace GodfatherTips.Controllers
{
    public class TipsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TipsController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        // GET: Tips
        [Authorize(Roles = "Admin, Vip")]
        public async Task<IActionResult> Index()
        {
            var tips = _dbContext.Tips.Include(t => t.Author);
            return View(await tips.ToListAsync());
        }

        // GET: Tips/Details/5
        [Authorize(Roles = "Admin, Vip")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _dbContext.Tips
                .Include(t => t.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tip == null)
            {
                return NotFound();
            }

            return View(tip);
        }

        // GET: Tips/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_dbContext.Users, "Id", "Id");
            return View();
        }

        // POST: Tips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nickname,Text,AuthorId,CreationDate")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(tip);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_dbContext.Users, "Id", "Id", tip.AuthorId);
            return View(tip);
        }

        // GET: Tips/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _dbContext.Tips.FindAsync(id);
            if (tip == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_dbContext.Users, "Id", "Id", tip.AuthorId);
            return View(tip);
        }

        // POST: Tips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nickname,Text,AuthorId,CreationDate")] Tip tip)
        {
            if (id != tip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(tip);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipExists(tip.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_dbContext.Users, "Id", "Id", tip.AuthorId);
            return View(tip);
        }

        // GET: Tips/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _dbContext.Tips
                .Include(t => t.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tip == null)
            {
                return NotFound();
            }

            return View(tip);
        }

        // POST: Tips/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tip = await _dbContext.Tips.FindAsync(id);
            _dbContext.Tips.Remove(tip);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipExists(int id)
        {
            return _dbContext.Tips.Any(e => e.Id == id);
        }
    }
}
