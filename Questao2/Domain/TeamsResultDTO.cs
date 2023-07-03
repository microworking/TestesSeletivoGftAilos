using System.Text.Json.Serialization;

namespace Questao2.Domain
{
    public class TeamsResultDTO
    {
        [JsonPropertyName("competition")]
        public string? Competition { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("round")]
        public string? Round { get; set; }

        [JsonPropertyName("team1")]
        public string? Team1 { get; set; }

        [JsonPropertyName("team2")]
        public string? Team2 { get; set; }

        [JsonPropertyName("team1goals")]
        public int Team1goals { get; set; }

        [JsonPropertyName("team2goals")]
        public int Team2goals { get; set; }
    }
}