﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StarCinema_Api.Data;

#nullable disable

namespace StarCinema_Api.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StarCinema_Api.Data.Entities.BookingDetail", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("bookingId")
                        .HasColumnType("int");

                    b.Property<int>("seatId")
                        .HasColumnType("int");

                    b.Property<int>("ticketId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("bookingId");

                    b.HasIndex("seatId");

                    b.HasIndex("ticketId");

                    b.ToTable("BookingDetails");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Bookings", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Categories", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Films", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("categoryid")
                        .HasColumnType("int");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("director")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("duration")
                        .HasColumnType("int");

                    b.Property<string>("isDelete")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("producer")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("release")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("categoryid");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Images", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("filmId")
                        .HasColumnType("int");

                    b.Property<string>("path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("filmId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Rooms", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("isDelete")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Schedules", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("endTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("filmId")
                        .HasColumnType("int");

                    b.Property<int>("roomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("startTime")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("filmId");

                    b.HasIndex("roomId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Seats", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("isDelete")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("roomId")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("roomId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Tickets", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<int>("scheduleId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("scheduleId")
                        .IsUnique();

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("gender")
                        .HasColumnType("bit");

                    b.Property<bool?>("isDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("isEmailVerified")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("phone")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("roleId")
                        .HasColumnType("int");

                    b.Property<string>("token")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("id");

                    b.HasIndex("roleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.BookingDetail", b =>
                {
                    b.HasOne("StarCinema_Api.Data.Entities.Bookings", "booking")
                        .WithMany("bookingDetails")
                        .HasForeignKey("bookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarCinema_Api.Data.Entities.Seats", "seat")
                        .WithMany("bookingDetails")
                        .HasForeignKey("seatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarCinema_Api.Data.Entities.Tickets", "ticket")
                        .WithMany("BookingDetails")
                        .HasForeignKey("ticketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("booking");

                    b.Navigation("seat");

                    b.Navigation("ticket");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Bookings", b =>
                {
                    b.HasOne("StarCinema_Api.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Films", b =>
                {
                    b.HasOne("StarCinema_Api.Data.Entities.Categories", "category")
                        .WithMany("films")
                        .HasForeignKey("categoryid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Images", b =>
                {
                    b.HasOne("StarCinema_Api.Data.Entities.Films", "film")
                        .WithMany("images")
                        .HasForeignKey("filmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("film");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Schedules", b =>
                {
                    b.HasOne("StarCinema_Api.Data.Entities.Films", "film")
                        .WithMany("Schedules")
                        .HasForeignKey("filmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarCinema_Api.Data.Entities.Rooms", "room")
                        .WithMany("schedules")
                        .HasForeignKey("roomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("film");

                    b.Navigation("room");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Seats", b =>
                {
                    b.HasOne("StarCinema_Api.Data.Entities.Rooms", "room")
                        .WithMany("seats")
                        .HasForeignKey("roomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("room");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Tickets", b =>
                {
                    b.HasOne("StarCinema_Api.Data.Entities.Schedules", "schedule")
                        .WithOne("ticket")
                        .HasForeignKey("StarCinema_Api.Data.Entities.Tickets", "scheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("schedule");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.User", b =>
                {
                    b.HasOne("StarCinema_Api.Data.Entities.Role", "role")
                        .WithMany("Users")
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("role");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Bookings", b =>
                {
                    b.Navigation("bookingDetails");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Categories", b =>
                {
                    b.Navigation("films");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Films", b =>
                {
                    b.Navigation("Schedules");

                    b.Navigation("images");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Rooms", b =>
                {
                    b.Navigation("schedules");

                    b.Navigation("seats");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Schedules", b =>
                {
                    b.Navigation("ticket")
                        .IsRequired();
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Seats", b =>
                {
                    b.Navigation("bookingDetails");
                });

            modelBuilder.Entity("StarCinema_Api.Data.Entities.Tickets", b =>
                {
                    b.Navigation("BookingDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
