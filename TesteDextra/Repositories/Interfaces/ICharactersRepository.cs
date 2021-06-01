using System;
using System.Collections.Generic;
using TesteDextra.Models;

namespace TesteDextra.Repositories.Interfaces
{
    public interface ICharactersRepository
    {
        bool Create(Character character);
        bool Delete(Guid Id);
        Character Get(Guid id);
        IEnumerable<Character> List(Guid? house = null);
        bool Update(Guid Id, Character character);
    }
}