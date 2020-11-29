using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Domain.Entities
{
    /// <summary>
    /// Base abstract entity
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Guid
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Parametrized CTOR
        /// </summary>
        /// <param name="id">GUID</param>
        protected BaseEntity(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// CTOR
        /// </summary>
        protected BaseEntity()
        {

        }

    }
}
