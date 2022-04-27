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
    public class CoteriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public CoteriesController(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: Coteries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coteries.ToListAsync());
        }

        // GET: Coteries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coterie = await _context.Coteries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coterie == null)
            {
                return NotFound();
            }

            return View(coterie);
        }

        // GET: Coteries/Create
        [Authorize]
        public IActionResult Create()
        {
            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.StoryTeller.ToString()))
            {
                ViewData["Characters"] = new SelectList(_context.VtMUsers, "Id", "UserName");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Coteries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Chasse,Lien,Portillon,CoterieType,Publicity,FormFile")] Coterie coterie)
        {
            if (ModelState.IsValid)
            {
                if(coterie.FormFile != null)
                {
                    coterie.FileData = await _imageService.EncodeImageAsync(coterie.FormFile);
                    coterie.FileContentType = _imageService.ContentType(coterie.FormFile);
                    coterie.FileName = coterie.FormFile.FileName;
                }
                _context.Add(coterie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coterie);
        }

        // GET: Coteries/Edit/5
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

                var coterie = await _context.Coteries.FindAsync(id);
                if (coterie == null)
                {
                    return NotFound();
                }
                return View(coterie);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Coteries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Chasse,Lien,Portillon,CoterieType,Publicity")] Coterie coterie, IFormFile formFile)
        {
            
                if (id != coterie.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {

                        Coterie newCoterie = await _context.Coteries.FirstOrDefaultAsync(c => c.Id == coterie.Id);

                        if (newCoterie.Name != coterie.Name) newCoterie.Name = coterie.Name;
                        if (newCoterie.Chasse != coterie.Chasse) newCoterie.Chasse = coterie.Chasse;
                        if (newCoterie.Portillon != coterie.Portillon) newCoterie.Portillon = coterie.Portillon;
                        if (newCoterie.Lien != coterie.Lien) newCoterie.Lien = coterie.Lien;
                        if (newCoterie.CoterieType != coterie.CoterieType) newCoterie.CoterieType = coterie.CoterieType;
                        if (newCoterie.CoterieTenets != coterie.CoterieTenets) newCoterie.CoterieTenets = coterie.CoterieTenets;
                        if (newCoterie.Characters != coterie.Characters) newCoterie.Characters = coterie.Characters;
                        if (newCoterie.Publicity != coterie.Publicity) newCoterie.Publicity = coterie.Publicity;
                        if (newCoterie.Description != coterie.Description) newCoterie.Description = coterie.Description;

                        if (formFile != null)
                        {
                            newCoterie.FileData = await _imageService.EncodeImageAsync(formFile);
                            newCoterie.FileContentType = _imageService.ContentType(formFile);
                            newCoterie.FileName = formFile.FileName;
                        }





                        _context.Update(newCoterie);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CoterieExists(coterie.Id))
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
                return View(coterie);
            
        }

        // GET: Coteries/Delete/5
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

                var coterie = await _context.Coteries
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (coterie == null)
                {
                    return NotFound();
                }

                return View(coterie);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Coteries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coterie = await _context.Coteries.FindAsync(id);
            _context.Coteries.Remove(coterie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoterieExists(int id)
        {
            return _context.Coteries.Any(e => e.Id == id);
        }
    }
}
