﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechInventAPI.Data;

#nullable disable

namespace TechInventAPI.Migrations
{
    [DbContext(typeof(TechInventContext))]
    [Migration("20241202202305_guids")]
    partial class guids
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TechInventAPI.Models.AdapterType", b =>
                {
                    b.Property<int>("IdAdapterType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_adapter_type");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("IdAdapterType")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Name" }, "name_UNIQUE")
                        .IsUnique();

                    b.ToTable("adapter_type", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Cabinet", b =>
                {
                    b.Property<int>("IdCabinet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_cabinet");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("IdCabinet")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdCabinet" }, "id_cabinet_UNIQUE")
                        .IsUnique();

                    b.ToTable("cabinet", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Component", b =>
                {
                    b.Property<int>("IdComponent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_component");

                    b.Property<int>("IdWorkplace")
                        .HasColumnType("int")
                        .HasColumnName("id_workplace");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("IdComponent")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdWorkplace" }, "fk_hardware_component_workplace1_idx");

                    b.ToTable("component", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("TechInventAPI.Models.InstalledSoftware", b =>
                {
                    b.Property<int>("IdSoftware")
                        .HasColumnType("int")
                        .HasColumnName("id_software");

                    b.Property<int>("IdWorkplace")
                        .HasColumnType("int")
                        .HasColumnName("id_workplace");

                    b.HasKey("IdSoftware", "IdWorkplace")
                        .HasName("PRIMARY");

                    b.HasIndex("IdWorkplace");

                    b.ToTable("installed_software", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Manufacturer", b =>
                {
                    b.Property<int>("IdManufacturer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_manufacturer");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("IdManufacturer")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Name" }, "name_UNIQUE")
                        .IsUnique()
                        .HasDatabaseName("name_UNIQUE1");

                    b.ToTable("manufacturer", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Os", b =>
                {
                    b.Property<int>("IdOs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_os");

                    b.Property<string>("OsName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("os_name");

                    b.Property<string>("OsVersion")
                        .HasMaxLength(125)
                        .HasColumnType("varchar(125)")
                        .HasColumnName("os_version");

                    b.HasKey("IdOs")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "OsName", "OsVersion" }, "UQ_name_version")
                        .IsUnique();

                    b.ToTable("os", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Role", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_role");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.HasKey("IdRole")
                        .HasName("PRIMARY");

                    b.ToTable("role", (string)null);

                    b.HasData(
                        new
                        {
                            IdRole = 1,
                            Name = "admin"
                        },
                        new
                        {
                            IdRole = 2,
                            Name = "user"
                        });
                });

            modelBuilder.Entity("TechInventAPI.Models.Software", b =>
                {
                    b.Property<int>("IdSoftware")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_software");

                    b.Property<int>("IdManufacturer")
                        .HasColumnType("int")
                        .HasColumnName("id_manufacturer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("version");

                    b.HasKey("IdSoftware")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdManufacturer" }, "fk_software_manufacturer");

                    b.ToTable("software", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_user");

                    b.Property<int>("IdRole")
                        .HasColumnType("int")
                        .HasColumnName("id_role");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.HasKey("IdUser")
                        .HasName("PRIMARY");

                    b.HasIndex("IdRole");

                    b.ToTable("user", (string)null);

                    b.HasData(
                        new
                        {
                            IdUser = 1,
                            IdRole = 1,
                            Login = "admin",
                            Password = "1"
                        });
                });

            modelBuilder.Entity("TechInventAPI.Models.Workplace", b =>
                {
                    b.Property<int>("IdWorkplace")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_workplace");

                    b.Property<Guid>("Guid")
                        .HasColumnType("char(36)")
                        .HasColumnName("guid");

                    b.Property<int>("IdCabinet")
                        .HasColumnType("int")
                        .HasColumnName("id_cabinet");

                    b.Property<int>("IdOs")
                        .HasColumnType("int")
                        .HasColumnName("id_os");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("IdWorkplace")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdCabinet" }, "fk_workplace_cabinet_idx");

                    b.HasIndex(new[] { "IdOs" }, "fk_workplace_os1_idx");

                    b.HasIndex(new[] { "Guid" }, "uq_workplace_guid")
                        .IsUnique();

                    b.ToTable("workplace", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Disk", b =>
                {
                    b.HasBaseType("TechInventAPI.Models.Component");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("model");

                    b.Property<int>("Size")
                        .HasColumnType("int")
                        .HasColumnName("size");

                    b.ToTable("disk", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Gpu", b =>
                {
                    b.HasBaseType("TechInventAPI.Models.Component");

                    b.Property<string>("AdapterRam")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("adapter_ram");

                    b.Property<string>("Uuid")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("uuid");

                    b.ToTable("gpu", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Mainboard", b =>
                {
                    b.HasBaseType("TechInventAPI.Models.Component");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("serial_number");

                    b.ToTable("mainboard", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.NetAdapter", b =>
                {
                    b.HasBaseType("TechInventAPI.Models.Component");

                    b.Property<int>("AdapterTypeIdAdapterType")
                        .HasColumnType("int")
                        .HasColumnName("adapter_type_id_adapter_type");

                    b.Property<int>("IdManufacturer")
                        .HasColumnType("int")
                        .HasColumnName("id_manufacturer");

                    b.Property<string>("MacAddress")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("mac_address");

                    b.HasIndex(new[] { "AdapterTypeIdAdapterType" }, "fk_net_adapter_adapter_type1_idx");

                    b.HasIndex(new[] { "IdManufacturer" }, "fk_net_adapter_manufacturer2_idx");

                    b.HasIndex(new[] { "IdComponent" }, "fk_ram_component1_idx");

                    b.ToTable("net_adapter", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Processor", b =>
                {
                    b.HasBaseType("TechInventAPI.Models.Component");

                    b.Property<string>("LogicalCores")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("logical_cores");

                    b.Property<string>("MaxClockSpeed")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("max_clock_speed");

                    b.Property<string>("PhysicalCores")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("physical_cores");

                    b.ToTable("processor", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Ram", b =>
                {
                    b.HasBaseType("TechInventAPI.Models.Component");

                    b.Property<string>("Capacity")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("capacity");

                    b.Property<int>("IdManufacturer")
                        .HasColumnType("int")
                        .HasColumnName("id_manufacturer");

                    b.Property<string>("MemoryType")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("memory_type");

                    b.Property<string>("PartNumber")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("part_number");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("serial_number");

                    b.Property<string>("Speed")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("speed");

                    b.HasIndex(new[] { "IdManufacturer" }, "fk_net_adapter_manufacturer1_idx");

                    b.ToTable("ram", (string)null);
                });

            modelBuilder.Entity("TechInventAPI.Models.Component", b =>
                {
                    b.HasOne("TechInventAPI.Models.Workplace", "IdWorkplaceNavigation")
                        .WithMany("Components")
                        .HasForeignKey("IdWorkplace")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_hardware_component_workplace1");

                    b.Navigation("IdWorkplaceNavigation");
                });

            modelBuilder.Entity("TechInventAPI.Models.InstalledSoftware", b =>
                {
                    b.HasOne("TechInventAPI.Models.Software", "SoftwareNavigation")
                        .WithMany("InstalledSoftware")
                        .HasForeignKey("IdSoftware")
                        .IsRequired()
                        .HasConstraintName("fk_software_installed_software");

                    b.HasOne("TechInventAPI.Models.Workplace", "WorkplaceNavigation")
                        .WithMany("InstalledSoftware")
                        .HasForeignKey("IdWorkplace")
                        .IsRequired()
                        .HasConstraintName("fk_workplace_installed_software");

                    b.Navigation("SoftwareNavigation");

                    b.Navigation("WorkplaceNavigation");
                });

            modelBuilder.Entity("TechInventAPI.Models.Software", b =>
                {
                    b.HasOne("TechInventAPI.Models.Manufacturer", "ManufacturerNavigation")
                        .WithMany("Softwares")
                        .HasForeignKey("IdManufacturer")
                        .IsRequired()
                        .HasConstraintName("fk_software_manufacturer");

                    b.Navigation("ManufacturerNavigation");
                });

            modelBuilder.Entity("TechInventAPI.Models.User", b =>
                {
                    b.HasOne("TechInventAPI.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("IdRole")
                        .IsRequired()
                        .HasConstraintName("fk_user_role");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TechInventAPI.Models.Workplace", b =>
                {
                    b.HasOne("TechInventAPI.Models.Cabinet", "IdCabinetNavigation")
                        .WithMany("Workplaces")
                        .HasForeignKey("IdCabinet")
                        .IsRequired()
                        .HasConstraintName("fk_workplace_cabinet");

                    b.HasOne("TechInventAPI.Models.Os", "IdOsNavigation")
                        .WithMany("Workplaces")
                        .HasForeignKey("IdOs")
                        .IsRequired()
                        .HasConstraintName("fk_workplace_os1");

                    b.Navigation("IdCabinetNavigation");

                    b.Navigation("IdOsNavigation");
                });

            modelBuilder.Entity("TechInventAPI.Models.Disk", b =>
                {
                    b.HasOne("TechInventAPI.Models.Component", "IdComponentNavigation")
                        .WithOne("Disk")
                        .HasForeignKey("TechInventAPI.Models.Disk", "IdComponent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_disk_component1");

                    b.Navigation("IdComponentNavigation");
                });

            modelBuilder.Entity("TechInventAPI.Models.Gpu", b =>
                {
                    b.HasOne("TechInventAPI.Models.Component", "IdComponentNavigation")
                        .WithOne("Gpu")
                        .HasForeignKey("TechInventAPI.Models.Gpu", "IdComponent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_gpu_component1");

                    b.Navigation("IdComponentNavigation");
                });

            modelBuilder.Entity("TechInventAPI.Models.Mainboard", b =>
                {
                    b.HasOne("TechInventAPI.Models.Component", "IdComponentNavigation")
                        .WithOne("Mainboard")
                        .HasForeignKey("TechInventAPI.Models.Mainboard", "IdComponent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_mainboard_component1");

                    b.Navigation("IdComponentNavigation");
                });

            modelBuilder.Entity("TechInventAPI.Models.NetAdapter", b =>
                {
                    b.HasOne("TechInventAPI.Models.AdapterType", "AdapterTypeIdAdapterTypeNavigation")
                        .WithMany("NetAdapters")
                        .HasForeignKey("AdapterTypeIdAdapterType")
                        .IsRequired()
                        .HasConstraintName("fk_net_adapter_adapter_type1");

                    b.HasOne("TechInventAPI.Models.Component", "IdComponentNavigation")
                        .WithOne("NetAdapter")
                        .HasForeignKey("TechInventAPI.Models.NetAdapter", "IdComponent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_ram_component1");

                    b.HasOne("TechInventAPI.Models.Manufacturer", "IdManufacturerNavigation")
                        .WithMany("NetAdapters")
                        .HasForeignKey("IdManufacturer")
                        .IsRequired()
                        .HasConstraintName("fk_net_adapter_manufacturer2");

                    b.Navigation("AdapterTypeIdAdapterTypeNavigation");

                    b.Navigation("IdComponentNavigation");

                    b.Navigation("IdManufacturerNavigation");
                });

            modelBuilder.Entity("TechInventAPI.Models.Processor", b =>
                {
                    b.HasOne("TechInventAPI.Models.Component", "IdComponentNavigation")
                        .WithOne("Processor")
                        .HasForeignKey("TechInventAPI.Models.Processor", "IdComponent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_processor_component1");

                    b.Navigation("IdComponentNavigation");
                });

            modelBuilder.Entity("TechInventAPI.Models.Ram", b =>
                {
                    b.HasOne("TechInventAPI.Models.Component", "IdComponentNavigation")
                        .WithOne("Ram")
                        .HasForeignKey("TechInventAPI.Models.Ram", "IdComponent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_net_adapter_component1");

                    b.HasOne("TechInventAPI.Models.Manufacturer", "IdManufacturerNavigation")
                        .WithMany("Rams")
                        .HasForeignKey("IdManufacturer")
                        .IsRequired()
                        .HasConstraintName("fk_net_adapter_manufacturer1");

                    b.Navigation("IdComponentNavigation");

                    b.Navigation("IdManufacturerNavigation");
                });

            modelBuilder.Entity("TechInventAPI.Models.AdapterType", b =>
                {
                    b.Navigation("NetAdapters");
                });

            modelBuilder.Entity("TechInventAPI.Models.Cabinet", b =>
                {
                    b.Navigation("Workplaces");
                });

            modelBuilder.Entity("TechInventAPI.Models.Component", b =>
                {
                    b.Navigation("Disk");

                    b.Navigation("Gpu");

                    b.Navigation("Mainboard");

                    b.Navigation("NetAdapter");

                    b.Navigation("Processor");

                    b.Navigation("Ram");
                });

            modelBuilder.Entity("TechInventAPI.Models.Manufacturer", b =>
                {
                    b.Navigation("NetAdapters");

                    b.Navigation("Rams");

                    b.Navigation("Softwares");
                });

            modelBuilder.Entity("TechInventAPI.Models.Os", b =>
                {
                    b.Navigation("Workplaces");
                });

            modelBuilder.Entity("TechInventAPI.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TechInventAPI.Models.Software", b =>
                {
                    b.Navigation("InstalledSoftware");
                });

            modelBuilder.Entity("TechInventAPI.Models.Workplace", b =>
                {
                    b.Navigation("Components");

                    b.Navigation("InstalledSoftware");
                });
#pragma warning restore 612, 618
        }
    }
}