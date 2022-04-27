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
    public class ThinBloodAlchemiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThinBloodAlchemiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ThinBloodAlchemies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ThinBloodAlchemies.Include(t => t.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ThinBloodAlchemies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thinBloodAlchemy = await _context.ThinBloodAlchemies
                .Include(t => t.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thinBloodAlchemy == null)
            {
                return NotFound();
            }

            return View(thinBloodAlchemy);
        }

        // GET: ThinBloodAlchemies/Create
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

        // POST: ThinBloodAlchemies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Ingredients,ActivationCost,DicePools,System,Duration,AthanorCorporis,Calcinatio,AlchemyLevel,BookId")] ThinBloodAlchemy thinBloodAlchemy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thinBloodAlchemy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", thinBloodAlchemy.BookId);
            return View(thinBloodAlchemy);
        }

        // GET: ThinBloodAlchemies/Edit/5
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

                var thinBloodAlchemy = await _context.ThinBloodAlchemies.FindAsync(id);
                if (thinBloodAlchemy == null)
                {
                    return NotFound();
                }
                ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", thinBloodAlchemy.BookId);
                return View(thinBloodAlchemy);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ThinBloodAlchemies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Ingredients,ActivationCost,DicePools,System,Duration,AthanorCorporis,Calcinatio,AlchemyLevel,BookId")] ThinBloodAlchemy thinBloodAlchemy)
        {
            if (id != thinBloodAlchemy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thinBloodAlchemy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThinBloodAlchemyExists(thinBloodAlchemy.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", thinBloodAlchemy.BookId);
            return View(thinBloodAlchemy);
        }

        // GET: ThinBloodAlchemies/Delete/5
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

                var thinBloodAlchemy = await _context.ThinBloodAlchemies
                    .Include(t => t.Book)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (thinBloodAlchemy == null)
                {
                    return NotFound();
                }

                return View(thinBloodAlchemy);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ThinBloodAlchemies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thinBloodAlchemy = await _context.ThinBloodAlchemies.FindAsync(id);
            _context.ThinBloodAlchemies.Remove(thinBloodAlchemy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThinBloodAlchemyExists(int id)
        {
            return _context.ThinBloodAlchemies.Any(e => e.Id == id);
        }
    }
}
