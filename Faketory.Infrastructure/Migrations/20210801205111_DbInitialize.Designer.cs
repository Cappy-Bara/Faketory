﻿// <auto-generated />
using System;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Faketory.Infrastructure.Migrations
{
    [DbContext(typeof(FaketoryDbContext))]
    [Migration("20210801205111_DbInitialize")]
    partial class DbInitialize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.IO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Bit")
                        .HasColumnType("int");

                    b.Property<int>("Byte")
                        .HasColumnType("int");

                    b.Property<Guid?>("SlotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<bool>("Value")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SlotId");

                    b.ToTable("InputsOutputs");
                });

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.PlcEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<Guid?>("SlotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.HasIndex("SlotId")
                        .IsUnique()
                        .HasFilter("[SlotId] IS NOT NULL");

                    b.ToTable("Plcs");
                });

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.PlcModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cpu")
                        .HasColumnType("int");

                    b.Property<int>("CpuModel")
                        .HasColumnType("int");

                    b.Property<short>("Rack")
                        .HasColumnType("smallint");

                    b.Property<short>("Slot")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("PlcModels");

                    b.HasData(
                        new
                        {
                            Id = 1200,
                            Cpu = 30,
                            CpuModel = 1200,
                            Rack = (short)0,
                            Slot = (short)1
                        },
                        new
                        {
                            Id = 1500,
                            Cpu = 40,
                            CpuModel = 1500,
                            Rack = (short)0,
                            Slot = (short)1
                        },
                        new
                        {
                            Id = 300,
                            Cpu = 10,
                            CpuModel = 300,
                            Rack = (short)0,
                            Slot = (short)2
                        },
                        new
                        {
                            Id = 400,
                            Cpu = 20,
                            CpuModel = 400,
                            Rack = (short)0,
                            Slot = (short)2
                        });
                });

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.Slot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<Guid?>("PlcId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.IO", b =>
                {
                    b.HasOne("Faketory.Domain.Resources.PLCRelated.Slot", null)
                        .WithMany("InputsOutputs")
                        .HasForeignKey("SlotId");
                });

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.PlcEntity", b =>
                {
                    b.HasOne("Faketory.Domain.Resources.PLCRelated.PlcModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Faketory.Domain.Resources.PLCRelated.Slot", null)
                        .WithOne("Plc")
                        .HasForeignKey("Faketory.Domain.Resources.PLCRelated.PlcEntity", "SlotId");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.Slot", b =>
                {
                    b.Navigation("InputsOutputs");

                    b.Navigation("Plc");
                });
#pragma warning restore 612, 618
        }
    }
}
