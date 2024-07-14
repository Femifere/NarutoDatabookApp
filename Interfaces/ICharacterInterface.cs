using NarutoDatabookApp.Models;
using System;
using System.Collections.Generic;

namespace NarutoDatabookApp.Interfaces
{
    public interface ICharacterInterface
    {
        ICollection<Character> GetCharacters();
        Character GetCharacter(int characterId);
        Character GetCharacter(string name);
        Character GetCharacter(DateTime birthDate);
        bool CharacterExists(int characterId);
        bool CreateCharacter(int teamId, Character character);
        bool UpdateCharacter(int teamId, Character character);
        bool DeleteCharacter(Character character);
        bool Save();
    }
}
