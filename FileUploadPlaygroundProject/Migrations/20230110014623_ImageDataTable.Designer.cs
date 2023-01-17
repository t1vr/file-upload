﻿// <auto-generated />
using System;
using FileUploadPlaygroundProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FileUploadPlaygroundProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230110014623_ImageDataTable")]
    partial class ImageDataTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FileUploadPlaygroundProject.Services.ImageData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("FullscreenContent")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("OriginalContent")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("OriginalFileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginalType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("ThumbnailContent")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("ImageData");
                });
#pragma warning restore 612, 618
        }
    }
}
