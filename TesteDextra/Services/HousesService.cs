using System.Collections.Generic;
using System.Threading.Tasks;
using TesteDextra.Models;
using TesteDextra.Repositories.Interfaces;
using TesteDextra.Services.Interfaces;

namespace TesteDextra.Services
{
    /// <summary>
    /// Potter's houses service
    /// </summary>
    public class HousesService : IHousesService
    {
        private readonly IHousesRepository _housesRepository;

        /// <summary>
        /// Potter's houses service constructor
        /// </summary>
        /// <param name="housesRepository"></param>
        public HousesService(IHousesRepository housesRepository)
        {
            _housesRepository = housesRepository;
        }

        /// <summary>
        /// Gets the houses from service
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<House>> GetHousesAsync() => await _housesRepository.GetHousesAsync();
    }
}
