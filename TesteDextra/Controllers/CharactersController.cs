using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TesteDextra.Services.Interfaces;
using TesteDextra.Models;

namespace TesteDextra.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : Controller
    {
        private readonly ICharactersService _charactersService;

        public CharactersController(ICharactersService charactersService)
        {
            _charactersService = charactersService;
        }

        /// <summary>
        /// Creates a new character.
        /// </summary>
        /// <param name="Character">The character to be created</param>
        /// <returns>ResultCode</returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] Character Character)
        {
            if (_charactersService.Create(Character, out string message))
            {
                return Ok();
            }
            else
            {
                return BadRequest(message);
            }
        }


        /// <summary>
        /// Updates a character.
        /// </summary>
        /// <param name="Id">The character's id to be updated</param>
        /// <param name="Character">The character to be updated</param>
        /// <returns>ResultCode</returns>
        [HttpPut("Update/{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(Guid Id, [FromBody] Character Character)
        {
            if (_charactersService.Update(Id, Character))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets a single character
        /// </summary>
        /// <param name="Id">Id of the requested character</param>
        /// <returns>The requested character or null</returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Character> Get(Guid Id)
        {
            var character = _charactersService.Get(Id);
            if (character != null)
            {
                return Ok(character);
            }
            else
            {
                return BadRequest();
            }
        }
        
        /// <summary>
        /// Gets a list of characters
        /// </summary>
        /// <param name="House"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult List(Guid? House = null)
        {
            var charactersList = _charactersService.List(House);
            if (charactersList != null)
            {
                return Ok(charactersList);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes a character
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{Id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(Guid Id)
        {
            if (_charactersService.Delete(Id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
