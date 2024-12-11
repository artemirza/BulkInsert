﻿// <auto-generated />
using System;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Models.TripData", b =>
                {
                    b.Property<DateTime>("tpep_pickup_datetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("tpep_dropoff_datetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("passenger_count")
                        .HasColumnType("int");

                    b.Property<int>("DOLocationID")
                        .HasColumnType("int");

                    b.Property<int>("PULocationID")
                        .HasColumnType("int");

                    b.Property<decimal>("fare_amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("store_and_fwd_flag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("tip_amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("trip_distance")
                        .HasColumnType("float");

                    b.HasKey("tpep_pickup_datetime", "tpep_dropoff_datetime", "passenger_count");

                    b.ToTable("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
