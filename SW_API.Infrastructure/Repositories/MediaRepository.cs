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
    public class MediaRepository : CRUDRepository, IMediaRepository
    {
        private readonly SWDbContext _dbContext;
        public MediaRepository(SWDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<MediaDTO> MediaByIdAsync(Guid id)
        {
            return await _dbContext.Media
                .Where(m => m.Id == id)
                .Select(media => new MediaDTO
                {
                    Id = media.Id,
                    Title = media.Title,
                    Type = media.Type,
                    ReleaseDate = media.ReleaseDate,
                    CharacterAppearances = media.CharacterAppearances.Select(appearance => new BasicCharacterDTO { Name = appearance.Character.Name, Id = appearance.Character.Id}).ToList()
                })
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<List<MediaDTO>> MediaPlainListAsync()
        {

            return await _dbContext.Media
                .Select(media => new MediaDTO
                {
                    Id = media.Id,
                    Title = media.Title,
                    Type = media.Type,
                    ReleaseDate = media.ReleaseDate,
                    CharacterAppearances = media.CharacterAppearances.Select(appearance => new BasicCharacterDTO { Name = appearance.Character.Name, Id = appearance.Character.Id }).ToList()
                })
                .ToListAsync();
        }

        public async Task<PaginatedList<MediaDTO>> MediaPaginatedListAsync(int pageSize, int pageNumber)
        {
            var offset = (pageNumber - 1) * pageSize;
            var value = await _dbContext.Media
                .Select(media => new MediaDTO
                {
                    Id = media.Id,
                    Title = media.Title,
                    Type = media.Type,
                    ReleaseDate = media.ReleaseDate,
                    CharacterAppearances = media.CharacterAppearances.Select(appearance => new BasicCharacterDTO { Name = appearance.Character.Name, Id = appearance.Character.Id }).ToList()
                })
                .Skip(offset).Take(pageSize)
                .ToListAsync();

            return new PaginatedList<MediaDTO>(_dbContext.Media.Count(), value);
        }
    }
}
