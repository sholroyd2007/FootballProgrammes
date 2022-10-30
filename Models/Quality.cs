using System.Text.Json.Serialization;

namespace FootballProgrammes.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Quality
    {
        Mint = 0,
        VeryGood = 1,
        Good = 2,
        OK = 3,
        Poor = 4
    }
}
