namespace NarutoDatabookApp.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string? Name { get; set; }

        // Foreign Key for Village
        public int? VillageId { get; set; }
        // Navigation Property to Village
        public Village? Village { get; set; }

        // Collection of Characters in the Team
        public ICollection<Character>? Characters { get; set; }
        public int NumberOfMembers { get; set; }
    }
}
