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

namespace FootballProgrammes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FootballProgrammesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IFootballProgrammeService FootballProgrammeService { get; }

        public FootballProgrammesController(ApplicationDbContext context,
            IFootballProgrammeService footballProgrammeService)
        {
            _context = context;
            FootballProgrammeService = footballProgrammeService;
        }

        // GET: Admin/FootballProgrammes
        public async Task<IActionResult> Index()
        {
            return View(await FootballProgrammeService.GetAllFootballProgrammes());
        }

        // GET: Admin/FootballProgrammes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footballProgramme = await FootballProgrammeService.GetFootballProgrammeById(id.Value);
            if (footballProgramme == null)
            {
                return NotFound();
            }

            return View(footballProgramme);
        }

        // GET: Admin/FootballProgrammes/Create
        public IActionResult Create()
        {            
            return View();
        }

        // POST: Admin/FootballProgrammes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FootballProgramme footballProgramme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(footballProgramme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { Area = "", Controller = "Home" });
            }
            return View(footballProgramme);
        }

        // GET: Admin/FootballProgrammes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footballProgramme = await FootballProgrammeService.GetFootballProgrammeById(id.Value);
            if (footballProgramme == null)
            {
                return NotFound();
            }
            
            return View(footballProgramme);
        }

        // POST: Admin/FootballProgrammes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FootballProgramme footballProgramme)
        {
            if (id != footballProgramme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(footballProgramme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FootballProgrammeExists(footballProgramme.Id))
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
            
            return View(footballProgramme);
        }

        // GET: Admin/FootballProgrammes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footballProgramme = await FootballProgrammeService.GetFootballProgrammeById(id.Value);
            if (footballProgramme == null)
            {
                return NotFound();
            }

            return View(footballProgramme);
        }

        // POST: Admin/FootballProgrammes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var footballProgramme = await FootballProgrammeService.GetFootballProgrammeById(id);
            _context.FootballProgrammes.Remove(footballProgramme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FootballProgrammeExists(int id)
        {
            return _context.FootballProgrammes.Any(e => e.Id == id);
        }
    }
}
