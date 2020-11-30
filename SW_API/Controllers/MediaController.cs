using Microsoft.AspNetCore.Mvc;
using SW_API.Domain.Entities;
using SW_API.Models;
using SW_API.Server.Models;
using SW_API.Server.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SW_API.Controllers
{
    /// <summary>
    /// Controlled for accessing media CRUD
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="mediaService">Media Service DI</param>
        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        /// <summary>
        /// Returns media by ID
        /// </summary>
        /// <param name="id">Media GUID</param>
        /// <returns>Media object</returns>
        [HttpGet]
        [Route("{id:Guid}", Name = nameof(GetMedia))]
        [ProducesResponseType(typeof(Media), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetMedia([FromRoute] Guid id)
        {
            var media = await _mediaService.GetById(id);

            if (media.Succeeded)
                return Ok(media.Value);

            return NotFound(media.Error.ReasonMessage);
        }

        /// <summary>
        /// Returns all media.
        /// </summary>
        /// <returns>Collection of media.</returns>
        [HttpGet]
        [Route("list/full", Name = nameof(GetMediaList))]
        [ProducesResponseType(typeof(IEnumerable<Media>), 200)]
        public async Task<IActionResult> GetMediaList()
        {
            var mediaCollection = await _mediaService.GetList();

            return Ok(mediaCollection.Value);
        }

        /// <summary>
        /// Returns all media - paginated.
        /// </summary>
        /// <returns>Paginated collection of media.</returns>
        [HttpGet]
        [Route("list", Name = nameof(GetMediaPaginated))]
        [ProducesResponseType(typeof(IEnumerable<Media>), 200)]
        public async Task<IActionResult> GetMediaPaginated([FromQuery] PagedListRequest request)
        {
            var mediaCollection = await _mediaService.GetList(request.PageSize, request.PageNumber);

            if (!mediaCollection.Succeeded)
                return StatusCode((int)mediaCollection.Error.ErrorCode, mediaCollection.Error.ReasonMessage);

            Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
            Response.Headers.Add("X-Total-Count", mediaCollection.Value.TotalCount.ToString());

            return Ok(mediaCollection.Value.Value);
        }

        /// <summary>
        /// Adds a media to the db.
        /// </summary>
        /// <param name="newMediaBody">Object containing info about media to be added.</param>
        /// <returns>Operation status</returns>
        [HttpPut]
        [Route("", Name = nameof(AddNewMedia))]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        public async Task<IActionResult> AddNewMedia([FromBody] AddMediaRequest newMediaBody)
        {
            var addOperation = await _mediaService.AddMedia(newMediaBody);

            if (addOperation.Succeeded)
                return Ok(addOperation.Value);

            return Conflict(addOperation.Error.ReasonMessage);
        }

        /// <summary>
        /// Removes a media from the db
        /// </summary>
        /// <param name="id">ID of given media object</param>
        /// <returns>Operation status</returns>
        [HttpDelete]
        [Route("{id:Guid}", Name = nameof(RemoveMedia))]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> RemoveMedia([FromRoute] Guid id)
        {
            var removalOperation = await _mediaService.DeleteMedia(id);

            if (removalOperation.Succeeded)
                return Ok(removalOperation.Value);

            return StatusCode((int)removalOperation.Error.ErrorCode, removalOperation.Error.ReasonMessage);
        }

        //TODO : POST UpdateMedia

    }
}
