#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VtM.Data;
using VtM.Models;

namespace VtM.Controllers
{
    public class DisciplinePowersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisciplinePowersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DisciplinePowers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DisciplinePowers.Include(d => d.Amalgram).Include(d => d.Book).Include(d => d.Discipline);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DisciplinePowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinePower = await _context.DisciplinePowers
                .Include(d => d.Amalgram)
                .Include(d => d.Book)
                .Include(d => d.Discipline)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplinePower == null)
            {
                return NotFound();
            }

            return View(disciplinePower);
        }

        // GET: DisciplinePowers/Create
        public IActionResult Create()
        {
            ViewData["AmalgamId"] = new SelectList(_context.Disciplines, "Id", "Id");
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Id");
            return View();
        }

        // POST: DisciplinePowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DisciplineId,AmalgamId,AmalgramLevel,DisciplineLevel,RouseCost,AdditionalCost,DisciplinePowerName,DisciplinePowerDescription,Duration,System,RollDescription,CounterRollDescription,BookId")] DisciplinePower disciplinePower)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplinePower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmalgamId"] = new SelectList(_context.Disciplines, "Id", "Id", disciplinePower.AmalgamId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", disciplinePower.BookId);
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Id", disciplinePower.DisciplineId);
            return View(disciplinePower);
        }

        // GET: DisciplinePowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinePower = await _context.DisciplinePowers.FindAsync(id);
            if (disciplinePower == null)
            {
                return NotFound();
            }
            ViewData["AmalgamId"] = new SelectList(_context.Disciplines, "Id", "Id", disciplinePower.AmalgamId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", disciplinePower.BookId);
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Id", disciplinePower.DisciplineId);
            return View(disciplinePower);
        }

        // POST: DisciplinePowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DisciplineId,AmalgamId,AmalgramLevel,DisciplineLevel,RouseCost,AdditionalCost,DisciplinePowerName,DisciplinePowerDescription,Duration,System,RollDescription,CounterRollDescription,BookId")] DisciplinePower disciplinePower)
        {
            if (id != disciplinePower.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplinePower);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinePowerExists(disciplinePower.Id))
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
            ViewData["AmalgamId"] = new SelectList(_context.Disciplines, "Id", "Id", disciplinePower.AmalgamId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", disciplinePower.BookId);
            ViewData["DisciplineId"] = new SelectList(_context.Disciplines, "Id", "Id", disciplinePower.DisciplineId);
            return View(disciplinePower);
        }

        // GET: DisciplinePowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinePower = await _context.DisciplinePowers
                .Include(d => d.Amalgram)
                .Include(d => d.Book)
                .Include(d => d.Discipline)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplinePower == null)
            {
                return NotFound();
            }

            return View(disciplinePower);
        }

        // POST: DisciplinePowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplinePower = await _context.DisciplinePowers.FindAsync(id);
            _context.DisciplinePowers.Remove(disciplinePower);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplinePowerExists(int id)
        {
            return _context.DisciplinePowers.Any(e => e.Id == id);
        }
    }
}
