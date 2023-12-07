﻿// <auto-generated />
using System;
using MeetupAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeetupAPI.Migrations
{
    [DbContext(typeof(MeetupContext))]
    [Migration("20231207015226_FixLectures")]
    partial class FixLectures
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MeetupAPI.Entities.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MeetupId")
                        .HasColumnType("int");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MeetupId");

                    b.ToTable("Lectures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Jane Smith",
                            Description = "A beginner-friendly session on programming basics.",
                            MeetupId = 1,
                            Topic = "Introduction to Programming"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Bob Johnson",
                            Description = "Exploring the latest trends in web development.",
                            MeetupId = 1,
                            Topic = "Web Development Trends"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Charlie Brown",
                            Description = "An introduction to the fundamentals of machine learning.",
                            MeetupId = 2,
                            Topic = "Machine Learning Basics"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Diana Davis",
                            Description = "Analyzing large datasets for actionable insights.",
                            MeetupId = 2,
                            Topic = "Big Data Analytics"
                        });
                });

            modelBuilder.Entity("MeetupAPI.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MeetupId")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MeetupId")
                        .IsUnique();

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Anytown",
                            MeetupId = 1,
                            PostalCode = "12345",
                            Street = "123 Main St"
                        },
                        new
                        {
                            Id = 2,
                            City = "Sciencetown",
                            MeetupId = 2,
                            PostalCode = "54321",
                            Street = "456 Data St"
                        });
                });

            modelBuilder.Entity("MeetupAPI.Entities.Meetup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organizer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Meetups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2023, 12, 13, 20, 52, 25, 951, DateTimeKind.Local).AddTicks(4228),
                            IsPrivate = false,
                            Name = "Tech Meetup",
                            Organizer = "John Doe"
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2023, 12, 20, 20, 52, 25, 953, DateTimeKind.Local).AddTicks(3076),
                            IsPrivate = true,
                            Name = "Data Science Forum",
                            Organizer = "Alice Johnson"
                        });
                });

            modelBuilder.Entity("MeetupAPI.Entities.Lecture", b =>
                {
                    b.HasOne("MeetupAPI.Entities.Meetup", null)
                        .WithMany("Lectures")
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MeetupAPI.Entities.Location", b =>
                {
                    b.HasOne("MeetupAPI.Entities.Meetup", null)
                        .WithOne("Location")
                        .HasForeignKey("MeetupAPI.Entities.Location", "MeetupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MeetupAPI.Entities.Meetup", b =>
                {
                    b.Navigation("Lectures");

                    b.Navigation("Location")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}