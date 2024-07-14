using NarutoDatabookApp.Data;
using NarutoDatabookApp.Interfaces;
using NarutoDatabookApp.Models;
using System.Linq;

namespace NarutoDatabookApp.Repository
{
    public class CharacterRepository : ICharacterInterface
    {
        private readonly DataContext _context;

        public CharacterRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateCharacter(int teamId, Character character)
        {
            var team = _context.Teams.Where(t => t.TeamId == teamId).FirstOrDefault();

            character.Team = team;

            _context.Add(character);
            return Save();
        }

        public bool DeleteCharacter(Character character)
        {
            _context.Remove(character);
            return Save();
        }

        public Character GetCharacter(int characterId)
        {
            return _context.Characters.Where(c => c.CharacterId == characterId).FirstOrDefault();
        }

        public Character GetCharacter(string name)
        {
            return _context.Characters.Where(c => c.Name == name).FirstOrDefault();
        }

        public Character GetCharacter(DateTime birthDate)
        {
            return _context.Characters.Where(c => c.BirthDate == birthDate).FirstOrDefault();
        }

        public ICollection<Character> GetCharacters()
        {
            return _context.Characters.OrderBy(c => c.CharacterId).ToList();
        }

        public bool CharacterExists(int characterId)
        {
            return _context.Characters.Any(c => c.CharacterId == characterId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateCharacter(int teamId, Character character)
        {
            character.Team = _context.Teams.Where(t => t.TeamId == teamId).FirstOrDefault();
            _context.Update(character);
            return Save();
        }
    }
}
