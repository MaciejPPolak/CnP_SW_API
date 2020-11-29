using SW_API.Domain.Entities;
using SW_API.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SW_API.Domain.Interfaces
{
    /// <summary>
    /// Interface to access data for Media entity
    /// </summary>
    public interface IMediaRepository : IBaseRepository
    {        
        /// <summary>
        /// Retrieves media by ID
        /// </summary>
        /// <param name="id">GUID of object</param>
        /// <returns>Media Entity</returns>
        Task<Media> MediaByIdAsync(Guid id);

        /// <summary>
        /// Retrieves list of all Media entities in the system
        /// </summary>
        /// <returns>Collection of Media Entity</returns>
        Task<List<Media>> MediaPlainListAsync();
        /// <summary>
        /// Returns slice of all media entities in the system based on parameters
        /// </summary>
        /// <param name="pageSize">How many to fetch</param>
        /// <param name="pageNumber">Which page of given size are you on</param>
        /// <returns>Collection of Media Entity</returns>
        Task<PaginatedList<Media>> MediaPaginatedListAsync(int pageSize, int pageNumber);
    }
}
