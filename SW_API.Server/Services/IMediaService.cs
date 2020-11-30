using SW_API.Domain.Entities;
using SW_API.Server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SW_API.Server.Services
{
    /// <summary>
    /// Interface for Media Service
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// Get media by ID
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns>Character DTO object</returns>
        public Task<Result<Media>> GetById(Guid id);

        /// <summary>
        /// Register new media in the system
        /// </summary>
        /// <param name="request">Media Data</param>
        /// <returns>GUID of added entity</returns>
        public Task<Result<Guid>> AddMedia(AddMediaRequest request);

        /// <summary>
        /// Get list of all media
        /// </summary>
        /// <returns>Media DTO list</returns>
        Task<Result<List<Media>>> GetList();

        /// <summary>
        /// Get list of all media with pagination
        /// </summary>
        /// <param name="pageSize">How many to fetch</param>
        /// <param name="pageNumber">Which page of given size are you on</param>
        /// <returns>Paginated media DTO list</returns>
        Task<Result<PaginatedList<Media>>> GetList(int pageSize, int pageNumber);

        /// <summary>
        /// Delete media by ID
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns>Status</returns>
        Task<Result<bool>> DeleteMedia(Guid id);
    }
}
