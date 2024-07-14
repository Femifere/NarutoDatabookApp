namespace NarutoDatabookApp.Dto
{
    public class CharacterDto
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int? TeamId { get; set; }
    }
}
