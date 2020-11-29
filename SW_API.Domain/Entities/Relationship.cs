using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Domain.Entities
{
    /// <summary>
    /// Representation of character to character relation
    /// </summary>
    public class Relationship
    {
        /// <summary>
        /// GUID of character
        /// </summary>
        public Guid CharacterId { get; set; }
        /// <summary>
        /// Character entity
        /// </summary>
        public Character Character { get; set; }
        /// <summary>
        /// GUID of connected character
        /// </summary>
        public Guid FriendId { get; set; }
        /// <summary>
        /// Connected character entity
        /// </summary>
        public Character Friend { get; set; }
    }
}
