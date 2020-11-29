using SW_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SW_API.Domain.Interfaces.Core
{
    /// <summary>
    /// Default EF Core data access Interface
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// Get object by ID
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="id">GUID</param>
        Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity;
        /// <summary>
        /// Get plain object list
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        Task<List<T>> PlainListAsync<T>() where T : BaseEntity;
        /// <summary>
        /// Get paginated object list
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        Task<PaginatedList<T>> PaginatedListAsync<T>(int pageSize, int pageNumber) where T : BaseEntity;
        /// <summary>
        /// Add object
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="entity"></param>
        Task<Guid> AddAsync<T>(T entity) where T : BaseEntity;
        /// <summary>
        /// Update Object
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="entity"></param>
        Task<Result<T>> UpdateAsync<T>(T entity) where T : BaseEntity;
        /// <summary>
        /// Delete Object
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="entity"></param>
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
    }
}
