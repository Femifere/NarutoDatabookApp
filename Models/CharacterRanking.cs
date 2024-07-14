// File: CharacterRanking.cs
namespace NarutoDatabookApp.Models
{
    public class CharacterRanking
    {
        public int CharacterId { get; set; }
        public Character? Character { get; set; }

        public int RankingId { get; set; }
        public Ranking? Ranking { get; set; }
    }
}
