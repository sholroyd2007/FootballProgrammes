using FootballProgrammes.Data;
using FootballProgrammes.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity;
using FootballProgrammes.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FootballProgrammes.Controllers
{
    public class PublicController : Controller
    {
        public PublicController(ApplicationDbContext databaseContext,
            IFootballProgrammeService footballProgrammeService)
        {
            DatabaseContext = databaseContext;
            FootballProgrammeService = footballProgrammeService;
        }

        public ApplicationDbContext DatabaseContext { get; }
        public IFootballProgrammeService FootballProgrammeService { get; }

        public async Task<IActionResult> Index()
        {
            var users = await DatabaseContext.Users.AsNoTracking().ToListAsync(); 
            return View(users);
        }

        [Authorize]
        public async Task<IActionResult> Profile(string id)
        {
            var user = await DatabaseContext.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (user != null)
            {
                var profileVM = new ProfileViewModel();
                var programmes = await FootballProgrammeService.GetFootballProgrammesByUserId(user.Id);
                var tickets = await FootballProgrammeService.GetTicketsByUserId(user.Id);
                var books = await FootballProgrammeService.GetBooksByUserId(user.Id);

                profileVM.UserName = user.UserName;
                profileVM.PublicProgrammes = programmes.Where(e => e.ForSale);
                profileVM.PublicBooks = books.Where(e => e.ForSale);
                profileVM.PublicTickets = tickets.Where(e => e.ForSale);

                return View(profileVM);
            }

            return NotFound();
            
        }
    }
}
