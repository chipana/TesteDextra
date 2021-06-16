using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteDextra.Models;
using TesteDextra.Repositories.Interfaces;
using TesteDextra.Storage;
using System.Threading;

namespace TesteDextra.Repositories
{
    /// <summary>
    /// Potter's characters repository
    /// </summary>
    public class CharactersRepository : MongoProvider<Character>, ICharactersRepository
    {
        /// <summary>
        /// Potter's characters repository constructor
        /// </summary>
        /// <param name="configuration"></param>
        public CharactersRepository(IConfiguration configuration)
            : base(configuration.GetConnectionString("PotterDb"), "PotterDb", nameof(Character))
        { }

        /// <summary>
        /// Creates a new character
        /// </summary>
        /// <param name="character"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(Character character, CancellationToken cancellationToken = default)
        {
            await base.CreateAsync(character, cancellationToken: cancellationToken);
            return true;
        }

        /// <summary>
        /// Gets a character by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Character> GetAsync(Guid id, CancellationToken cancellationToken = default) => await GetAsync(p => p.Id == id, cancellationToken);

        /// <summary>
        /// Updates a character by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="character"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid id, Character character, CancellationToken cancellationToken = default) => (await UpdateAsync(p => p.Id == id, character, cancellationToken: cancellationToken)).IsAcknowledged;

        /// <summary>
        /// Deletes a character by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default) => (await DeleteAsync(p => p.Id == id, cancellationToken)).IsAcknowledged;


        /// <summary>
        /// Gets a list of characters
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Character>> ListAsync(CancellationToken cancellationToken = default) => await ListAsync(p => true, cancellationToken: cancellationToken);
    }
}
