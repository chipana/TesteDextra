using System.Collections.Generic;
using System.Threading.Tasks;
using TesteDextra.Models;

namespace TesteDextra.Repositories.Interfaces
{
    public interface IHousesRepository
    {
        IEnumerable<House> GetHouses();
    }
}