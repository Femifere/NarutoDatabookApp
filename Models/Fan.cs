// File: Fan.cs
namespace NarutoDatabookApp.Models
{
    public class Fan
    {
        public int FanId { get; set; }
        public string? Name { get; set; }
        public DateTime DateWatched { get; set; }

        // Foreign Key for Ranking
        public int? RankingId { get; set; }
        // Navigation Property to Ranking
        public Ranking? Ranking { get; set; }
    }
}
