using FootballProgrammes.Data;
using FootballProgrammes.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballProgrammes.Services
{
    public interface IFootballProgrammeService
    {
        Task<IEnumerable<HomeClub>> GetAllHomeClubs();
        Task<IEnumerable<AwayClub>> GetAllAwayClubs();
        Task<IEnumerable<FootballProgramme>> GetAllFootballProgrammes();
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Ticket>> GetAllTickets();

        Task<HomeClub> GetHomeClubById(int id);
        Task<AwayClub> GetAwayClubById(int id);
        Task<FootballProgramme> GetFootballProgrammeById(int id);
        Task<Book> GetBookById(int id);
        Task<Ticket> GetTicketById(int id);
        
    }

    public class FootballProgrammeService : IFootballProgrammeService
    {
        public FootballProgrammeService(ApplicationDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public ApplicationDbContext DatabaseContext { get; }

        public async Task<IEnumerable<AwayClub>> GetAllAwayClubs()
        {
            return await DatabaseContext.AwayClubs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await DatabaseContext.Books.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FootballProgramme>> GetAllFootballProgrammes()
        {
            return await DatabaseContext.FootballProgrammes
                .AsNoTracking()
                .Include(e=>e.HomeClub)
                .Include(e=>e.AwayClub)
                .ToListAsync();
        }

        public async Task<IEnumerable<HomeClub>> GetAllHomeClubs()
        {
            return await DatabaseContext.HomeClubs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            return await DatabaseContext.Tickets
                .AsNoTracking()
                .Include(e => e.HomeClub)
                .Include(e => e.AwayClub)
                .ToListAsync();
        }

        public async Task<AwayClub> GetAwayClubById(int id)
        {
            return await DatabaseContext.AwayClubs.AsNoTracking().FirstOrDefaultAsync(e=>e.Id == id);
        }

        public async Task<Book> GetBookById(int id)
        {
            return await DatabaseContext.Books.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<FootballProgramme> GetFootballProgrammeById(int id)
        {
            return await DatabaseContext.FootballProgrammes
                .AsNoTracking()
                .Include(e => e.HomeClub)
                .Include(e => e.AwayClub)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<HomeClub> GetHomeClubById(int id)
        {
            return await DatabaseContext.HomeClubs.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            return await DatabaseContext.Tickets.AsNoTracking()
                .Include(e => e.HomeClub)
                .Include(e => e.AwayClub)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
