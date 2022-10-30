using System.Text.Json.Serialization;

namespace FootballProgrammes.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CompetitionType
    {
        League,
        Cup,
        Friendly,
        International,
        Other
    }
}
