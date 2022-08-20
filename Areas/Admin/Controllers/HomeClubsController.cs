using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballProgrammes.Data;
using FootballProgrammes.Models;
using FootballProgrammes.Services;
using Microsoft.AspNetCore.Authorization;

namespace FootballProgrammes.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeClubsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IFootballProgrammeService FootballProgrammeService { get; }

        public HomeClubsController(ApplicationDbContext context,
            IFootballProgrammeService footballProgrammeService)
        {
            _context = context;
            FootballProgrammeService = footballProgrammeService;
        }

        // GET: Admin/HomeClubs
        public async Task<IActionResult> Index()
        {
            return View(await FootballProgrammeService.GetAllHomeClubs());
        }

        // GET: Admin/HomeClubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeClub = await FootballProgrammeService.GetHomeClubById(id.Value);
            if (homeClub == null)
            {
                return NotFound();
            }

            return View(homeClub);
        }

        // GET: Admin/HomeClubs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HomeClubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeClub homeClub)
        {
            if (ModelState.IsValid)
            {
                _context.HomeClubs.Add(homeClub);
                await _context.SaveChangesAsync();

                var awayClub = new AwayClub()
                {
                    Name = homeClub.Name,
                    International = homeClub.International
                };
                _context.AwayClubs.Add(awayClub);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { Area = "", Controller = "Home"});
            }
            return View(homeClub);
        }

        // GET: Admin/HomeClubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeClub = await FootballProgrammeService.GetHomeClubById(id.Value);
            if (homeClub == null)
            {
                return NotFound();
            }
            return View(homeClub);
        }

        // POST: Admin/HomeClubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HomeClub homeClub)
        {
            if (id != homeClub.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeClub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeClubExists(homeClub.Id))
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
            return View(homeClub);
        }

        // GET: Admin/HomeClubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeClub = await FootballProgrammeService.GetHomeClubById(id.Value);
            if (homeClub == null)
            {
                return NotFound();
            }

            return View(homeClub);
        }

        // POST: Admin/HomeClubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeClub = await FootballProgrammeService.GetHomeClubById(id);
            _context.HomeClubs.Remove(homeClub);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeClubExists(int id)
        {
            return _context.HomeClubs.Any(e => e.Id == id);
        }
    }
}
