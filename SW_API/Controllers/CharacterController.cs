using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SW_API.Domain.Entities;
using SW_API.Models;
using SW_API.Server.Models;
using SW_API.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW_API.Controllers
{
    /// <summary>
    /// Controller for accessing character CRUD
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="characterService">Character Service DI</param>
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        /// <summary>
        /// Returns character by ID
        /// </summary>
        /// <param name="id">Character GUID</param>
        /// <returns>Character object</returns>
        [HttpGet]
        [Route("{id:Guid}", Name = nameof(GetCharacter))]
        [ProducesResponseType(typeof(Character), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetCharacter([FromRoute] Guid id)
        {
            var character = await _characterService.GetById(id);

            if (character.Succeeded)
                return Ok(character.Value);

            return NotFound(character.Error.ReasonMessage);
        }

        /// <summary>
        /// Returns all characters.
        /// </summary>
        /// <returns>Collection of characters.</returns>
        [HttpGet]
        [Route("list/full", Name = nameof(GetCharacters))]
        [ProducesResponseType(typeof(IEnumerable<Character>), 200)]
        public async Task<IActionResult> GetCharacters()
        {
            var characterCollection = await _characterService.GetList();

            return Ok(characterCollection.Value);
        }

        /// <summary>
        /// Returns all characters - paginated.
        /// </summary>
        /// <returns>Paginated collection of characters.</returns>
        [HttpGet]
        [Route("list", Name = nameof(GetCharactersPaginated))]
        [ProducesResponseType(typeof(IEnumerable<Character>), 200)]
        public async Task<IActionResult> GetCharactersPaginated([FromQuery]PagedListRequest request)
        {
            var characterCollection = await _characterService.GetList(request.PageNumber, request.PageSize);

            if(!characterCollection.Succeeded)
                return StatusCode((int)characterCollection.Error.ErrorCode, characterCollection.Error.ReasonMessage);

            Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
            Response.Headers.Add("X-Total-Count", characterCollection.Value.TotalCount.ToString());

            return Ok(characterCollection.Value);
        }

        /// <summary>
        /// Adds a character to the db.
        /// </summary>
        /// <param name="newCharacterBody">Object containing info about character to be added.</param>
        /// <returns>Operation status</returns>
        [HttpPost]
        [Route("", Name = nameof(AddNewCharacter))]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        public async Task<IActionResult> AddNewCharacter([FromBody] AddCharacterRequest newCharacterBody)
        {
            var addOperation = await _characterService.AddCharacter(newCharacterBody);

            if (addOperation.Succeeded)
                return Ok(addOperation.Value);

            return Conflict(addOperation.Error.ReasonMessage);
        }

        /// <summary>
        /// Removes a character from the db
        /// </summary>
        /// <param name="id">ID of given character object</param>
        /// <returns>Operation status</returns>
        [HttpDelete]
        [Route("{id:Guid}", Name = nameof(RemoveCharacter))]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> RemoveCharacter([FromRoute] Guid id)
        {
            var removalOperation = await _characterService.DeleteCharacter(id);

            if (removalOperation.Succeeded)
                return Ok(removalOperation.Value);

            return StatusCode((int)removalOperation.Error.ErrorCode, removalOperation.Error.ReasonMessage);
        }

    }
}
