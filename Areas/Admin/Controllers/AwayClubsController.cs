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
    public class AwayClubsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AwayClubsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AwayClubs
        public async Task<IActionResult> Index()
        {
            return View(await _context.AwayClubs.ToListAsync());
        }

        // GET: Admin/AwayClubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awayClub = await _context.AwayClubs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (awayClub == null)
            {
                return NotFound();
            }

            return View(awayClub);
        }

        // GET: Admin/AwayClubs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AwayClubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("International,Id,Name,Description")] AwayClub awayClub)
        {
            if (ModelState.IsValid)
            {
                _context.Add(awayClub);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(awayClub);
        }

        // GET: Admin/AwayClubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awayClub = await _context.AwayClubs.FindAsync(id);
            if (awayClub == null)
            {
                return NotFound();
            }
            return View(awayClub);
        }

        // POST: Admin/AwayClubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("International,Id,Name,Description")] AwayClub awayClub)
        {
            if (id != awayClub.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(awayClub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AwayClubExists(awayClub.Id))
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
            return View(awayClub);
        }

        // GET: Admin/AwayClubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awayClub = await _context.AwayClubs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (awayClub == null)
            {
                return NotFound();
            }

            return View(awayClub);
        }

        // POST: Admin/AwayClubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var awayClub = await _context.AwayClubs.FindAsync(id);
            _context.AwayClubs.Remove(awayClub);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AwayClubExists(int id)
        {
            return _context.AwayClubs.Any(e => e.Id == id);
        }
    }
}
