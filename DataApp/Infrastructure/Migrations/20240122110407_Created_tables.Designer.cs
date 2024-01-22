﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(LocalDataContext))]
    [Migration("20240122110407_Created_tables")]
    partial class Created_tables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Entities.AddressEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Infrastructure.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("RoleName")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Infrastructure.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("date");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Guid");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Infrastructure.Entities.UserProfileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Infrastructure.Entities.UserRoleEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserGuid");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Infrastructure.Entities.UserEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.AddressEntity", "Address")
                        .WithOne("User")
                        .HasForeignKey("Infrastructure.Entities.UserEntity", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.UserProfileEntity", "UserProfile")
                        .WithOne("User")
                        .HasForeignKey("Infrastructure.Entities.UserEntity", "RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Infrastructure.Entities.UserRoleEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.RoleEntity", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Infrastructure.Entities.AddressEntity", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Entities.RoleEntity", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Infrastructure.Entities.UserEntity", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Infrastructure.Entities.UserProfileEntity", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
