﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModuleRegistration.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ModuleRegistration.Migrations
{
    [DbContext(typeof(ModuleRegistrationContext))]
    partial class ModuleRegistrationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ModuleRegistration.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ClassCode")
                        .IsRequired()
                        .HasColumnName("class_code");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code");

                    b.Property<string>("CoordinatorUid")
                        .IsRequired()
                        .HasColumnName("coordinator_uid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.ToTable("module");
                });

            modelBuilder.Entity("ModuleRegistration.Models.ModuleStaff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("ModuleId");

                    b.Property<string>("StaffUid");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("StaffUid");

                    b.ToTable("module_staff");
                });

            modelBuilder.Entity("ModuleRegistration.Models.ModuleStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("ModuleId");

                    b.Property<string>("StudentUid");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("StudentUid");

                    b.ToTable("module_student");
                });

            modelBuilder.Entity("ModuleRegistration.Models.Staff", b =>
                {
                    b.Property<string>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("uid");

                    b.Property<string>("Forename")
                        .HasColumnName("forename");

                    b.Property<string>("Surname")
                        .HasColumnName("surname");

                    b.HasKey("Uid");

                    b.ToTable("staff");
                });

            modelBuilder.Entity("ModuleRegistration.Models.Student", b =>
                {
                    b.Property<string>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("uid");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasColumnName("forename");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnName("surname");

                    b.HasKey("Uid");

                    b.ToTable("student");
                });

            modelBuilder.Entity("ModuleRegistration.Models.ModuleStaff", b =>
                {
                    b.HasOne("ModuleRegistration.Models.Module", "Module")
                        .WithMany("ModuleStaff")
                        .HasForeignKey("ModuleId");

                    b.HasOne("ModuleRegistration.Models.Staff", "Staff")
                        .WithMany("ModuleStaff")
                        .HasForeignKey("StaffUid");
                });

            modelBuilder.Entity("ModuleRegistration.Models.ModuleStudent", b =>
                {
                    b.HasOne("ModuleRegistration.Models.Module", "Module")
                        .WithMany("ModuleStudents")
                        .HasForeignKey("ModuleId");

                    b.HasOne("ModuleRegistration.Models.Student", "Student")
                        .WithMany("ModuleStudents")
                        .HasForeignKey("StudentUid");
                });
#pragma warning restore 612, 618
        }
    }
}
