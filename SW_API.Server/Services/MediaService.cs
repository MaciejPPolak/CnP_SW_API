using SW_API.Domain.Entities;
using SW_API.Domain.Interfaces;
using SW_API.Server.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SW_API.Server.Services
{
    /// <summary>
    /// Service to operate on Media data
    /// </summary>
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository  _repository;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="repository">Media repository DI</param>
        public MediaService(IMediaRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get media by ID
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns>Media DTO object</returns>
        public async Task<Result<Media>> GetById(Guid id)
        {
            var media = await _repository.MediaByIdAsync(id);

            if (media is null)
            {
                return Result<Media>.Failure(new Error(HttpStatusCode.NotFound, $"Object of given ID {id} doesn't exist"));
            }

            return Result<Media>.Success(media);
        }

        /// <summary>
        /// Get list of all media
        /// </summary>
        /// <returns>Media DTO list</returns>
        public async Task<Result<List<Media>>> GetList()
        {
            var mediaList = await _repository.MediaPlainListAsync();

            if (mediaList is null)
                return Result<List<Media>>.Failure(new Error(HttpStatusCode.NotFound, $"No media found."));

            return Result<List<Media>>.Success(mediaList);
        }

        /// <summary>
        /// Get list of all media with pagination
        /// </summary>
        /// <param name="pageSize">How many to fetch</param>
        /// <param name="pageNumber">Which page of given size are you on</param>
        /// <returns>Paginated media DTO list</returns>
        public async Task<Result<PaginatedList<Media>>> GetList(int pageSize, int pageNumber)
        {
            var mediaList = await _repository.MediaPaginatedListAsync(pageSize, pageNumber);

            if (mediaList is null)
                return Result<PaginatedList<Media>>.Failure(new Error(HttpStatusCode.NotFound, $"No media found."));

            return Result<PaginatedList<Media>>.Success(mediaList);
        }

        /// <summary>
        /// Register new media in the system
        /// </summary>
        /// <param name="request">Media Data</param>
        /// <returns>GUID of added entity</returns>
        public async Task<Result<Guid>> AddMedia(AddMediaRequest request)
        {
            var guid = Guid.NewGuid();

            var media = new Media()
            {
                Id = guid,
                Title = request.Title,
                Type = request.Type,
                ReleaseDate = request.ReleaseDate
            };

            var result = await _repository.AddAsync(media);

            return Result<Guid>.Success(result);
        }

        /// <summary>
        /// Delete media by ID
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns>Status</returns>
        public async Task<Result<bool>> DeleteMedia(Guid id)
        {
            var searchResult = await _repository.GetByIdAsync<Media>(id);

            if (searchResult is null)
                return Result<bool>.Failure(new Error(HttpStatusCode.NotFound, $"Object of given ID {id} doesn't exist"));

            await _repository.DeleteAsync(searchResult);

            return Result<bool>.Success(true);
        }
    }
}
