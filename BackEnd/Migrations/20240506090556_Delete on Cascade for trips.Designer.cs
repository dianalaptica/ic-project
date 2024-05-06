﻿// <auto-generated />
using System;
using BackEnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackEnd.Migrations
{
    [DbContext(typeof(ToursitDbContext))]
    [Migration("20240506090556_Delete on Cascade for trips")]
    partial class DeleteonCascadefortrips
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackEnd.Domain.Models.AppliedForGuide", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<byte[]>("IdentityCard")
                        .IsRequired()
                        .HasColumnType("image");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.HasIndex("CityId");

                    b.ToTable("AppliedForGuide", (string)null);
                });

            modelBuilder.Entity("BackEnd.Domain.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("BackEnd.Domain.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("BackEnd.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("BackEnd.Domain.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<int>("GuideId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<int>("MaxTourists")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("GuideId");

                    b.ToTable("Trip", (string)null);
                });

            modelBuilder.Entity("BackEnd.Domain.Models.TripNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("TripNotification", (string)null);
                });

            modelBuilder.Entity("BackEnd.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("image");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("BackEnd.Domain.Models.UserNotification", b =>
                {
                    b.Property<int>("NotificationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.HasKey("NotificationId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserNotification", (string)null);
                });

            modelBuilder.Entity("UserTrip", b =>
                {
                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TripId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTrip", (string)null);
                });

            modelBuilder.Entity("BackEnd.Domain.Models.AppliedForGuide", b =>
                {
                    b.HasOne("BackEnd.Domain.Models.City", "City")
                        .WithMany("AppliedForGuides")
                        .HasForeignKey("CityId")
                        .IsRequired()
                        .HasConstraintName("FK_AppliedForGuide_City");

                    b.Navigation("City");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.City", b =>
                {
                    b.HasOne("BackEnd.Domain.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .IsRequired()
                        .HasConstraintName("FK_City_Country");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.Trip", b =>
                {
                    b.HasOne("BackEnd.Domain.Models.City", "City")
                        .WithMany("Trips")
                        .HasForeignKey("CityId")
                        .IsRequired()
                        .HasConstraintName("FK_Trip_City");

                    b.HasOne("BackEnd.Domain.Models.AppliedForGuide", "Guide")
                        .WithMany("Trips")
                        .HasForeignKey("GuideId")
                        .IsRequired()
                        .HasConstraintName("FK_Trip_AppliedForGuide");

                    b.Navigation("City");

                    b.Navigation("Guide");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.TripNotification", b =>
                {
                    b.HasOne("BackEnd.Domain.Models.Trip", "Trip")
                        .WithMany("TripNotifications")
                        .HasForeignKey("TripId")
                        .IsRequired()
                        .HasConstraintName("FK_TripNotification_Trip");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.User", b =>
                {
                    b.HasOne("BackEnd.Domain.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_User_Role");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.UserNotification", b =>
                {
                    b.HasOne("BackEnd.Domain.Models.TripNotification", "Notification")
                        .WithMany("UserNotifications")
                        .HasForeignKey("NotificationId")
                        .IsRequired()
                        .HasConstraintName("FK_UserNotification_TripNotification");

                    b.HasOne("BackEnd.Domain.Models.User", "User")
                        .WithMany("UserNotifications")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserNotification_User");

                    b.Navigation("Notification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserTrip", b =>
                {
                    b.HasOne("BackEnd.Domain.Models.Trip", null)
                        .WithMany()
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserTrip_Trip");

                    b.HasOne("BackEnd.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserTrip_User");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.AppliedForGuide", b =>
                {
                    b.Navigation("Trips");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.City", b =>
                {
                    b.Navigation("AppliedForGuides");

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.Trip", b =>
                {
                    b.Navigation("TripNotifications");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.TripNotification", b =>
                {
                    b.Navigation("UserNotifications");
                });

            modelBuilder.Entity("BackEnd.Domain.Models.User", b =>
                {
                    b.Navigation("UserNotifications");
                });
#pragma warning restore 612, 618
        }
    }
}
