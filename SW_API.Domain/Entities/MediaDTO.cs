using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Domain.Entities
{
    /// <summary>
    /// Complete Media DTO
    /// </summary>
    public class MediaDTO: BasicMediaDTO
    {
        /// <summary>
        /// Release date
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// Character apperances
        /// </summary>
        public virtual ICollection<BasicCharacterDTO> CharacterAppearances { get; set; }

    }
}
