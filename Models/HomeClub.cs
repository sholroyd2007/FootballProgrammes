using CsvHelper.Configuration.Attributes;

namespace FootballProgrammes.Models
{
    public class HomeClub : Entity
    {
        [Index(1)]
        public bool International { get; set; }
    }
}
