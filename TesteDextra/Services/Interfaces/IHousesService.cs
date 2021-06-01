using System.Collections.Generic;
using System.Threading.Tasks;
using TesteDextra.Models;

namespace TesteDextra.Services.Interfaces
{
    public interface IHousesService
    {
        IEnumerable<House> GetHouses();
    }
}