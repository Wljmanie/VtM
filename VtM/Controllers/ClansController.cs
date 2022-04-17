#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VtM.Data;
using VtM.Enums;
using VtM.Models;
using VtM.Services;
using VtM.Services.Interfaces;

namespace VtM.Controllers
{
    public class ClansController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public ClansController(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: Clans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Clans.Include(c => c.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Clans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clan = await _context.Clans
                .Include(c => c.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clan == null)
            {
                return NotFound();
            }

            return View(clan);
        }

        // GET: Clans/Create
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

        // POST: Clans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Bane,Compulsion,BookId,FormFile")] Clan clan)
        {
            
            if (ModelState.IsValid)
            {
                if(clan.FormFile != null)
                {
                    clan.FileData = await _imageService.EncodeImageAsync(clan.FormFile);
                    clan.FileContentType = _imageService.ContentType(clan.FormFile);
                    clan.FileName = clan.FormFile.FileName;
                }


                _context.Add(clan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", clan.BookId);
            return View(clan);
        }

        // GET: Clans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.StoryTeller.ToString()))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var clan = await _context.Clans.FindAsync(id);
                if (clan == null)
                {
                    return NotFound();
                }
                ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", clan.BookId);
                return View(clan);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Clans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Bane,Compulsion,BookId,FileName,FileData,FileContentType")] Clan clan)
        {
            if (id != clan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClanExists(clan.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", clan.BookId);
            return View(clan);
        }

        // GET: Clans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.IsInRole(Roles.Admin.ToString())
                || User.IsInRole(Roles.StoryTeller.ToString()))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var clan = await _context.Clans
                    .Include(c => c.Book)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (clan == null)
                {
                    return NotFound();
                }

                return View(clan);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Clans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clan = await _context.Clans.FindAsync(id);
            _context.Clans.Remove(clan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClanExists(int id)
        {
            return _context.Clans.Any(e => e.Id == id);
        }
    }
}
