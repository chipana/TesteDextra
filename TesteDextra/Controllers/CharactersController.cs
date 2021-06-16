using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TesteDextra.Services.Interfaces;
using TesteDextra.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TesteDextra.Controllers
{
    /// <summary>
    /// Character Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : Controller
    {
        private readonly ICharactersService _charactersService;

        /// <summary>
        /// Character Controller Constructor
        /// </summary>
        /// <param name="charactersService"></param>
        public CharactersController(ICharactersService charactersService)
        {
            _charactersService = charactersService;
        }

        /// <summary>
        /// Creates a new character.
        /// </summary>
        /// <param name="Character">The character to be created</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ResultCode</returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<RequestResult> CreateAsync([FromBody] Character Character, CancellationToken cancellationToken = default)
        {
            return await _charactersService.CreateAsync(Character, cancellationToken);
        }


        /// <summary>
        /// Updates a character.
        /// </summary>
        /// <param name="Id">The character's id to be updated</param>
        /// <param name="Character">The character to be updated</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ResultCode</returns>
        [HttpPut("Update/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<RequestResult> UpdateAsync(Guid Id, [FromBody] Character Character, CancellationToken cancellationToken = default)
        {
            return await _charactersService.UpdateAsync(Id, Character);
        }

        /// <summary>
        /// Gets a single character
        /// </summary>
        /// <param name="Id">Id of the requested character</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The requested character or null</returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Character> GetAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await _charactersService.GetAsync(Id, cancellationToken);
        }

        /// <summary>
        /// Gets a list of characters
        /// </summary>
        /// <param name="House"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<Character>> List(Guid? House = null, CancellationToken cancellationToken = default)
        {
            return await _charactersService.ListAsync(House, cancellationToken);
        }

        /// <summary>
        /// Deletes a character
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{Id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<RequestResult> DeleteAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await _charactersService.DeleteAsync(Id, cancellationToken);
        }
    }
}
