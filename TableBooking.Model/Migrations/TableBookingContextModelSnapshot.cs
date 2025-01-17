﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TableBooking.Model;

#nullable disable

namespace TableBooking.Model.Migrations
{
    [DbContext(typeof(TableBookingContext))]
    partial class TableBookingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TableBooking.Model.Models.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TableBooking.Model.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("AppRoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TableBooking.Model.Models.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AmountOfPeople")
                        .HasColumnType("integer");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("integer");

                    b.Property<Guid>("TableId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("TableId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("TableBooking.Model.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("DateOfRating")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NumberOfLikes")
                        .HasColumnType("integer");

                    b.Property<int>("RatingStars")
                        .HasColumnType("integer");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("TableBooking.Model.Models.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CloseTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<DateTime>("OpenTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("PrimaryImageUrl")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasDefaultValue("https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png");

                    b.Property<double>("Rating")
                        .HasPrecision(1, 1)
                        .HasColumnType("double precision");

                    b.Property<string>("SecondaryImageUrl")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasDefaultValue("https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/240px-No_image_available.svg.png");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("TableBooking.Model.Models.RevokedToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RevokedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.HasKey("Id");

                    b.ToTable("RevokedTokens");
                });

            modelBuilder.Entity("TableBooking.Model.Models.Table", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("integer");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("TableBooking.Model.Models.AppUser", b =>
                {
                    b.HasOne("TableBooking.Model.Models.AppRole", "AppRole")
                        .WithMany()
                        .HasForeignKey("AppRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppRole");
                });

            modelBuilder.Entity("TableBooking.Model.Models.Booking", b =>
                {
                    b.HasOne("TableBooking.Model.Models.AppUser", null)
                        .WithMany("Bookings")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TableBooking.Model.Models.Table", null)
                        .WithMany("Bookings")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TableBooking.Model.Models.Rating", b =>
                {
                    b.HasOne("TableBooking.Model.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TableBooking.Model.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("TableBooking.Model.Models.Table", b =>
                {
                    b.HasOne("TableBooking.Model.Models.Restaurant", null)
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TableBooking.Model.Models.AppUser", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("TableBooking.Model.Models.Restaurant", b =>
                {
                    b.Navigation("Tables");
                });

            modelBuilder.Entity("TableBooking.Model.Models.Table", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
