using System;
using System.Collections.Generic;
using TesteDextra.Services.Interfaces;
using TesteDextra.Repositories.Interfaces;
using TesteDextra.Models;
using System.Linq;

namespace TesteDextra.Services
{
    public class CharactersService : ICharactersService
    {
        private readonly ICharactersRepository _charactersRepository;
        private readonly IHousesService _housesService;

        public CharactersService(ICharactersRepository charactersRepository,
            IHousesService housesService)
        {
            _charactersRepository = charactersRepository;
            _housesService = housesService;
        }

        public bool Create(Character character, out string message)
        {
            if (Get(character.Id) == null)
            {
                message = "Duplicated Id";
                return false;
            }
            
            if(!_housesService.GetHouses().Any(p => p.Id == character.House))
            {
                message = "Unavailable house. Check your code and try again.";
                return false;
            }


            message = "";
            return _charactersRepository.Create(character);
        }

        public Character Get(Guid id) => _charactersRepository.Get(id);

        public bool Update(Guid Id, Character character) => _charactersRepository.Update(Id, character);

        public bool Delete(Guid Id) => _charactersRepository.Delete(Id);

        public IEnumerable<Character> List(Guid? house = null) => _charactersRepository.List(house);
    }
}
