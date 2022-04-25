#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VtM.Data;
using VtM.Enums;
using VtM.Models;

namespace VtM.Controllers
{
    public class RitualsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RitualsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rituals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rituals.Include(r => r.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rituals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ritual = await _context.Rituals
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ritual == null)
            {
                return NotFound();
            }

            return View(ritual);
        }

        // GET: Rituals/Create
        [Authorize]
        public IActionResult Create()
        {
            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.StoryTeller.ToString()))
            {
                ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Rituals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RitualLevel,Ingredients,Process,System,BookId")] Ritual ritual)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ritual);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", ritual.BookId);
            return View(ritual);
        }

        // GET: Rituals/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.StoryTeller.ToString()))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ritual = await _context.Rituals.FindAsync(id);
                if (ritual == null)
                {
                    return NotFound();
                }
                ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", ritual.BookId);
                return View(ritual);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Rituals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RitualLevel,Ingredients,Process,System,BookId")] Ritual ritual)
        {
            if (id != ritual.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ritual);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RitualExists(ritual.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", ritual.BookId);
            return View(ritual);
        }

        // GET: Rituals/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.StoryTeller.ToString()))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var ritual = await _context.Rituals
                    .Include(r => r.Book)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (ritual == null)
                {
                    return NotFound();
                }

                return View(ritual);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Rituals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ritual = await _context.Rituals.FindAsync(id);
            _context.Rituals.Remove(ritual);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RitualExists(int id)
        {
            return _context.Rituals.Any(e => e.Id == id);
        }
    }
}
