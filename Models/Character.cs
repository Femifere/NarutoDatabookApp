namespace NarutoDatabookApp.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }

        // Foreign Key for Team
        public int? TeamId { get; set; }
        // Navigation Property to Team
        public Team? Team { get; set; }


        // Many-to-Many Relationship with Specialties
        public ICollection<CharacterSpecialty>? CharacterSpecialties { get; set; }

        // Many-to-Many Relationship with Rankings
        public ICollection<CharacterRanking>? CharacterRankings { get; set; }
    }
}
