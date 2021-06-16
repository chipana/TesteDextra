using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TesteDextra.Models;

namespace TesteDextra.Services.Interfaces
{
    /// <summary>
    /// Potter's characters service
    /// </summary>
    public interface ICharactersService
    {
        /// <summary>
        /// Create a new character
        /// </summary>
        /// <param name="character"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult> CreateAsync(Character character, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a character
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult> DeleteAsync(Guid Id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a character
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Character> GetAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of characters
        /// </summary>
        /// <param name="house"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Character>> ListAsync(Guid? house = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a character
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="character"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult> UpdateAsync(Guid Id, Character character, CancellationToken cancellationToken = default);
    }
}