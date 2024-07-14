// File: Ranking.cs
namespace NarutoDatabookApp.Models
{
    public class Ranking
    {
        public int RankingId { get; set; }
        public string? Name { get; set; }

        // Many-to-Many Relationship with Characters
        public ICollection<CharacterRanking>? CharacterRankings { get; set; }

        // Navigation Property for Fans
        public ICollection<Fan>? Fans { get; set; }
        public string Reason { get; set; } // Ensure this property exists
        public int Rating { get; set; } // Ensure this property exists
    }
}
