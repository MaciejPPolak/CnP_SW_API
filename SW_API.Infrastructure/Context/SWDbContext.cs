using Microsoft.EntityFrameworkCore;
using SW_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Infrastructure.Context
{
    public class SWDbContext: DbContext
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public SWDbContext(DbContextOptions<SWDbContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Media> Media { get; set; }

    }
}
