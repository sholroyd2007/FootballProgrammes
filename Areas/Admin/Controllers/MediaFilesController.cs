using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballProgrammes.Data;
using FootballProgrammes.Models;

namespace FootballProgrammes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MediaFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MediaFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MediaFiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MediaFiles.Include(m => m.Book).Include(m => m.FootballProgramme).Include(m => m.Ticket);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/MediaFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaFile = await _context.MediaFiles
                .Include(m => m.Book)
                .Include(m => m.FootballProgramme)
                .Include(m => m.Ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaFile == null)
            {
                return NotFound();
            }

            return View(mediaFile);
        }

        // GET: Admin/MediaFiles/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            ViewData["FootballProgrammeId"] = new SelectList(_context.FootballProgrammes, "Id", "Id");
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Id");
            return View();
        }

        // POST: Admin/MediaFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Data,ContentType,DateAdded,FootballProgrammeId,TicketId,BookId,Id,Name,Description")] MediaFile mediaFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediaFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", mediaFile.BookId);
            ViewData["FootballProgrammeId"] = new SelectList(_context.FootballProgrammes, "Id", "Id", mediaFile.FootballProgrammeId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Id", mediaFile.TicketId);
            return View(mediaFile);
        }

        // GET: Admin/MediaFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaFile = await _context.MediaFiles.FindAsync(id);
            if (mediaFile == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", mediaFile.BookId);
            ViewData["FootballProgrammeId"] = new SelectList(_context.FootballProgrammes, "Id", "Id", mediaFile.FootballProgrammeId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Id", mediaFile.TicketId);
            return View(mediaFile);
        }

        // POST: Admin/MediaFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Data,ContentType,DateAdded,FootballProgrammeId,TicketId,BookId,Id,Name,Description")] MediaFile mediaFile)
        {
            if (id != mediaFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaFileExists(mediaFile.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", mediaFile.BookId);
            ViewData["FootballProgrammeId"] = new SelectList(_context.FootballProgrammes, "Id", "Id", mediaFile.FootballProgrammeId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Id", mediaFile.TicketId);
            return View(mediaFile);
        }

        // GET: Admin/MediaFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaFile = await _context.MediaFiles
                .Include(m => m.Book)
                .Include(m => m.FootballProgramme)
                .Include(m => m.Ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaFile == null)
            {
                return NotFound();
            }

            return View(mediaFile);
        }

        // POST: Admin/MediaFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaFile = await _context.MediaFiles.FindAsync(id);
            _context.MediaFiles.Remove(mediaFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaFileExists(int id)
        {
            return _context.MediaFiles.Any(e => e.Id == id);
        }
    }
}
