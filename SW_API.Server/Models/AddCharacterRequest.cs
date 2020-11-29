using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Server.Models
{
    /// <summary>
    /// Data necessary to create new character
    /// </summary>
    public class AddCharacterRequest
    {
        /// <summary>
        /// Character Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Guids of media apperances
        /// </summary>
        public virtual ICollection<Guid> MediaAppearances { get; set; }

        /// <summary>
        /// Guids of related characters
        /// </summary>
        public virtual ICollection<Guid> Friends { get; set; }
    }
}
