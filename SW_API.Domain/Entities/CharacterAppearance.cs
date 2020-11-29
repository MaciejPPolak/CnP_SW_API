using System;

namespace SW_API.Domain.Entities
{
    /// <summary>
    /// Representation of character to media relation
    /// </summary>
    public class CharacterAppearance
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
        /// GUID of media
        /// </summary>
        public Guid MediaId { get; set; }
        /// <summary>
        /// Media entity
        /// </summary>
        public Media Media { get; set; }

    }
}
