using System;

namespace FootballProgrammes.Models
{
    public class Book : Entity
    {
        public string Author { get; set; }

        public string UserId { get; set; }

        public bool ForSale { get; set; }

        public bool Sold { get; set; }

        public decimal? Price { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateSold { get; set; }
    }
}
