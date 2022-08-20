using System;

namespace FootballProgrammes.Models
{
    public class FootballProgramme : Entity
    {
        public HomeClub HomeClub { get; set; }

        public int HomeClubId { get; set; }

        public AwayClub AwayClub { get; set; }

        public int AwayClubId { get; set; }

        public string Year { get; set; }

        public Country Country { get; set; }

        public CompetitionType CompetitionType { get; set; }

        public Quality Quality { get; set; }

        public bool Womens { get; set; }

        public string UserId { get; set; }

        public bool ForSale { get; set; }

        public bool Sold { get; set; }

        public decimal? Price { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateSold { get; set; }
    }
}
