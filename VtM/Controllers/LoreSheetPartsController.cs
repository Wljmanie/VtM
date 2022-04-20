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
    public class LoreSheetPartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoreSheetPartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoreSheetParts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LoreSheetParts.Include(l => l.LoreSheet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LoreSheetParts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loreSheetPart = await _context.LoreSheetParts
                .Include(l => l.LoreSheet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loreSheetPart == null)
            {
                return NotFound();
            }

            return View(loreSheetPart);
        }

        // GET: LoreSheetParts/Create
        [Authorize]
        public IActionResult Create()
        {
            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.StoryTeller.ToString()))
            {
                ViewData["LoreSheetId"] = new SelectList(_context.LoreSheets, "Id", "Name");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LoreSheetParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,LoreSheetId,Name,Description,Level")] LoreSheetPart loreSheetPart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loreSheetPart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoreSheetId"] = new SelectList(_context.LoreSheets, "Id", "Name", loreSheetPart.LoreSheetId);
            return View(loreSheetPart);
        }

        // GET: LoreSheetParts/Edit/5
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

                var loreSheetPart = await _context.LoreSheetParts.FindAsync(id);
                if (loreSheetPart == null)
                {
                    return NotFound();
                }
                ViewData["LoreSheetId"] = new SelectList(_context.LoreSheets, "Id", "Name", loreSheetPart.LoreSheetId);
                return View(loreSheetPart);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LoreSheetParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LoreSheetId,Name,Description,Level")] LoreSheetPart loreSheetPart)
        {
            if (id != loreSheetPart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loreSheetPart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoreSheetPartExists(loreSheetPart.Id))
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
            ViewData["LoreSheetId"] = new SelectList(_context.LoreSheets, "Id", "Id", loreSheetPart.LoreSheetId);
            return View(loreSheetPart);
        }

        // GET: LoreSheetParts/Delete/5
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

                var loreSheetPart = await _context.LoreSheetParts
                    .Include(l => l.LoreSheet)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (loreSheetPart == null)
                {
                    return NotFound();
                }

                return View(loreSheetPart);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LoreSheetParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loreSheetPart = await _context.LoreSheetParts.FindAsync(id);
            _context.LoreSheetParts.Remove(loreSheetPart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoreSheetPartExists(int id)
        {
            return _context.LoreSheetParts.Any(e => e.Id == id);
        }
    }
}
