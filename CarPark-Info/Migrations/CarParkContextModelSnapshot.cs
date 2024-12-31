﻿// <auto-generated />
using CarPark_Info.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarPark_Info.Migrations
{
    [DbContext(typeof(CarParkContext))]
    partial class CarParkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.20");

            modelBuilder.Entity("CarPark_Info.Models.CarPark", b =>
                {
                    b.Property<string>("car_park_no")
                        .HasColumnType("TEXT");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("car_park_basement")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("car_park_decks")
                        .HasColumnType("INTEGER");

                    b.Property<string>("car_park_type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("free_parking")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("gantry_height")
                        .HasColumnType("REAL");

                    b.Property<string>("night_parking")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("short_term_parking")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("type_of_parking_system")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("x_coord")
                        .HasColumnType("REAL");

                    b.Property<double>("y_coord")
                        .HasColumnType("REAL");

                    b.HasKey("car_park_no");

                    b.ToTable("CarParks");
                });

            modelBuilder.Entity("CarPark_Info.Models.Favourite", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("car_park_no")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "car_park_no");

                    b.HasIndex("car_park_no");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("CarPark_Info.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = "a1",
                            email = "testuser1@example.com",
                            username = "Test User1"
                        },
                        new
                        {
                            UserId = "a2",
                            email = "testuser2@example.com",
                            username = "Test User2"
                        },
                        new
                        {
                            UserId = "a3",
                            email = "testuser3@example.com",
                            username = "Test User3"
                        });
                });

            modelBuilder.Entity("CarPark_Info.Models.Favourite", b =>
                {
                    b.HasOne("CarPark_Info.Models.User", "User")
                        .WithMany("Favourites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarPark_Info.Models.CarPark", "CarPark")
                        .WithMany("Favourites")
                        .HasForeignKey("car_park_no")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarPark");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CarPark_Info.Models.CarPark", b =>
                {
                    b.Navigation("Favourites");
                });

            modelBuilder.Entity("CarPark_Info.Models.User", b =>
                {
                    b.Navigation("Favourites");
                });
#pragma warning restore 612, 618
        }
    }
}
