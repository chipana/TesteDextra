using System;
using System.Collections.Generic;
using TesteDextra.Services.Interfaces;
using TesteDextra.Repositories.Interfaces;
using TesteDextra.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace TesteDextra.Services
{
    /// <summary>
    /// Potter's characters service
    /// </summary>
    public class CharactersService : ICharactersService
    {
        private readonly ICharactersRepository _charactersRepository;
        private readonly IHousesService _housesService;

        /// <summary>
        /// Potter's characters service constructor
        /// </summary>
        /// <param name="charactersRepository"></param>
        /// <param name="housesService"></param>
        public CharactersService(ICharactersRepository charactersRepository,
            IHousesService housesService)
        {
            _charactersRepository = charactersRepository;
            _housesService = housesService;
        }

        /// <summary>
        /// Create a new character
        /// </summary>
        /// <param name="character"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RequestResult> CreateAsync(Character character, CancellationToken cancellationToken = default)
        {
            if (await GetAsync(character.Id, cancellationToken) != null)
            {
                return new RequestResult(HttpStatusCode.BadRequest, "Character already exists.");
            }

            if (!(await _housesService.GetHousesAsync()).Any(p => p.Id == character.House))
            {
                return new RequestResult(HttpStatusCode.BadRequest, "Unavailable house. Check your code and try again.");
            }

            if (await _charactersRepository.CreateAsync(character, cancellationToken))
            {
                return new RequestResult { Code = HttpStatusCode.OK };
            }

            return new RequestResult(HttpStatusCode.InternalServerError, "Unknown error");
        }

        /// <summary>
        /// Gets a character
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Character> GetAsync(Guid id, CancellationToken cancellationToken = default) => await _charactersRepository.GetAsync(id, cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="character"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RequestResult> UpdateAsync(Guid Id, Character character, CancellationToken cancellationToken = default)
        {
            if (await GetAsync(Id, cancellationToken) == null)
            {
                return new RequestResult(HttpStatusCode.BadRequest, "Character doesn't exists.");
            }

            if (!(await _housesService.GetHousesAsync()).Any(p => p.Id == character.House))
            {
                return new RequestResult(HttpStatusCode.BadRequest, "Unavailable house.");
            }

            if (await _charactersRepository.UpdateAsync(Id, character, cancellationToken))
            {
                return new RequestResult { Code = HttpStatusCode.OK };
            }

            return new RequestResult(HttpStatusCode.InternalServerError, "Unknown error");
        }

        /// <summary>
        /// Deletes a character
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RequestResult> DeleteAsync(Guid Id, CancellationToken cancellationToken = default) 
        {
            if (await GetAsync(Id, cancellationToken) == null)
            {
                return new RequestResult(HttpStatusCode.BadRequest, "Character doesn't exists.");
            }

            if (await _charactersRepository.DeleteAsync(Id, cancellationToken))
            {
                return new RequestResult { Code = HttpStatusCode.OK };
            }

            return new RequestResult(HttpStatusCode.InternalServerError, "Unknown error");
        }

        /// <summary>
        /// Gets a list of characters
        /// </summary>
        /// <param name="house"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Character>> ListAsync(Guid? house = null, CancellationToken cancellationToken = default)
        {
            var result = await _charactersRepository.ListAsync(cancellationToken);
            
            if (house.HasValue)
            {
                result = result.Where(p => p.House == house);
            }

            return result;
        }
    }
}
