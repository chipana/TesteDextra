using System.Collections.Generic;
using System.Threading.Tasks;
using TesteDextra.Models;

namespace TesteDextra.Repositories.Interfaces
{
    /// <summary>
    /// Potter's Houses repository
    /// </summary>
    public interface IHousesRepository
    {
        /// <summary>
        /// Gets houses from repository
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<House>> GetHousesAsync();
    }
}