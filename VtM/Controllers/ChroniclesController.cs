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
    public class ChroniclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChroniclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chronicles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Chronicles.Include(c => c.StoryTeller);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Chronicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chronicle = await _context.Chronicles
                .Include(c => c.StoryTeller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chronicle == null)
            {
                return NotFound();
            }

            return View(chronicle);
        }

        // GET: Chronicles/Create
        [Authorize]
        public IActionResult Create()
        {
            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.StoryTeller.ToString()))
            {
                ViewData["StoryTellerId"] = new SelectList(_context.VtMUsers, "Id", "UserName");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Chronicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StoryTellerId")] Chronicle chronicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chronicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoryTellerId"] = new SelectList(_context.VtMUsers, "Id", "UserName", chronicle.StoryTellerId);
            return View(chronicle);
        }

        // GET: Chronicles/Edit/5
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

                var chronicle = await _context.Chronicles.FindAsync(id);
                if (chronicle == null)
                {
                    return NotFound();
                }
                ViewData["StoryTellerId"] = new SelectList(_context.VtMUsers, "Id", "UserName", chronicle.StoryTellerId);
                return View(chronicle);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Chronicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StoryTellerId")] Chronicle chronicle)
        {
            if (id != chronicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chronicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChronicleExists(chronicle.Id))
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
            ViewData["StoryTellerId"] = new SelectList(_context.VtMUsers, "Id", "UserName", chronicle.StoryTellerId);
            return View(chronicle);
        }

        // GET: Chronicles/Delete/5
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

                var chronicle = await _context.Chronicles
                    .Include(c => c.StoryTeller)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (chronicle == null)
                {
                    return NotFound();
                }

                return View(chronicle);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Chronicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chronicle = await _context.Chronicles.FindAsync(id);
            _context.Chronicles.Remove(chronicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChronicleExists(int id)
        {
            return _context.Chronicles.Any(e => e.Id == id);
        }
    }
}
