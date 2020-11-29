﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SW_API.Infrastructure.Context;

namespace SW_API.Infrastructure.Migrations
{
    [DbContext(typeof(SWDbContext))]
    [Migration("20201129100955_InitialGeneration")]
    partial class InitialGeneration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("SW_API.Domain.Entities.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("SW_API.Domain.Entities.CharacterAppearance", b =>
                {
                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MediaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CharacterId", "MediaId");

                    b.HasIndex("MediaId");

                    b.ToTable("CharacterAppearances");
                });

            modelBuilder.Entity("SW_API.Domain.Entities.Media", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("SW_API.Domain.Entities.Relationship", b =>
                {
                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FriendId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CharacterId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("Relationships");
                });

            modelBuilder.Entity("SW_API.Domain.Entities.CharacterAppearance", b =>
                {
                    b.HasOne("SW_API.Domain.Entities.Character", "Character")
                        .WithMany("MediaAppearances")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SW_API.Domain.Entities.Media", "Media")
                        .WithMany("CharacterAppearances")
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Media");
                });

            modelBuilder.Entity("SW_API.Domain.Entities.Relationship", b =>
                {
                    b.HasOne("SW_API.Domain.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SW_API.Domain.Entities.Character", "Friend")
                        .WithMany("Friends")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Friend");
                });

            modelBuilder.Entity("SW_API.Domain.Entities.Character", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("MediaAppearances");
                });

            modelBuilder.Entity("SW_API.Domain.Entities.Media", b =>
                {
                    b.Navigation("CharacterAppearances");
                });
#pragma warning restore 612, 618
        }
    }
}
