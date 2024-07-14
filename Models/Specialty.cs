// File: Specialty.cs
namespace NarutoDatabookApp.Models
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public string? Name { get; set; }
        public string? Rarity { get; set; }

        // Many-to-Many Relationship with Characters
        public ICollection<CharacterSpecialty>? CharacterSpecialties { get; set; }
    }
}
