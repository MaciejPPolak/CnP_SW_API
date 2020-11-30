using SW_API.Domain.Entities;
using SW_API.Domain.Interfaces;
using SW_API.Server.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SW_API.Server.Services
{
    /// <summary>
    /// Service to operate on Character data
    /// </summary>
    public class CharacterService: ICharacterService
    {
        private readonly ICharacterRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">Character Repository DI</param>
        public CharacterService(ICharacterRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get Character by ID
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns>Character DTO object</returns>
        public async Task<Result<CharacterDTO>> GetById(Guid id)
        {

            var character = await _repository.CharacterByIdAsync(id);

            if(character is null)
            {
                return Result<CharacterDTO>.Failure(new Error(HttpStatusCode.NotFound, $"Object of given ID {id} doesn't exist"));
            }

            return Result<CharacterDTO>.Success(character);
        }

        /// <summary>
        /// Get list of all Characters
        /// </summary>
        /// <returns>Character DTO list</returns>
        public async Task<Result<List<CharacterDTO>>> GetList()
        {
            var characterList = await _repository.CharacterPlainListAsync();

            if(characterList is null)
                return Result<List<CharacterDTO>>.Failure(new Error(HttpStatusCode.NotFound, $"No characters found."));

            return Result<List<CharacterDTO>>.Success(characterList);

        }

        /// <summary>
        /// Get list of all Characters with pagination
        /// </summary>
        /// <param name="pageSize">How many to fetch</param>
        /// <param name="pageNumber">Which page of given size are you on</param>
        /// <returns>Paginated character DTO list</returns>
        public async Task<Result<PaginatedList<CharacterDTO>>> GetList(int pageSize, int pageNumber)
        {
            var characterList = await _repository.CharacterPaginatedListAsync(pageSize, pageNumber);

            if (characterList is null)
                return Result<PaginatedList<CharacterDTO>>.Failure(new Error(HttpStatusCode.NotFound, $"No characters found."));

            return Result<PaginatedList<CharacterDTO>>.Success(characterList);

        }

        /// <summary>
        /// Delete Character by ID
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns>Status</returns>
        public async Task<Result<bool>> DeleteCharacter(Guid id)
        {
            var searchResult = await _repository.GetByIdAsync<Character>(id);

            if(searchResult is null)
                return Result<bool>.Failure(new Error(HttpStatusCode.NotFound, $"Object of given ID {id} doesn't exist"));

            await _repository.DeleteAsync(searchResult);

            return Result<bool>.Success(true);

        }

        /// <summary>
        /// Register new character in the system
        /// </summary>
        /// <param name="request">Character Data</param>
        /// <returns>GUID of added entity</returns>
        public async Task<Result<Guid>> AddCharacter(AddCharacterRequest request)
        {
            var guid = Guid.NewGuid();

            var character = new Character()
            {
                Id = guid,
                Name = request.Name,
                Friends = new List<Relationship>(),
                MediaAppearances = new List<CharacterAppearance>()
            };

            // Possible to re-work using AutoMapper. 

            if(request.Friends != null && request.Friends.Count != 0)
            {
                foreach(var friend in request.Friends)
                {
                    var relationshipObject = CreateRelationship(guid, friend);
                    character.Friends.Add(relationshipObject);
                };
            }
            if (request.MediaAppearances != null && request.MediaAppearances.Count != 0)
            {
                foreach (var media in request.MediaAppearances)
                {
                    var appearanceObject = CreateAppearance(guid, media);
                    character.MediaAppearances.Add(appearanceObject);
                };
            }

            var result = await _repository.AddAsync(character);

            return Result<Guid>.Success(result);
        }

        private Relationship CreateRelationship(Guid characterId, Guid friendId)
        {
            return new Relationship()
            {
                CharacterId = characterId,
                FriendId = friendId
            };
        }

        private CharacterAppearance CreateAppearance(Guid characterId, Guid mediaId)
        {
            return new CharacterAppearance()
            {
                CharacterId = characterId,
                MediaId = mediaId
            };
        }
    }
}
