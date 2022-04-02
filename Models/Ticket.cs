namespace FootballProgrammes.Models
{
    public class Ticket : Entity
    {
        public HomeClub HomeClub { get; set; }

        public int HomeClubId { get; set; }

        public AwayClub AwayClub { get; set; }

        public int AwayClubId { get; set; }

        public string Year { get; set; }

        public string Country { get; set; }

        public CompetitionType CompetitionType { get; set; }

        public Quality Quality { get; set; }

        public bool Womens { get; set; }
    }
}
