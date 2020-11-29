using Microsoft.EntityFrameworkCore;
using SW_API.Domain.Entities;
using SW_API.Domain.Entities.Core;
using SW_API.Domain.Interfaces;
using SW_API.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_API.Infrastructure.Repositories
{
    public class CharacterRepository: CRUDRepository, ICharacterRepository
    {
        private readonly SWDbContext _dbContext;
        public CharacterRepository(SWDbContext context): base(context)
        {
            _dbContext = context;
        }

        public async Task<CharacterDTO> CharacterByIdAsync(Guid id)
        {
            return await _dbContext.Characters
                .Select(character => new CharacterDTO
                {
                    Id = character.Id,
                    Name = character.Name,
                    Friends = character.Friends.Select(frnd => new BasicCharacterDTO() { Name = frnd.Friend.Name, Id = frnd.Friend.Id }).ToList(),
                    Appearances = character.MediaAppearances.Select(media => new BasicMediaDTO() { Title = media.Media.Title, Id = media.Media.Id, Type = media.Media.Type }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<CharacterDTO>> CharacterPlainListAsync()
        {
            return await _dbContext.Characters
                .Select(character => new CharacterDTO
                {
                    Id = character.Id,
                    Name = character.Name,
                    Friends = character.Friends.Select(frnd => new BasicCharacterDTO() { Name = frnd.Friend.Name, Id = frnd.Friend.Id }).ToList(),
                    Appearances = character.MediaAppearances.Select(media => new BasicMediaDTO(){ Title = media.Media.Title, Id = media.Media.Id, Type = media.Media.Type}).ToList()
                })
                .ToListAsync();
        }

        public async Task<PaginatedList<CharacterDTO>> CharacterPaginatedListAsync(int pageSize, int pageNumber)
        {
            var offset = (pageNumber - 1) * pageSize;

            var value = await _dbContext.Characters
                .Select(character => new CharacterDTO
                {
                    Id = character.Id,
                    Name = character.Name,
                    Friends = character.Friends.Select(frnd => new BasicCharacterDTO() { Name = frnd.Friend.Name, Id = frnd.Friend.Id }).ToList(),
                    Appearances = character.MediaAppearances.Select(media => new BasicMediaDTO() { Title = media.Media.Title, Id = media.Media.Id, Type = media.Media.Type }).ToList()
                })
                .Skip(offset).Take(pageSize)
                .ToListAsync();


            return new PaginatedList<CharacterDTO>(_dbContext.Characters.Count(), value);
        }
    }
}
