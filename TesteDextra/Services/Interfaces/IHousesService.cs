using System.Collections.Generic;
using System.Threading.Tasks;
using TesteDextra.Models;

namespace TesteDextra.Services.Interfaces
{
    /// <summary>
    /// Potter's houses service
    /// </summary>
    public interface IHousesService
    {
        /// <summary>
        /// Gets the houses from service
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<House>> GetHousesAsync();
    }
}