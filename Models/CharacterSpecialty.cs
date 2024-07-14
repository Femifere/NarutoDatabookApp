// File: CharacterSpecialty.cs
namespace NarutoDatabookApp.Models
{
    public class CharacterSpecialty
    {
        public int CharacterId { get; set; }
        public Character? Character { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }
    }
}
