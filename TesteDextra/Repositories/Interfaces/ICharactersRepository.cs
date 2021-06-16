using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TesteDextra.Models;

namespace TesteDextra.Repositories.Interfaces
{
    /// <summary>
    /// Potter's characters repository
    /// </summary>
    public interface ICharactersRepository
    {
        /// <summary>
        /// Creates a new character
        /// </summary>
        /// <param name="character"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(Character character, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a character by id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid Id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a character by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Character> GetAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of characters
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Character>> ListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a character by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="character"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid Id, Character character, CancellationToken cancellationToken = default);
    }
}