using FootballProgrammes.Data;
using FootballProgrammes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Task<IEnumerable<FootballProgramme>> GetFootballProgrammesByUserId(string id);
        Task<IEnumerable<Book>> GetBooksByUserId(string id);
        Task<IEnumerable<Ticket>> GetTicketsByUserId(string id);
        Task<HomeClub> GetHomeClubById(int id);
        Task<AwayClub> GetAwayClubById(int id);
        Task<FootballProgramme> GetFootballProgrammeById(int id);
        Task<Book> GetBookById(int id);
        Task<Ticket> GetTicketById(int id);
        Task SellFootballProgramme(FootballProgramme footballProgramme);
        Task SellBook(Book book);
        Task SellTicket(Ticket ticket);
        Task<Book>AddBook (Book book);
        Task<Ticket>AddTicket(Ticket ticket);
        Task<FootballProgramme>AddFootballProgramme(FootballProgramme footballProgramme);


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

        public async Task<IEnumerable<Book>> GetBooksByUserId(string id)
        {
            return await DatabaseContext.Books.AsNoTracking().Where(e=>e.UserId == id).ToListAsync();
        }

        public async Task<IEnumerable<FootballProgramme>> GetAllFootballProgrammes()
        {
            return await DatabaseContext.FootballProgrammes
                .AsNoTracking()
                .Include(e=>e.HomeClub)
                .Include(e=>e.AwayClub)
                .ToListAsync();
        }

        public async Task<IEnumerable<FootballProgramme>> GetFootballProgrammesByUserId(string id)
        {
            return await DatabaseContext.FootballProgrammes
                .AsNoTracking()
                .Include(e => e.HomeClub)
                .Include(e => e.AwayClub)
                .Where(e => e.UserId == id)
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

        public async Task<IEnumerable<Ticket>> GetTicketsByUserId(string id)
        {
            return await DatabaseContext.Tickets
                .AsNoTracking()
                .Include(e => e.HomeClub)
                .Include(e => e.AwayClub)
                .Where(e => e.UserId == id)
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

        public async Task SellFootballProgramme(FootballProgramme footballProgramme)
        {
            footballProgramme.ForSale = true;
            DatabaseContext.Update(footballProgramme);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task SellBook(Book book)
        {
            book.ForSale = true;
            DatabaseContext.Update(book);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task SellTicket(Ticket ticket)
        {
            ticket.ForSale = true;
            DatabaseContext.Update(ticket);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task<Book> AddBook(Book book)
        {
            book.DateAdded = DateTime.UtcNow.ToLocalTime();
            DatabaseContext.Add(book);
            await DatabaseContext.SaveChangesAsync();
            return book;
        }

        public async Task<Ticket> AddTicket(Ticket ticket)
        {
            ticket.DateAdded = DateTime.UtcNow.ToLocalTime();
            DatabaseContext.Add(ticket);
            await DatabaseContext.SaveChangesAsync();
            return ticket;
        }

        public async Task<FootballProgramme> AddFootballProgramme(FootballProgramme footballProgramme)
        {
            footballProgramme.DateAdded = DateTime.UtcNow.ToLocalTime();
            DatabaseContext.Add(footballProgramme);
            await DatabaseContext.SaveChangesAsync();
            return footballProgramme;
        }
    }
}
