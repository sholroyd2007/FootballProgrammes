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
using Microsoft.AspNet.Identity;
using System.Text;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Authorization;

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
            var userId = User.Identity.GetUserId();
            var programmes = await FootballProgrammeService.GetFootballProgrammesByUserId(userId);
            return View(programmes);
        }

        public async Task<IActionResult> Tickets()
        {
            var userId = User.Identity.GetUserId();
            var tickets = await FootballProgrammeService.GetTicketsByUserId(userId);
            return View(tickets);
        }

        public async Task<IActionResult> Books()
        {
            var userId = User.Identity.GetUserId();
            var books = await FootballProgrammeService.GetBooksByUserId(userId);
            return View(books);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                var newBook = await FootballProgrammeService.AddBook(book);
                return RedirectToAction(nameof(Index), new { Area = "", Controller = "Home" });
            }
            return View(book);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFootballProgramme(FootballProgramme footballProgramme)
        {
            if (ModelState.IsValid)
            {
                var newFootballProgramme = await FootballProgrammeService.AddFootballProgramme(footballProgramme);
                return RedirectToAction(nameof(Index), new { Area = "", Controller = "Home" });
            }
            return View(footballProgramme);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var newTicket = await FootballProgrammeService.AddTicket(ticket);
                return RedirectToAction(nameof(Index), new { Area = "", Controller = "Home" });
            }

            return View(ticket);
        }

        [Authorize]
        public async Task<IActionResult> ExportUserProgrammesToCSV()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            var builder = new StringBuilder();
            builder.AppendLine("HomeTeam,AwayTeam,Year,Country,Competition,Quality,Description,Date Added,ForSale,Sold,DateSold,Price");
            foreach (var item in await FootballProgrammeService.GetFootballProgrammesByUserId(userId))
            {
                builder.AppendLine($"{item.HomeClub.Name},{item.AwayClub.Name},{item.Year},{item.Country},{item.CompetitionType},{item.Quality},{item.Description},{item.DateAdded.ToString("dd MMMM yyyy")},{(item.ForSale ? "Yes" : "No")},{(item.Sold ? "Yes" : "No")},{item.DateSold?.ToString("dd MMMM yyyy")},{item.Price}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", $"Programme_List_{userName}.csv");
        }

        [Authorize]
        public async Task<IActionResult> ExportUserBooksToCSV()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            var builder = new StringBuilder();
            builder.AppendLine("Name,Author,Description,Date Added,ForSale,Sold,DateSold,Price");
            foreach (var item in await FootballProgrammeService.GetBooksByUserId(userId))
            {
                builder.AppendLine($"{item.Name},{item.Author},{item.Description},{item.DateAdded.ToString("dd MMMM yyyy")},{(item.ForSale ? "Yes" : "No")},{(item.Sold ? "Yes" : "No")},{item.DateSold?.ToString("dd MMMM yyyy")},{item.Price}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", $"Book_List_{userName}.csv");
        }

        [Authorize]
        public async Task<IActionResult> ExportUserTicketsToCSV()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            var builder = new StringBuilder();
            builder.AppendLine("HomeTeam,AwayTeam,Year,Country,Competition,Quality,Description,Date Added,ForSale,Sold,DateSold,Price");
            foreach (var item in await FootballProgrammeService.GetTicketsByUserId(userId))
            {
                builder.AppendLine($"{item.HomeClub.Name},{item.AwayClub.Name},{item.Year},{item.Country},{item.CompetitionType},{item.Quality},{item.Description},{item.DateAdded.ToString("dd MMMM yyyy")},{(item.ForSale ? "Yes" : "No")},{(item.Sold ? "Yes" : "No")},{item.DateSold?.ToString("dd MMMM yyyy")},{item.Price}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", $"Ticket_List_{userName}.csv");
        }

        [Authorize]
        public async Task<IActionResult> ExportUserCollectionToExcel()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName(); 
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Programmes");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "HomeTeam";
                worksheet.Cell(currentRow, 2).Value = "AwayTeam";
                worksheet.Cell(currentRow, 3).Value = "Year";
                worksheet.Cell(currentRow, 4).Value = "Country";
                worksheet.Cell(currentRow, 5).Value = "Competition";
                worksheet.Cell(currentRow, 6).Value = "Quality";
                worksheet.Cell(currentRow, 7).Value = "Description";
                worksheet.Cell(currentRow, 8).Value = "Date Added";
                worksheet.Cell(currentRow, 9).Value = "For Sale";
                worksheet.Cell(currentRow, 10).Value = "Sold";
                worksheet.Cell(currentRow, 11).Value = "Date Sold";
                worksheet.Cell(currentRow, 12).Value = "Price";
                foreach (var item in await FootballProgrammeService.GetFootballProgrammesByUserId(userId))
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item?.HomeClub?.Name;
                    worksheet.Cell(currentRow, 2).Value = item?.AwayClub?.Name;
                    worksheet.Cell(currentRow, 3).Value = item?.Year;
                    worksheet.Cell(currentRow, 4).Value = item?.Country;
                    worksheet.Cell(currentRow, 5).Value = item?.CompetitionType;
                    worksheet.Cell(currentRow, 6).Value = item?.Quality;
                    worksheet.Cell(currentRow, 7).Value = item?.Description;
                    worksheet.Cell(currentRow, 8).Value = item?.DateAdded.ToString("dd MMMM yyyy");
                    worksheet.Cell(currentRow, 9).Value = item.ForSale ? "Yes" : "No";
                    worksheet.Cell(currentRow, 10).Value = item.Sold ? "Yes" : "No";
                    worksheet.Cell(currentRow, 11).Value = item?.DateSold?.ToString("dd MMMM yyyy");
                    worksheet.Cell(currentRow, 12).Value = item?.Price;
                }

                var worksheet2 = workbook.Worksheets.Add("Tickets");
                var currentRow2 = 1;
                worksheet2.Cell(currentRow2, 1).Value = "HomeTeam";
                worksheet2.Cell(currentRow2, 2).Value = "AwayTeam";
                worksheet2.Cell(currentRow2, 3).Value = "Year";
                worksheet2.Cell(currentRow2, 4).Value = "Country";
                worksheet2.Cell(currentRow2, 5).Value = "Competition";
                worksheet2.Cell(currentRow2, 6).Value = "Quality";
                worksheet2.Cell(currentRow2, 7).Value = "Description";
                worksheet2.Cell(currentRow2, 8).Value = "Date Added";
                worksheet2.Cell(currentRow2, 9).Value = "For Sale";
                worksheet2.Cell(currentRow2, 10).Value = "Sold";
                worksheet2.Cell(currentRow2, 11).Value = "Date Sold";
                worksheet2.Cell(currentRow2, 12).Value = "Price";
                foreach (var item in await FootballProgrammeService.GetTicketsByUserId(userId))
                {
                    currentRow2++;
                    worksheet2.Cell(currentRow2, 1).Value = item?.HomeClub?.Name;
                    worksheet2.Cell(currentRow2, 2).Value = item?.AwayClub?.Name;
                    worksheet2.Cell(currentRow2, 3).Value = item?.Year;
                    worksheet2.Cell(currentRow2, 4).Value = item?.Country;
                    worksheet2.Cell(currentRow2, 5).Value = item?.CompetitionType;
                    worksheet2.Cell(currentRow2, 6).Value = item?.Quality;
                    worksheet2.Cell(currentRow2, 7).Value = item?.Description;
                    worksheet2.Cell(currentRow2, 8).Value = item?.DateAdded.ToString("dd MMMM yyyy");
                    worksheet2.Cell(currentRow2, 9).Value = item.ForSale ? "Yes" : "No";
                    worksheet2.Cell(currentRow2, 10).Value = item.Sold ? "Yes" : "No";
                    worksheet2.Cell(currentRow2, 11).Value = item?.DateSold?.ToString("dd MMMM yyyy");
                    worksheet2.Cell(currentRow2, 12).Value = item?.Price;
                }

                var worksheet3 = workbook.Worksheets.Add("Books");
                var currentRow3 = 1;
                worksheet3.Cell(currentRow3, 1).Value = "Name";
                worksheet3.Cell(currentRow3, 2).Value = "Author";
                worksheet3.Cell(currentRow3, 3).Value = "Description";
                worksheet3.Cell(currentRow3, 4).Value = "Date Added";
                worksheet3.Cell(currentRow3, 5).Value = "For Sale";
                worksheet3.Cell(currentRow3, 6).Value = "Sold";
                worksheet3.Cell(currentRow3, 7).Value = "Date Sold";
                worksheet3.Cell(currentRow3, 8).Value = "Price";
                foreach (var item in await FootballProgrammeService.GetBooksByUserId(userId))
                {
                    currentRow3++;
                    worksheet3.Cell(currentRow3, 1).Value = item?.Name;
                    worksheet3.Cell(currentRow3, 2).Value = item?.Author;
                    worksheet3.Cell(currentRow3, 3).Value = item?.Description;
                    worksheet3.Cell(currentRow3, 4).Value = item?.DateAdded.ToString("dd MMMM yyyy");
                    worksheet3.Cell(currentRow3, 5).Value = item.ForSale ? "Yes" : "No";
                    worksheet3.Cell(currentRow3, 6).Value = item.Sold ? "Yes" : "No";
                    worksheet3.Cell(currentRow3, 7).Value = item?.DateSold?.ToString("dd MMMM yyyy");
                    worksheet3.Cell(currentRow3, 8).Value = item?.Price;
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Collection_Full_{userName}.xlsx");
                }
            }
        }

        [Authorize]
        public async Task<IActionResult> SellFootballProgramme(int id)
        {
            var programme = await FootballProgrammeService.GetFootballProgrammeById(id);
            if(programme != null)
            {
                var currentUserId = User.Identity.GetUserId();
                if(currentUserId == programme.UserId)
                {
                    await FootballProgrammeService.SellFootballProgramme(programme);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Unauthorized();
                }
            }
            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> SellBook(int id)
        {
            var book = await FootballProgrammeService.GetBookById(id);
            if (book != null)
            {
                var currentUserId = User.Identity.GetUserId();
                if (currentUserId == book.UserId)
                {
                    await FootballProgrammeService.SellBook(book);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Unauthorized();
                }
            }
            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> SellTicket(int id)
        {
            var ticket = await FootballProgrammeService.GetTicketById(id);
            if (ticket != null)
            {
                var currentUserId = User.Identity.GetUserId();
                if (currentUserId == ticket.UserId)
                {
                    await FootballProgrammeService.SellTicket(ticket);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Unauthorized();
                }
            }
            return NotFound();
        }

    }
}
