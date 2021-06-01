using System;
using System.Collections.Generic;
using TesteDextra.Models;

namespace TesteDextra.Services.Interfaces
{
    public interface ICharactersService
    {
        bool Create(Character character, out string message);
        bool Delete(Guid Id);
        Character Get(Guid id);
        IEnumerable<Character> List(Guid? house = null);
        bool Update(Guid Id, Character character);
    }
}