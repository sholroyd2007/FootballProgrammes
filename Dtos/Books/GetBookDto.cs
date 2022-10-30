using FootballProgrammes.Models;
using System;

namespace FootballProgrammes.Dtos.Books
{
    public class GetBookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public bool ForSale { get; set; }
        public bool Sold { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateSold { get; set; }
    }
}
