using FootballProgrammes.Models;
using System.Collections.Generic;

namespace FootballProgrammes.ViewModels
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public IEnumerable<FootballProgramme> PublicProgrammes { get; set; }
        public IEnumerable<Ticket> PublicTickets { get; set; }
        public IEnumerable<Book> PublicBooks { get; set; }
    }
}
