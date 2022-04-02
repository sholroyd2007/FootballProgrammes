using FootballProgrammes.Data;
using FootballProgrammes.Models;
using FootballProgrammes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FootballProgrammes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ApplicationDbContext DatabaseContext { get; }
        public IFootballProgrammeService FootballProgrammeService { get; }

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext databaseContext,
            IFootballProgrammeService footballProgrammeService)
        {
            _logger = logger;
            DatabaseContext = databaseContext;
            FootballProgrammeService = footballProgrammeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Programmes()
        {
            return View(await FootballProgrammeService.GetAllFootballProgrammes());
        }

        public async Task<IActionResult> Tickets()
        {
            return View(await FootballProgrammeService.GetAllTickets());
        }

        public async Task<IActionResult> Books()
        {
            return View(await FootballProgrammeService.GetAllBooks());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
