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
using VtM.Models;

namespace VtM.Controllers
{
    public class HavensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HavensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Havens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Havens.ToListAsync());
        }

        // GET: Havens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haven = await _context.Havens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (haven == null)
            {
                return NotFound();
            }

            return View(haven);
        }

        // GET: Havens/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Havens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Description,HavenRating,Publicity")] Haven haven)
        {
            if (ModelState.IsValid)
            {
                _context.Add(haven);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(haven);
        }

        // GET: Havens/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haven = await _context.Havens.FindAsync(id);
            if (haven == null)
            {
                return NotFound();
            }
            return View(haven);
        }

        // POST: Havens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Description,HavenRating,Publicity")] Haven haven)
        {
            if (id != haven.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(haven);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HavenExists(haven.Id))
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
            return View(haven);
        }

        // GET: Havens/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haven = await _context.Havens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (haven == null)
            {
                return NotFound();
            }

            return View(haven);
        }

        // POST: Havens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var haven = await _context.Havens.FindAsync(id);
            _context.Havens.Remove(haven);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HavenExists(int id)
        {
            return _context.Havens.Any(e => e.Id == id);
        }
    }
}
