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
    public class BloodPotenciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloodPotenciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BloodPotencies
        public async Task<IActionResult> Index()
        {
            return View(await _context.BloodPotencies.ToListAsync());
        }

      

        // GET: BloodPotencies/Create
        [Authorize]
        public IActionResult Create()
        {
            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.StoryTeller.ToString()))
                return View();
            return RedirectToAction(nameof(Index));
        }

        // POST: BloodPotencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Level,BloodSurge,DamageMendedPerRouse,DisciplinePowerBonues,BaneSeverity,DisciplineRouseCheckReroll,FeedingPenalty")] BloodPotency bloodPotency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloodPotency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloodPotency);
        }

        // GET: BloodPotencies/Edit/5
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

                var bloodPotency = await _context.BloodPotencies.FindAsync(id);
                if (bloodPotency == null)
                {
                    return NotFound();
                }
                return View(bloodPotency);
            }
                
            return RedirectToAction(nameof(Index));


            
        }

        // POST: BloodPotencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Level,BloodSurge,DamageMendedPerRouse,DisciplinePowerBonues,BaneSeverity,DisciplineRouseCheckReroll,FeedingPenalty")] BloodPotency bloodPotency)
        {
            if (id != bloodPotency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodPotency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodPotencyExists(bloodPotency.Id))
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
            return View(bloodPotency);
        }

        // GET: BloodPotencies/Delete/5
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

                var bloodPotency = await _context.BloodPotencies
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (bloodPotency == null)
                {
                    return NotFound();
                }

                return View(bloodPotency);
            }              
            return RedirectToAction(nameof(Index)); 
        }

        // POST: BloodPotencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloodPotency = await _context.BloodPotencies.FindAsync(id);
            _context.BloodPotencies.Remove(bloodPotency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodPotencyExists(int id)
        {
            return _context.BloodPotencies.Any(e => e.Id == id);
        }
    }
}
