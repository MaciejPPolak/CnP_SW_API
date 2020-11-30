using SW_API.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SW_API.Domain.Interfaces
{
    public interface IRelationRepository
    {
        Task AddRelationalEntity<T>(T relationalEntity) where T : RelationEntity;

        Task RemoveRelationalEntity<T>(T relationalEntity) where T : RelationEntity;
    }
}
