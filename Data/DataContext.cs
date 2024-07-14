using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NarutoDatabookApp.Models;
using System.Linq;

namespace NarutoDatabookApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterRanking> CharacterRankings { get; set; }
        public DbSet<CharacterSpecialty> CharacterSpecialties { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Village> Villages { get; set; }

        private readonly ILoggerFactory _loggerFactory;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext(DbContextOptions<DataContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory); // Configure logger factory if provided
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Remove the OneToManyCascadeDeleteConvention
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Configure relationships here

            // Character to Team relationship
            modelBuilder.Entity<Character>()
                .HasOne(c => c.Team)
                .WithMany(t => t.Characters)
                .HasForeignKey(c => c.TeamId)
                .IsRequired(false); // Character can optionally belong to a Team


            // CharacterSpecialty (many-to-many)
            modelBuilder.Entity<CharacterSpecialty>()
                .HasKey(cs => new { cs.CharacterId, cs.SpecialtyId });

            modelBuilder.Entity<CharacterSpecialty>()
                .HasOne(cs => cs.Character)
                .WithMany(c => c.CharacterSpecialties)
                .HasForeignKey(cs => cs.CharacterId);

            modelBuilder.Entity<CharacterSpecialty>()
                .HasOne(cs => cs.Specialty)
                .WithMany(s => s.CharacterSpecialties)
                .HasForeignKey(cs => cs.SpecialtyId);

            // CharacterRanking (many-to-many)
            modelBuilder.Entity<CharacterRanking>()
                .HasKey(cr => new { cr.CharacterId, cr.RankingId });

            modelBuilder.Entity<CharacterRanking>()
                .HasOne(cr => cr.Character)
                .WithMany(c => c.CharacterRankings)
                .HasForeignKey(cr => cr.CharacterId);

            modelBuilder.Entity<CharacterRanking>()
                .HasOne(cr => cr.Ranking)
                .WithMany(r => r.CharacterRankings)
                .HasForeignKey(cr => cr.RankingId);

            // Fan to Ranking relationship
            modelBuilder.Entity<Fan>()
                .HasOne(f => f.Ranking)
                .WithMany(r => r.Fans)
                .HasForeignKey(f => f.RankingId)
                .IsRequired(false); // Ranking is optional for Fan

            // Ensure all properties are included
            modelBuilder.Entity<Ranking>()
                .Property(r => r.Reason)
                .IsRequired(); // Ensure Reason property is required

            modelBuilder.Entity<Ranking>()
                .Property(r => r.Rating)
                .IsRequired(); // Ensure Rating property is required

            base.OnModelCreating(modelBuilder);
        }
    }
}
