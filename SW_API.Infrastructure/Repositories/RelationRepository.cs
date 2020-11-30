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
    public class RelationRepository: IRelationRepository
    {
        private readonly DbContext _baseContext;

        protected RelationRepository(DbContext context)
        {
            _baseContext = context;
        }

        public async Task AddRelationalEntity<T>(T relationalEntity) where T: RelationEntity
        {
            await _baseContext.Set<T>().AddAsync(relationalEntity);
            await _baseContext.SaveChangesAsync();
        }
        public async Task RemoveRelationalEntity<T>(T relationalEntity) where T : RelationEntity
        {
            _baseContext.Set<T>().Remove(relationalEntity);
            await _baseContext.SaveChangesAsync();
        }


    }
}
