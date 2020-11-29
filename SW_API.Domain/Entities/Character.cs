using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Domain.Entities
{
    /// <summary>
    /// Character Entity
    /// </summary>
    public class Character: BaseEntity
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public Character()
        {

        }

        /// <summary>
        /// Character Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Media apperances
        /// </summary>
        public virtual ICollection<Media> Media { get; set; }

        /// <summary>
        /// Related characters
        /// </summary>
        public virtual ICollection<Character> Friends { get; set; }

    }
}
