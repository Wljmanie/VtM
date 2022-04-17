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
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Characters.Include(c => c.BloodPotency).Include(c => c.Chronicle).Include(c => c.Clan).Include(c => c.Coterie).Include(c => c.Haven).Include(c => c.PredatorType).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.BloodPotency)
                .Include(c => c.Chronicle)
                .Include(c => c.Clan)
                .Include(c => c.Coterie)
                .Include(c => c.Haven)
                .Include(c => c.PredatorType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            ViewData["BloodPotencyId"] = new SelectList(_context.BloodPotencies, "Id", "Id");
            ViewData["ChronicleId"] = new SelectList(_context.Chronicles, "Id", "Id");
            ViewData["ClanId"] = new SelectList(_context.Clans, "Id", "Id");
            ViewData["CoterieId"] = new SelectList(_context.Coteries, "Id", "Id");
            ViewData["HavenId"] = new SelectList(_context.Havens, "Id", "Id");
            ViewData["PredatorTypeId"] = new SelectList(_context.PredatorTypes, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.VtMUsers, "Id", "Id");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Name,Concept,ChronicleId,Ambition,Desire,PredatorTypeId,ClanId,Generation,Sire,FileName,FileData,FileContentType,Strength,Dexterity,Stamina,Charisma,Manipulation,Composure,Intelligence,Wits,Resolve,SuperficialDamageTaken,AggravatedDamageTaken,SuperficialWillpowerDamageTaken,AggravatedWillpowerDamageTaken,Humanity,Stains,Hunger,BloodPotencyId,ResonanceType,TrueAge,ApparentAge,DateOfBirth,DateOfDeath,Appearance,DistinguishingFeatures,History,ExperienceTotal,ExperienceSpent,CharacterPublicity,CoterieId,HavenId,ThinBloodDistillationMethod")] Character character)
        {
            if (ModelState.IsValid)
            {
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BloodPotencyId"] = new SelectList(_context.BloodPotencies, "Id", "Id", character.BloodPotencyId);
            ViewData["ChronicleId"] = new SelectList(_context.Chronicles, "Id", "Id", character.ChronicleId);
            ViewData["ClanId"] = new SelectList(_context.Clans, "Id", "Id", character.ClanId);
            ViewData["CoterieId"] = new SelectList(_context.Coteries, "Id", "Id", character.CoterieId);
            ViewData["HavenId"] = new SelectList(_context.Havens, "Id", "Id", character.HavenId);
            ViewData["PredatorTypeId"] = new SelectList(_context.PredatorTypes, "Id", "Id", character.PredatorTypeId);
            ViewData["UserId"] = new SelectList(_context.VtMUsers, "Id", "Id", character.UserId);
            return View(character);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            ViewData["BloodPotencyId"] = new SelectList(_context.BloodPotencies, "Id", "Id", character.BloodPotencyId);
            ViewData["ChronicleId"] = new SelectList(_context.Chronicles, "Id", "Id", character.ChronicleId);
            ViewData["ClanId"] = new SelectList(_context.Clans, "Id", "Id", character.ClanId);
            ViewData["CoterieId"] = new SelectList(_context.Coteries, "Id", "Id", character.CoterieId);
            ViewData["HavenId"] = new SelectList(_context.Havens, "Id", "Id", character.HavenId);
            ViewData["PredatorTypeId"] = new SelectList(_context.PredatorTypes, "Id", "Id", character.PredatorTypeId);
            ViewData["UserId"] = new SelectList(_context.VtMUsers, "Id", "Id", character.UserId);
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Name,Concept,ChronicleId,Ambition,Desire,PredatorTypeId,ClanId,Generation,Sire,FileName,FileData,FileContentType,Strength,Dexterity,Stamina,Charisma,Manipulation,Composure,Intelligence,Wits,Resolve,SuperficialDamageTaken,AggravatedDamageTaken,SuperficialWillpowerDamageTaken,AggravatedWillpowerDamageTaken,Humanity,Stains,Hunger,BloodPotencyId,ResonanceType,TrueAge,ApparentAge,DateOfBirth,DateOfDeath,Appearance,DistinguishingFeatures,History,ExperienceTotal,ExperienceSpent,CharacterPublicity,CoterieId,HavenId,ThinBloodDistillationMethod")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.Id))
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
            ViewData["BloodPotencyId"] = new SelectList(_context.BloodPotencies, "Id", "Id", character.BloodPotencyId);
            ViewData["ChronicleId"] = new SelectList(_context.Chronicles, "Id", "Id", character.ChronicleId);
            ViewData["ClanId"] = new SelectList(_context.Clans, "Id", "Id", character.ClanId);
            ViewData["CoterieId"] = new SelectList(_context.Coteries, "Id", "Id", character.CoterieId);
            ViewData["HavenId"] = new SelectList(_context.Havens, "Id", "Id", character.HavenId);
            ViewData["PredatorTypeId"] = new SelectList(_context.PredatorTypes, "Id", "Id", character.PredatorTypeId);
            ViewData["UserId"] = new SelectList(_context.VtMUsers, "Id", "Id", character.UserId);
            return View(character);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.BloodPotency)
                .Include(c => c.Chronicle)
                .Include(c => c.Clan)
                .Include(c => c.Coterie)
                .Include(c => c.Haven)
                .Include(c => c.PredatorType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
