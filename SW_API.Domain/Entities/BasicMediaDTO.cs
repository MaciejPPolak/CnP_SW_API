using SW_API.Domain.Entities;
using SW_API.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Domain.Entities
{
    /// <summary>
    /// DTO - Basic Media Type
    /// </summary>
    public class BasicMediaDTO: BaseEntity
    {
        /// <summary>
        /// Title of media property
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Type of media
        /// </summary>
        public MediaEntityType Type { get; set; }
    }
}
