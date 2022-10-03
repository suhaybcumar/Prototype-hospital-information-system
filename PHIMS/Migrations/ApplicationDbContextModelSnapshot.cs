﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PHIMS.Data;

#nullable disable

namespace PHIMS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PHIMS.Models.Admin", b =>
                {
                    b.Property<int>("Admin_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Admin_Id"));

                    b.Property<DateTime>("DOB")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("F_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("L_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("M_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Phone_Number")
                        .HasColumnType("integer");

                    b.Property<DateTime>("R_Date")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Admin_Id");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("PHIMS.Models.Doctor", b =>
                {
                    b.Property<int>("Doctor_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Doctor_Id"));

                    b.Property<int>("Admin_Id")
                        .HasColumnType("integer");

                    b.Property<bool>("Available")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Employee_Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("F_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("L_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("M_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("National_Id")
                        .HasColumnType("integer");

                    b.Property<int>("Phone_Number")
                        .HasColumnType("integer");

                    b.Property<DateTime>("R_Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Specializations")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Working_days")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Working_hours")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Doctor_Id");

                    b.HasIndex("Admin_Id");

                    b.HasIndex("UserId");

                    b.ToTable("Doctor", (string)null);
                });

            modelBuilder.Entity("PHIMS.Models.Lab", b =>
                {
                    b.Property<Guid>("Lab_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Doctor_Id")
                        .HasColumnType("integer");

                    b.Property<int>("Pateint_Id")
                        .HasColumnType("integer");

                    b.Property<string>("Prescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Lab_Id");

                    b.HasIndex("Doctor_Id");

                    b.HasIndex("Pateint_Id");

                    b.ToTable("Lab", (string)null);
                });

            modelBuilder.Entity("PHIMS.Models.Pateint", b =>
                {
                    b.Property<int>("Pateint_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Pateint_Id"));

                    b.Property<bool>("Approve")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Doctr_Id")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("F_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fee")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("L_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("M_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MyProperty")
                        .HasColumnType("integer");

                    b.Property<int>("Phone_Number")
                        .HasColumnType("integer");

                    b.Property<DateTime>("R_Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("User_Id")
                        .HasColumnType("integer");

                    b.HasKey("Pateint_Id");

                    b.HasIndex("Doctr_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Pateint", (string)null);
                });

            modelBuilder.Entity("PHIMS.Models.Result", b =>
                {
                    b.Property<int>("Id_Result")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id_Result"));

                    b.Property<string>("Advice")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Doctor_ID")
                        .HasColumnType("integer");

                    b.Property<int>("Pateint_ID")
                        .HasColumnType("integer");

                    b.Property<string>("result")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id_Result");

                    b.HasIndex("Doctor_ID");

                    b.HasIndex("Pateint_ID");

                    b.ToTable("Result", (string)null);
                });

            modelBuilder.Entity("PHIMS.Models.User", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("User_Id"));

                    b.Property<int>("Admin_Id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("F_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("L_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("M_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Phone_Number")
                        .HasColumnType("integer");

                    b.Property<DateTime>("R_Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserIdId")
                        .HasColumnType("text");

                    b.HasKey("User_Id");

                    b.HasIndex("Admin_Id");

                    b.HasIndex("UserIdId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("PHIMS.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PHIMS.Models.Doctor", b =>
                {
                    b.HasOne("PHIMS.Models.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("Admin_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PHIMS.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("PHIMS.Models.Lab", b =>
                {
                    b.HasOne("PHIMS.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("Doctor_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PHIMS.Models.Pateint", "Pateint")
                        .WithMany()
                        .HasForeignKey("Pateint_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Pateint");
                });

            modelBuilder.Entity("PHIMS.Models.Pateint", b =>
                {
                    b.HasOne("PHIMS.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("Doctr_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PHIMS.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PHIMS.Models.Result", b =>
                {
                    b.HasOne("PHIMS.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("Doctor_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PHIMS.Models.Pateint", "Pateint")
                        .WithMany()
                        .HasForeignKey("Pateint_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Pateint");
                });

            modelBuilder.Entity("PHIMS.Models.User", b =>
                {
                    b.HasOne("PHIMS.Models.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("Admin_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PHIMS.Models.ApplicationUser", "UserId")
                        .WithMany()
                        .HasForeignKey("UserIdId");

                    b.Navigation("Admin");

                    b.Navigation("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
