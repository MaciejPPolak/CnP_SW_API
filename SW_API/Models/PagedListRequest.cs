
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace SW_API.Models
{
    /// <summary>
    /// Query containing pagination data
    /// </summary>
    public class PagedListRequest
    {
        /// <summary>
        /// Size of page
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }

        /// <summary>
        /// Page number starts from 1
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }
    }
}
