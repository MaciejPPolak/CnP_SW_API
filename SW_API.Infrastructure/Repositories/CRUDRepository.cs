using Microsoft.EntityFrameworkCore;
using SW_API.Domain.Entities;
using SW_API.Domain.Entities.Core;
using SW_API.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SW_API.Infrastructure.Repositories
{
    public class CRUDRepository : IBaseRepository
    {
        private readonly DbContext _baseContext;

        protected CRUDRepository(DbContext context)
        {
            _baseContext = context;
        }

        public async Task<Guid> AddAsync<T>(T entity) where T : BaseEntity
        {
            await _baseContext.Set<T>().AddAsync(entity);
            await _baseContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            _baseContext.Set<T>().Remove(entity);
            await _baseContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity
        {
            return await _baseContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<PaginatedList<T>> PaginatedListAsync<T>(int pageSize, int pageNumber) where T : BaseEntity
        {
            var offset = (pageNumber - 1) * pageSize;
            var value = await _baseContext.Set<T>().Skip(offset).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(_baseContext.Set<T>().Count(), value);
        }

        public async Task<List<T>> PlainListAsync<T>() where T : BaseEntity
        {
            return await _baseContext.Set<T>().ToListAsync();
        }

        public async Task<Result<T>> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            try
            {
                _baseContext.Entry(entity).State = EntityState.Modified;
                await _baseContext.SaveChangesAsync();
                return Result<T>.Success();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result<T>.Failure(new Error(HttpStatusCode.NotFound, "Entity not found"));
            }
        }
    }
}
