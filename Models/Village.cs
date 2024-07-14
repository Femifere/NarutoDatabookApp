namespace NarutoDatabookApp.Models
{
    public class Village
    {
        public int VillageId { get; set; }
        public string? Name { get; set; }
        public int Population { get; set; }

        // Collection of Teams in the Village
        public ICollection<Team>? Teams { get; set; }

    }
}
