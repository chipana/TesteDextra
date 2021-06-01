using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteDextra.Models;
using TesteDextra.Repositories.Interfaces;
using TesteDextra.Services.Interfaces;

namespace TesteDextra.Services
{
    public class HousesService : IHousesService
    {
        private readonly IHousesRepository _housesRepository;

        public HousesService(IHousesRepository housesRepository)
        {
            _housesRepository = housesRepository;
        }

        public IEnumerable<House> GetHouses()
        {
            return _housesRepository.GetHouses();
        }
    }
}
