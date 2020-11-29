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

        public async Task<Media> MediaByIdAsync(Guid id)
        {
            return await _dbContext.Media
                .Where(c => c.Id == id)
                .Include(med => med.CharacterAppearances).ThenInclude(ca => ca.Character)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Media>> MediaPlainListAsync()
        {

            return await _dbContext.Media
                .Include(med => med.CharacterAppearances).ThenInclude(ca => ca.Character)
                .ToListAsync();
        }

        public async Task<PaginatedList<Media>> MediaPaginatedListAsync(int pageSize, int pageNumber)
        {
            var offset = (pageNumber - 1) * pageSize;
            var value = await _dbContext.Media
                .Include(med => med.CharacterAppearances).ThenInclude(ca => ca.Character)
                .Skip(offset).Take(pageSize)
                .ToListAsync();

            return new PaginatedList<Media>(_dbContext.Media.Count(), value);
        }
    }
}
