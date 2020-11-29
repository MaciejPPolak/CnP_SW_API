using SW_API.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Domain.Entities
{
    public class Media
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public Media()
        {

        }

        /// <summary>
        /// Title of media property
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Type of media
        /// </summary>
        public MediaEntityType Type { get; set; }
        /// <summary>
        /// Release date
        /// </summary>
        public DateTime ReleaseDate { get; set; }
    }
}
