using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Domain.Entities
{    
    /// <summary>
     /// DTO - Character information
     /// </summary>
    public class CharacterDTO : BasicCharacterDTO
    {
        /// <summary>
        /// Related characters
        /// </summary>
        public virtual ICollection<BasicCharacterDTO> Friends { get; set; }

        /// <summary>
        /// Media apperances
        /// </summary>
        public virtual ICollection<BasicMediaDTO> Appearances { get; set; }
    }
}
