using SW_API.Domain.Entities;
using SW_API.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SW_API.Server.Services
{
    /// <summary>
    /// Interface for Character Service
    /// </summary>
    public interface ICharacterService
    {
        /// <summary>
        /// Get Character by ID
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns>Character DTO object</returns>
        public Task<Result<CharacterDTO>> GetById(Guid id);

        /// <summary>
        /// Register new character in the system
        /// </summary>
        /// <param name="request">Character Data</param>
        /// <returns>GUID of added entity</returns>
        public Task<Result<Guid>> AddCharacter(AddCharacterRequest request);

        /// <summary>
        /// Get list of all Characters
        /// </summary>
        /// <returns>Character DTO list</returns>
        Task<Result<List<CharacterDTO>>> GetList();

        /// <summary>
        /// Get list of all Characters with pagination
        /// </summary>
        /// <param name="pageSize">How many to fetch</param>
        /// <param name="pageNumber">Which page of given size are you on</param>
        /// <returns>Paginated character DTO list</returns>
        Task<Result<PaginatedList<CharacterDTO>>> GetList(int pageSize, int pageNumber);

        /// <summary>
        /// Delete Character by ID
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns>Status</returns>
        Task<Result<bool>> DeleteCharacter(Guid id);

    }
}
