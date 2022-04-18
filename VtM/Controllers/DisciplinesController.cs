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
using VtM.Services.Interfaces;

namespace VtM.Controllers
{
    public class DisciplinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public DisciplinesController(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: Disciplines
        public async Task<IActionResult> Index()
        {

            return View(await _context.Disciplines.ToListAsync());
            
        }

        

        // GET: Disciplines/Create
        [Authorize]
        public IActionResult Create()
        {
            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.StoryTeller.ToString()))
                return View();
            return RedirectToAction(nameof(Index));
        }

        // POST: Disciplines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,FormFile")] Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                if (discipline.FormFile != null)
                {
                    discipline.FileData = await _imageService.EncodeImageAsync(discipline.FormFile);
                    discipline.FileContentType = _imageService.ContentType(discipline.FormFile);
                    discipline.FileName = discipline.FormFile.FileName;
                }
                _context.Add(discipline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discipline);
        }

        // GET: Disciplines/Edit/5
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

                var discipline = await _context.Disciplines.FindAsync(id);
                if (discipline == null)
                {
                    return NotFound();
                }
                return View(discipline);
            }
                
            return RedirectToAction(nameof(Index));
        }

        // POST: Disciplines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Discipline discipline, IFormFile formFile)
        {
            if (id != discipline.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Discipline newDiscipline = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == discipline.Id);

                    if(newDiscipline.Name != discipline.Name) newDiscipline.Name = discipline.Name;

                    if(formFile != null)
                    {
                        newDiscipline.FileData = await _imageService.EncodeImageAsync(formFile);
                        newDiscipline.FileContentType = _imageService.ContentType(formFile);
                        newDiscipline.FileName = formFile.FileName;
                    }




                    _context.Update(newDiscipline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplineExists(discipline.Id))
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
            return View(discipline);
        }

        // GET: Disciplines/Delete/5
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

                var discipline = await _context.Disciplines
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (discipline == null)
                {
                    return NotFound();
                }

                return View(discipline);
            }
                
            return RedirectToAction(nameof(Index));
            
        }

        // POST: Disciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discipline = await _context.Disciplines.FindAsync(id);
            _context.Disciplines.Remove(discipline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplineExists(int id)
        {
            return _context.Disciplines.Any(e => e.Id == id);
        }
    }
}
