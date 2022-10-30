using FootballProgrammes.Models;
using System;

namespace FootballProgrammes.Dtos.Programmes
{
    public class GetProgrammeDto
    {
        public int Id { get; set; }
        public string HomeClubName { get; set; }
        public string AwayClubName { get; set; }
        public Country Country { get; set; }
        public CompetitionType Competition { get; set; }
        public string Year { get; set; }
        public bool Womens { get; set; }
        public Quality Quality { get; set; }
        public string UserId { get; set; }
        public bool ForSale { get; set; }
        public bool Sold { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateSold { get; set; }
        public string Description { get; set; }
    }
}
