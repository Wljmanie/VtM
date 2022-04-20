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
    public class LoreSheetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoreSheetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoreSheets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LoreSheets.Include(l => l.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LoreSheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loreSheet = await _context.LoreSheets
                .Include(l => l.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loreSheet == null)
            {
                return NotFound();
            }

            return View(loreSheet);
        }

        // GET: LoreSheets/Create
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

        // POST: LoreSheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,BookId")] LoreSheet loreSheet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loreSheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", loreSheet.BookId);
            return View(loreSheet);
        }

        // GET: LoreSheets/Edit/5
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

                var loreSheet = await _context.LoreSheets.FindAsync(id);
                if (loreSheet == null)
                {
                    return NotFound();
                }
                ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", loreSheet.BookId);
                return View(loreSheet);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LoreSheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,BookId")] LoreSheet loreSheet)
        {
            if (id != loreSheet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loreSheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoreSheetExists(loreSheet.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", loreSheet.BookId);
            return View(loreSheet);
        }

        // GET: LoreSheets/Delete/5
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

                var loreSheet = await _context.LoreSheets
                    .Include(l => l.Book)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (loreSheet == null)
                {
                    return NotFound();
                }

                return View(loreSheet);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LoreSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loreSheet = await _context.LoreSheets.FindAsync(id);
            _context.LoreSheets.Remove(loreSheet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoreSheetExists(int id)
        {
            return _context.LoreSheets.Any(e => e.Id == id);
        }
    }
}
