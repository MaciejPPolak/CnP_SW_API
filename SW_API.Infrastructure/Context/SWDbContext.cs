using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SW_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW_API.Infrastructure.Context
{
    /// <summary>
    /// Star Wars DB Context
    /// </summary>
    public class SWDbContext : DbContext
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public SWDbContext(DbContextOptions<SWDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// DB Set of character entity
        /// </summary>
        public DbSet<Character> Characters { get; set; }
        /// <summary>
        /// DB Set of media entity
        /// </summary>
        public DbSet<Media> Media { get; set; }
        /// <summary>
        /// DB Set of Character to Media relations
        /// </summary>
        public DbSet<CharacterAppearance> CharacterAppearances { get; set; }
        /// <summary>
        /// DB Set of character to character relations
        /// </summary>
        public DbSet<Relationship> Relationships { get; set; }

        /// <summary>
        /// On configuration action
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// On model creating action
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterAppearance>()
                .HasKey(appearance => new { appearance.CharacterId, appearance.MediaId });

            modelBuilder.Entity<CharacterAppearance>()
                .HasOne(appearance => appearance.Character).WithMany(ch => ch.MediaAppearances)
                .HasForeignKey(appearance => appearance.CharacterId);

            modelBuilder.Entity<CharacterAppearance>()
                .HasOne(appearance => appearance.Media).WithMany(md => md.CharacterAppearances)
                .HasForeignKey(appearance => appearance.MediaId);

            modelBuilder.Entity<Relationship>()
                .HasKey(rel => new { rel.CharacterId, rel.FriendId });

            modelBuilder.Entity<Relationship>()
                .HasOne(rel => rel.Character).WithMany(ch => ch.Friends)
                .HasForeignKey(rel => rel.CharacterId);

            modelBuilder.Entity<Relationship>()
                .HasOne(rel => rel.Friend).WithMany()
                .HasForeignKey(rel => rel.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
