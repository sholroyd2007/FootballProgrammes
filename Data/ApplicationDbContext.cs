using FootballProgrammes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballProgrammes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<AwayClub> AwayClubs { get; set; }
        public DbSet<HomeClub> HomeClubs { get; set; }
        public DbSet<FootballProgramme> FootballProgrammes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }        
        public DbSet<MediaFile> MediaFiles { get; set; }        
        public DbSet<Message> Messages { get; set; }        
    }
    
}


