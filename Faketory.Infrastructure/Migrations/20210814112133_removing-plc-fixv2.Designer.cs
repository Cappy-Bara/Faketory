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
    [Migration("20210814112133_removing-plc-fixv2")]
    partial class removingplcfixv2
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

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Plcs");
                });

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.PlcModel", b =>
                {
                    b.Property<int>("CpuModel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cpu")
                        .HasColumnType("int");

                    b.Property<short>("Rack")
                        .HasColumnType("smallint");

                    b.Property<short>("Slot")
                        .HasColumnType("smallint");

                    b.HasKey("CpuModel");

                    b.ToTable("PlcModels");

                    b.HasData(
                        new
                        {
                            CpuModel = 1200,
                            Cpu = 30,
                            Rack = (short)0,
                            Slot = (short)1
                        },
                        new
                        {
                            CpuModel = 1500,
                            Cpu = 40,
                            Rack = (short)0,
                            Slot = (short)1
                        },
                        new
                        {
                            CpuModel = 300,
                            Cpu = 10,
                            Rack = (short)0,
                            Slot = (short)2
                        },
                        new
                        {
                            CpuModel = 400,
                            Cpu = 20,
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

                    b.HasIndex("PlcId")
                        .IsUnique()
                        .HasFilter("[PlcId] IS NOT NULL");

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

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.Slot", b =>
                {
                    b.HasOne("Faketory.Domain.Resources.PLCRelated.PlcEntity", "Plc")
                        .WithOne()
                        .HasForeignKey("Faketory.Domain.Resources.PLCRelated.Slot", "PlcId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Plc");
                });

            modelBuilder.Entity("Faketory.Domain.Resources.PLCRelated.Slot", b =>
                {
                    b.Navigation("InputsOutputs");
                });
#pragma warning restore 612, 618
        }
    }
}
