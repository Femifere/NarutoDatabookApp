using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NarutoDatabookApp.Data;
using NarutoDatabookApp.Models;
using System.Linq;

namespace NarutoDatabookApp
{
    public class Seed
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<Seed> _logger;

        public Seed(DataContext dataContext, ILogger<Seed> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public void SeedDataContext()
        {
            _logger.LogInformation("Starting to seed data...");

            if (!_dataContext.Characters.Any())
            {
                // Villages
                var villages = new List<Village>
                {
                    new Village { Name = "Leaf Village", Population = 100000 },
                    new Village { Name = "Sand Village", Population = 80000 },
                    new Village { Name = "Mist Village", Population = 50000 },
                    new Village { Name = "Stone Village", Population = 45000 },
                    new Village { Name = "Cloud Village", Population = 60000 },
                    // Add more villages as needed
                };

                _logger.LogInformation("Adding villages...");
                _dataContext.Villages.AddRange(villages);
                _dataContext.SaveChanges();

                // Teams
                var teams = new List<Team>
                {
                    new Team { Name = "Team 7", NumberOfMembers = 3, VillageId = 1 },
                    new Team { Name = "Team 10", NumberOfMembers = 3, VillageId = 1 },
                    new Team { Name = "Team Guy", NumberOfMembers = 4, VillageId = 1 },
                    new Team { Name = "Team Kurenai", NumberOfMembers = 4, VillageId = 1 },
                    new Team { Name = "Akatsuki", NumberOfMembers = 10, VillageId = null }, // Akatsuki is not tied to one village
                    // Add more teams as needed
                };

                _logger.LogInformation("Adding teams...");
                _dataContext.Teams.AddRange(teams);
                _dataContext.SaveChanges();

                // Characters
                var characters = new List<Character>
                {
                    new Character { Name = "Naruto Uzumaki", BirthDate = new DateTime(1997, 10, 10), TeamId = 1 },
                    new Character { Name = "Sasuke Uchiha", BirthDate = new DateTime(1997, 7, 23), TeamId = 1 },
                    new Character { Name = "Sakura Haruno", BirthDate = new DateTime(1997, 3, 28), TeamId = 1 },
                    new Character { Name = "Kakashi Hatake", BirthDate = new DateTime(1985, 9, 15), TeamId = 1 },
                    new Character { Name = "Shikamaru Nara", BirthDate = new DateTime(1997, 9, 22), TeamId = 2 },
                    new Character { Name = "Ino Yamanaka", BirthDate = new DateTime(1997, 9, 23), TeamId = 2 },
                    new Character { Name = "Choji Akimichi", BirthDate = new DateTime(1997, 5, 1), TeamId = 2 },
                    new Character { Name = "Neji Hyuga", BirthDate = new DateTime(1997, 7, 3), TeamId = 3 },
                    new Character { Name = "Rock Lee", BirthDate = new DateTime(1997, 11, 27), TeamId = 3 },
                    new Character { Name = "Tenten", BirthDate = new DateTime(1997, 3, 9), TeamId = 3 },
                    new Character { Name = "Might Guy", BirthDate = new DateTime(1978, 1, 1), TeamId = 3 },
                    new Character { Name = "Kurenai Yuhi", BirthDate = new DateTime(1978, 6, 11), TeamId = 4 },
                    new Character { Name = "Hinata Hyuga", BirthDate = new DateTime(1997, 12, 27), TeamId = 4 },
                    new Character { Name = "Kiba Inuzuka", BirthDate = new DateTime(1997, 7, 7), TeamId = 4 },
                    new Character { Name = "Shino Aburame", BirthDate = new DateTime(1998, 1, 23), TeamId = 4 },
                    // Villains
                    new Character { Name = "Pain", BirthDate = new DateTime(1985, 9, 19), TeamId = 5 },
                    new Character { Name = "Itachi Uchiha", BirthDate = new DateTime(1984, 6, 9), TeamId = 5 },
                    new Character { Name = "Kisame Hoshigaki", BirthDate = new DateTime(1975, 3, 18), TeamId = 5 },
                    new Character { Name = "Deidara", BirthDate = new DateTime(1988, 5, 5), TeamId = 5 },
                    new Character { Name = "Konan", BirthDate = new DateTime(1985, 2, 20), TeamId = 5 },
                    // Add more characters as needed
                };

                _logger.LogInformation("Adding characters...");
                _dataContext.Characters.AddRange(characters);
                _dataContext.SaveChanges();

                // Specialties
                var specialties = new List<Specialty>
                {
                    new Specialty { Name = "Shadow Clone Jutsu", Rarity = "S" },
                    new Specialty { Name = "Rasengan", Rarity = "S" },
                    new Specialty { Name = "Chidori", Rarity = "S" },
                    new Specialty { Name = "Byakugan", Rarity = "S" },
                    new Specialty { Name = "Sharingan", Rarity = "S" },
                    new Specialty { Name = "Fire Style: Fireball Jutsu", Rarity = "A" },
                    new Specialty { Name = "Water Style: Water Dragon Jutsu", Rarity = "A" },
                    new Specialty { Name = "Earth Style: Mud Wall", Rarity = "B" },
                    new Specialty { Name = "Wind Style: Great Breakthrough", Rarity = "B" },
                    new Specialty { Name = "Lightning Style: Lightning Blade", Rarity = "A" },
                    new Specialty { Name = "Eight Gates", Rarity = "S" },
                    new Specialty { Name = "Mind Transfer Jutsu", Rarity = "A" },
                    new Specialty { Name = "Expansion Jutsu", Rarity = "B" },
                    new Specialty { Name = "Insect Manipulation", Rarity = "B" },
                    // Add more specialties as needed
                };

                _logger.LogInformation("Adding specialties...");
                _dataContext.Specialties.AddRange(specialties);
                _dataContext.SaveChanges();

                // Rankings
                var rankings = new List<Ranking>
                {
                    new Ranking { Name = "Genin", Reason = "Entry level ninja", Rating = 3 },
                    new Ranking { Name = "Chunin", Reason = "Intermediate ninja", Rating = 4 },
                    new Ranking { Name = "Jonin", Reason = "Elite ninja", Rating = 5 },
                    new Ranking { Name = "Kage", Reason = "Village leader", Rating = 6 },
                    // Add more rankings as needed
                };

                _logger.LogInformation("Adding rankings...");
                _dataContext.Rankings.AddRange(rankings);
                _dataContext.SaveChanges();

                // Fans
                var fans = new List<Fan>
                {
                    new Fan { Name = "Fan1", DateWatched = DateTime.UtcNow, RankingId = 1 },
                    new Fan { Name = "Fan2", DateWatched = DateTime.UtcNow, RankingId = 2 },
                    // Add more fans as needed
                };

                _logger.LogInformation("Adding fans...");
                _dataContext.Fans.AddRange(fans);
                _dataContext.SaveChanges();

                // CharacterRankings (many-to-many)
                var characterRankings = new List<CharacterRanking>
                {
                    new CharacterRanking { CharacterId = 1, RankingId = 4 }, // Naruto Uzumaki - Hokage
                    new CharacterRanking { CharacterId = 2, RankingId = 3 }, // Sasuke Uchiha - Elite ninja
                    new CharacterRanking { CharacterId = 3, RankingId = 3 }, // Sakura Haruno - Elite ninja
                    new CharacterRanking { CharacterId = 4, RankingId = 4 }, // Kakashi Hatake - Hokage
                    new CharacterRanking { CharacterId = 5, RankingId = 3 }, // Shikamaru Nara - Intermediate ninja
                    new CharacterRanking { CharacterId = 6, RankingId = 3 }, // Ino Yamanaka - Intermediate ninja
                    new CharacterRanking { CharacterId = 7, RankingId = 3 }, // Choji Akimichi - Intermediate ninja
                    new CharacterRanking { CharacterId = 8, RankingId = 2 }, // Neji Hyuga - Elite ninja
                    new CharacterRanking { CharacterId = 9, RankingId = 3 }, // Rock Lee - Intermediate ninja
                    new CharacterRanking { CharacterId = 10, RankingId = 3 }, // Tenten - Intermediate ninja
                    new CharacterRanking { CharacterId = 11, RankingId = 3 }, // Might Guy - Elite ninja
                    new CharacterRanking { CharacterId = 12, RankingId = 3 }, // Kurenai Yuhi - Elite ninja
                    new CharacterRanking { CharacterId = 13, RankingId = 3 }, // Hinata Hyuga - Intermediate ninja
                    new CharacterRanking { CharacterId = 14, RankingId = 3 }, // Kiba Inuzuka - Intermediate ninja
                    new CharacterRanking { CharacterId = 15, RankingId = 3 }
                };


                _logger.LogInformation("Adding character rankings...");
                _dataContext.CharacterRankings.AddRange(characterRankings);
                _dataContext.SaveChanges();

                // CharacterSpecialties (many-to-many)
                var characterSpecialties = new List<CharacterSpecialty>
        {
            new CharacterSpecialty { CharacterId = 1, SpecialtyId = 1 }, // Naruto - Shadow Clone Jutsu
            new CharacterSpecialty { CharacterId = 1, SpecialtyId = 2 }, // Naruto - Rasengan
            new CharacterSpecialty { CharacterId = 2, SpecialtyId = 3 }, // Sasuke - Chidori
            new CharacterSpecialty { CharacterId = 2, SpecialtyId = 5 }, // Sasuke - Sharingan
            new CharacterSpecialty { CharacterId = 3, SpecialtyId = 1 }, // Sakura - Shadow Clone Jutsu
            new CharacterSpecialty { CharacterId = 4, SpecialtyId = 5 }, // Kakashi - Sharingan
            new CharacterSpecialty { CharacterId = 4, SpecialtyId = 10 }, // Kakashi - Lightning Blade
            new CharacterSpecialty { CharacterId = 5, SpecialtyId = 1 }, // Shikamaru - Shadow Clone Jutsu
            new CharacterSpecialty { CharacterId = 5, SpecialtyId = 11 }, // Shikamaru - Mind Transfer Jutsu
            new CharacterSpecialty { CharacterId = 6, SpecialtyId = 12 }, // Ino - Mind Transfer Jutsu
            new CharacterSpecialty { CharacterId = 7, SpecialtyId = 13 }, // Choji - Expansion Jutsu
            new CharacterSpecialty { CharacterId = 8, SpecialtyId = 4 }, // Neji - Byakugan
            new CharacterSpecialty { CharacterId = 9, SpecialtyId = 10 }, // Rock Lee - Eight Gates
            new CharacterSpecialty { CharacterId = 10, SpecialtyId = 1 }, // Tenten - Shadow Clone Jutsu
            new CharacterSpecialty { CharacterId = 11, SpecialtyId = 10 }, // Might Guy - Eight Gates
            new CharacterSpecialty { CharacterId = 12, SpecialtyId = 1 }, // Kurenai - Shadow Clone Jutsu
            new CharacterSpecialty { CharacterId = 13, SpecialtyId = 4 }, // Hinata - Byakugan
            new CharacterSpecialty { CharacterId = 14, SpecialtyId = 1 }, // Kiba - Shadow Clone Jutsu
            new CharacterSpecialty { CharacterId = 15, SpecialtyId = 14 }, // Shino - Insect Manipulation
            new CharacterSpecialty { CharacterId = 16, SpecialtyId = 10 }, // Pain - Lightning Blade
            new CharacterSpecialty { CharacterId = 16, SpecialtyId = 5 }, // Pain - Sharingan
            new CharacterSpecialty { CharacterId = 17, SpecialtyId = 5 }, // Itachi - Sharingan
            new CharacterSpecialty { CharacterId = 17, SpecialtyId = 6 }, // Itachi - Fire Style: Fireball Jutsu
            new CharacterSpecialty { CharacterId = 18, SpecialtyId = 7 }, // Kisame - Water Style: Water Dragon Jutsu
            new CharacterSpecialty { CharacterId = 19, SpecialtyId = 6 }, // Deidara - Fire Style: Fireball Jutsu
            new CharacterSpecialty { CharacterId = 19, SpecialtyId = 8 }, // Deidara - Earth Style: Mud Wall
            new CharacterSpecialty { CharacterId = 20, SpecialtyId = 5 }, // Konan - Sharingan
            new CharacterSpecialty { CharacterId = 20, SpecialtyId = 9 }, // Konan - Wind Style: Great Breakthrough
            // Add more character specialties as needed
        };

                _logger.LogInformation("Adding character specialties...");
                _dataContext.CharacterSpecialties.AddRange(characterSpecialties);
                _dataContext.SaveChanges();

                _logger.LogInformation("Data seeded successfully.");
            }
            else
            {
                _logger.LogInformation("Data already exists. No seeding required.");
            }
        }

    }
}
