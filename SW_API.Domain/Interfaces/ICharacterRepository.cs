using SW_API.Domain.Entities;
using SW_API.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SW_API.Domain.Interfaces
{
    /// <summary>
    /// Interface to access data for Character entity
    /// </summary>
    public interface ICharacterRepository: IBaseRepository
    {
        /// <summary>
        /// Retrieves media by ID
        /// </summary>
        /// <param name="id">GUID of object</param>
        /// <returns>Character DTO</returns>
        Task<CharacterDTO> CharacterByIdAsync(Guid id);
        /// <summary>
        /// Retrieves list of all Characters in the system
        /// </summary>
        /// <returns>Collection of Character DTOs</returns>
        Task<List<CharacterDTO>> CharacterPlainListAsync();
        /// <summary>
        /// Returns slice of all Character entities in the system based on parameters
        /// </summary>
        /// <param name="pageSize">How many to fetch</param>
        /// <param name="pageNumber">Which page of given size are you on</param>
        /// <returns>Collection of Character DTOs</returns>
        Task<PaginatedList<CharacterDTO>> CharacterPaginatedListAsync(int pageSize, int pageNumber);
    }
}
