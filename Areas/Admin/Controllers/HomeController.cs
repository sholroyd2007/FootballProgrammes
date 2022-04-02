using ClosedXML.Excel;
using FootballProgrammes.Data;
using FootballProgrammes.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FootballProgrammes.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class HomeController : Controller
    {
        public HomeController(ApplicationDbContext databaseContext,
            IFootballProgrammeService footballProgrammeService)
        {
            DatabaseContext = databaseContext;
            FootballProgrammeService = footballProgrammeService;
        }

        public ApplicationDbContext DatabaseContext { get; }
        public IFootballProgrammeService FootballProgrammeService { get; }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ExportProgrammesToCSV()
        {
            var builder = new StringBuilder();
            builder.AppendLine("HomeTeam,AwayTeam,Year,Country,Competition,Quality,Description");
            foreach(var item in await FootballProgrammeService.GetAllFootballProgrammes())
            {
                builder.AppendLine($"{item.HomeClub.Name},{item.AwayClub.Name},{item.Year},{item.Country},{item.CompetitionType},{item.Quality},{item.Description}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Programme_List.csv");
        }

        public async Task<IActionResult> ExportBooksToCSV()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Name,Author,Description");
            foreach (var item in await FootballProgrammeService.GetAllBooks())
            {
                builder.AppendLine($"{item.Name},{item.Author},{item.Description}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Book_List.csv");
        }

        public async Task<IActionResult> ExportTicketsToCSV()
        {
            var builder = new StringBuilder();
            builder.AppendLine("HomeTeam,AwayTeam,Year,Country,Competition,Quality,Description");
            foreach (var item in await FootballProgrammeService.GetAllTickets())
            {
                builder.AppendLine($"{item.HomeClub.Name},{item.AwayClub.Name},{item.Year},{item.Country},{item.CompetitionType},{item.Quality},{item.Description}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Ticket_List.csv");
        }

        public async Task<IActionResult> ExportAllToExcel()
        {
            using(var workbook = new XLWorkbook())
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
                foreach(var item in await FootballProgrammeService.GetAllFootballProgrammes())
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item?.HomeClub?.Name;
                    worksheet.Cell(currentRow, 2).Value = item?.AwayClub?.Name;
                    worksheet.Cell(currentRow, 3).Value = item?.Year;
                    worksheet.Cell(currentRow, 4).Value = item?.Country;
                    worksheet.Cell(currentRow, 5).Value = item?.CompetitionType;
                    worksheet.Cell(currentRow, 6).Value = item?.Quality;
                    worksheet.Cell(currentRow, 7).Value = item?.Description;
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
                foreach (var item in await FootballProgrammeService.GetAllTickets())
                {
                    currentRow2++;
                    worksheet2.Cell(currentRow2, 1).Value = item?.HomeClub?.Name;
                    worksheet2.Cell(currentRow2, 2).Value = item?.AwayClub?.Name;
                    worksheet2.Cell(currentRow2, 3).Value = item?.Year;
                    worksheet2.Cell(currentRow2, 4).Value = item?.Country;
                    worksheet2.Cell(currentRow2, 5).Value = item?.CompetitionType;
                    worksheet2.Cell(currentRow2, 6).Value = item?.Quality;
                    worksheet2.Cell(currentRow2, 7).Value = item?.Description;
                }

                var worksheet3 = workbook.Worksheets.Add("Books");
                var currentRow3 = 1;
                worksheet3.Cell(currentRow3, 1).Value = "Name";
                worksheet3.Cell(currentRow3, 2).Value = "Author";
                worksheet3.Cell(currentRow3, 3).Value = "Description";
                foreach (var item in await FootballProgrammeService.GetAllBooks())
                {
                    currentRow3++;
                    worksheet3.Cell(currentRow3, 1).Value = item?.Name;
                    worksheet3.Cell(currentRow3, 2).Value = item?.Author;
                    worksheet3.Cell(currentRow3, 3).Value = item?.Description;
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Programme_List_Full.xlsx");
                }
            }
        }

    }
}
