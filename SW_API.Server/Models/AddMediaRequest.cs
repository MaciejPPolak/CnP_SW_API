﻿using SW_API.Domain.Enums;
using System;

namespace SW_API.Server.Models
{
    /// <summary>
    /// Data necessary to create new media entity
    /// </summary>
    public class AddMediaRequest
    {
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
