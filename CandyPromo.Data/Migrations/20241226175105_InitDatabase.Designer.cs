﻿// <auto-generated />
using System;
using CandyPromo.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CandyPromo.Data.Migrations
{
    [DbContext(typeof(CandyPromoContext))]
    [Migration("20241226175105_InitDatabase")]
    partial class InitDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CandyPromo.Data.Models.Prize", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PromocodeId")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PromocodeId")
                        .IsUnique();

                    b.ToTable("Prizes");
                });

            modelBuilder.Entity("CandyPromo.Data.Models.Promocode", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PrizeId")
                        .HasColumnType("uuid");

                    b.HasKey("Code");

                    b.HasIndex("OwnerId");

                    b.ToTable("Promocodes");
                });

            modelBuilder.Entity("CandyPromo.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CandyPromo.Data.Models.Prize", b =>
                {
                    b.HasOne("CandyPromo.Data.Models.Promocode", "Promocode")
                        .WithOne("Prize")
                        .HasForeignKey("CandyPromo.Data.Models.Prize", "PromocodeId");

                    b.Navigation("Promocode");
                });

            modelBuilder.Entity("CandyPromo.Data.Models.Promocode", b =>
                {
                    b.HasOne("CandyPromo.Data.Models.User", "Owner")
                        .WithMany("Promocodes")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CandyPromo.Data.Models.Promocode", b =>
                {
                    b.Navigation("Prize");
                });

            modelBuilder.Entity("CandyPromo.Data.Models.User", b =>
                {
                    b.Navigation("Promocodes");
                });
#pragma warning restore 612, 618
        }
    }
}
