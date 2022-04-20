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
    public class PredatorTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PredatorTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PredatorTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PredatorTypes.Include(p => p.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PredatorTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predatorType = await _context.PredatorTypes
                .Include(p => p.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (predatorType == null)
            {
                return NotFound();
            }

            return View(predatorType);
        }

        // GET: PredatorTypes/Create
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

        // POST: PredatorTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,HuntingRole,BookId")] PredatorType predatorType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(predatorType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", predatorType.BookId);
            return View(predatorType);
        }

        // GET: PredatorTypes/Edit/5
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

                var predatorType = await _context.PredatorTypes.FindAsync(id);
                if (predatorType == null)
                {
                    return NotFound();
                }
                ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", predatorType.BookId);
                return View(predatorType);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: PredatorTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,HuntingRole,BookId")] PredatorType predatorType)
        {
            if (id != predatorType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(predatorType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredatorTypeExists(predatorType.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", predatorType.BookId);
            return View(predatorType);
        }

        // GET: PredatorTypes/Delete/5
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

                var predatorType = await _context.PredatorTypes
                    .Include(p => p.Book)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (predatorType == null)
                {
                    return NotFound();
                }

                return View(predatorType);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: PredatorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var predatorType = await _context.PredatorTypes.FindAsync(id);
            _context.PredatorTypes.Remove(predatorType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PredatorTypeExists(int id)
        {
            return _context.PredatorTypes.Any(e => e.Id == id);
        }
    }
}
