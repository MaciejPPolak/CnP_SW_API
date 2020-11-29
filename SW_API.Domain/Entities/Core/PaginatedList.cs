using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Domain.Entities
{
    /// <summary>
    /// Paginated list base object
    /// </summary>
    /// <typeparam name="T">Type of list</typeparam>
    public class PaginatedList<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="total">Collection count</param>
        /// <param name="value">Collection</param>
        public PaginatedList(int total, IEnumerable<T> value)
        {
            TotalCount = total;
            Value = value;
        }
        /// <summary>
        /// List value
        /// </summary>
        public IEnumerable<T> Value { get; }
        /// <summary>
        /// Total elements in db
        /// </summary>
        public int TotalCount { get; }
    }
}
