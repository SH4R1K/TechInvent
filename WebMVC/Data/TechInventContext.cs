using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.Data;

public partial class TechInventContext : DbContext
{
    public TechInventContext()
    {
    }

    public TechInventContext(DbContextOptions<TechInventContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdapterType> AdapterTypes { get; set; }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Gpu> Gpus { get; set; }

    public virtual DbSet<Mainboard> Mainboards { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<NetAdapter> NetAdapters { get; set; }

    public virtual DbSet<Os> Os { get; set; }

    public virtual DbSet<Processor> Processors { get; set; }

    public virtual DbSet<Ram> Rams { get; set; }

    public virtual DbSet<Workplace> Workplaces { get; set; }

    public virtual DbSet<Software> Softwares { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdapterType>(entity =>
        {
            entity.HasKey(e => e.IdAdapterType).HasName("PRIMARY");

            entity.ToTable("adapter_type");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.IdAdapterType).HasColumnName("id_adapter_type");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Cabinet>(entity =>
        {
            entity.HasKey(e => e.IdCabinet).HasName("PRIMARY");

            entity.ToTable("cabinet");

            entity.HasIndex(e => e.IdCabinet, "id_cabinet_UNIQUE").IsUnique();

            entity.Property(e => e.IdCabinet).HasColumnName("id_cabinet");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Component>().UseTptMappingStrategy();
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.IdComponent).HasName("PRIMARY");

            entity.ToTable("component");

            entity.HasIndex(e => e.IdWorkplace, "fk_hardware_component_workplace1_idx");

            entity.Property(e => e.IdComponent).HasColumnName("id_component");
            entity.Property(e => e.IdWorkplace).HasColumnName("id_workplace");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.IdWorkplaceNavigation).WithMany(p => p.Components)
                .HasForeignKey(d => d.IdWorkplace)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_hardware_component_workplace1");
        });

        modelBuilder.Entity<Gpu>(entity =>
        {
            entity.ToTable("gpu");

            entity.Property(e => e.IdComponent).HasColumnName("id_component");
            entity.Property(e => e.AdapterRam)
                .HasMaxLength(100)
                .HasColumnName("adapter_ram");
            entity.Property(e => e.Uuid)
                .HasMaxLength(100)
                .HasColumnName("uuid");

            entity.HasOne(d => d.IdComponentNavigation).WithOne(p => p.Gpu)
                .HasForeignKey<Gpu>(d => d.IdComponent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_gpu_component1");
        });
        modelBuilder.Entity<Disk>(entity =>
        {
            entity.ToTable("disk");

            entity.Property(e => e.IdComponent).HasColumnName("id_component");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("model");
            entity.Property(e => e.Size)
                .HasColumnName("size");

            entity.HasOne(d => d.IdComponentNavigation).WithOne(p => p.Disk)
                .HasForeignKey<Disk>(d => d.IdComponent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_disk_component1");
        });

        modelBuilder.Entity<Mainboard>(entity =>
        {
            entity.ToTable("mainboard");

            entity.Property(e => e.IdComponent).HasColumnName("id_component");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(100)
                .HasColumnName("serial_number");

            entity.HasOne(d => d.IdComponentNavigation).WithOne(p => p.Mainboard)
                .HasForeignKey<Mainboard>(d => d.IdComponent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_mainboard_component1");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.IdManufacturer).HasName("PRIMARY");

            entity.ToTable("manufacturer");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.IdManufacturer).HasColumnName("id_manufacturer");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<NetAdapter>(entity =>
        {
            entity.ToTable("net_adapter");

            entity.HasIndex(e => e.AdapterTypeIdAdapterType, "fk_net_adapter_adapter_type1_idx");

            entity.HasIndex(e => e.IdManufacturer, "fk_net_adapter_manufacturer2_idx");

            entity.HasIndex(e => e.IdComponent, "fk_ram_component1_idx");

            entity.Property(e => e.IdComponent).HasColumnName("id_component");
            entity.Property(e => e.AdapterTypeIdAdapterType).HasColumnName("adapter_type_id_adapter_type");
            entity.Property(e => e.IdManufacturer).HasColumnName("id_manufacturer");
            entity.Property(e => e.MacAddress)
                .HasMaxLength(100)
                .HasColumnName("mac_address");

            entity.HasOne(d => d.AdapterTypeIdAdapterTypeNavigation).WithMany(p => p.NetAdapters)
                .HasForeignKey(d => d.AdapterTypeIdAdapterType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_net_adapter_adapter_type1");

            entity.HasOne(d => d.IdComponentNavigation).WithOne(p => p.NetAdapter)
                .HasForeignKey<NetAdapter>(d => d.IdComponent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_ram_component1");

            entity.HasOne(d => d.IdManufacturerNavigation).WithMany(p => p.NetAdapters)
                .HasForeignKey(d => d.IdManufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_net_adapter_manufacturer2");
        });

        modelBuilder.Entity<Os>(entity =>
        {
            entity.HasKey(e => e.IdOs).HasName("PRIMARY");

            entity.ToTable("os");

            entity.HasIndex(e => new { e.OsName, e.OsVersion }, "UQ_name_version").IsUnique();

            entity.Property(e => e.IdOs).HasColumnName("id_os");
            entity.Property(e => e.OsName)
                .HasMaxLength(100)
                .HasColumnName("os_name");
            entity.Property(e => e.OsVersion)
                .HasMaxLength(125)
                .HasColumnName("os_version");
        });

        modelBuilder.Entity<Processor>(entity =>
        {
            entity.ToTable("processor");

            entity.Property(e => e.IdComponent).HasColumnName("id_component");
            entity.Property(e => e.LogicalCores)
                .HasMaxLength(100)
                .HasColumnName("logical_cores");
            entity.Property(e => e.MaxClockSpeed)
                .HasMaxLength(100)
                .HasColumnName("max_clock_speed");
            entity.Property(e => e.PhysicalCores)
                .HasMaxLength(100)
                .HasColumnName("physical_cores");

            entity.HasOne(d => d.IdComponentNavigation).WithOne(p => p.Processor)
                .HasForeignKey<Processor>(d => d.IdComponent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_processor_component1");
        });

        modelBuilder.Entity<Ram>(entity =>
        {
            entity.ToTable("ram");

            entity.HasIndex(e => e.IdManufacturer, "fk_net_adapter_manufacturer1_idx");

            entity.Property(e => e.IdComponent).HasColumnName("id_component");
            entity.Property(e => e.Capacity)
                .HasMaxLength(100)
                .HasColumnName("capacity");
            entity.Property(e => e.IdManufacturer).HasColumnName("id_manufacturer");
            entity.Property(e => e.MemoryType)
                .HasMaxLength(100)
                .HasColumnName("memory_type");
            entity.Property(e => e.PartNumber)
                .HasMaxLength(100)
                .HasColumnName("part_number");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(100)
                .HasColumnName("serial_number");
            entity.Property(e => e.Speed)
                .HasMaxLength(100)
                .HasColumnName("speed");

            entity.HasOne(d => d.IdComponentNavigation).WithOne(p => p.Ram)
                .HasForeignKey<Ram>(d => d.IdComponent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_net_adapter_component1");

            entity.HasOne(d => d.IdManufacturerNavigation).WithMany(p => p.Rams)
                .HasForeignKey(d => d.IdManufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_net_adapter_manufacturer1");
        });

        modelBuilder.Entity<Workplace>(entity =>
        {
            entity.HasKey(e => e.IdWorkplace).HasName("PRIMARY");
            entity.ToTable("workplace");

            entity.HasIndex(e => e.IdCabinet, "fk_workplace_cabinet_idx");

            entity.HasIndex(e => e.IdOs, "fk_workplace_os1_idx");

            entity.HasIndex(e => e.Guid, "uq_workplace_guid").IsUnique();

            entity.Property(e => e.IdWorkplace).HasColumnName("id_workplace");
            entity.Property(e => e.IdCabinet).HasColumnName("id_cabinet");
            entity.Property(e => e.IdOs).HasColumnName("id_os");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Guid)
                .HasDefaultValue(null)
                .HasColumnName("guid");
            entity.Property(e => e.LastUpdate)
                .HasColumnName("last_update");

            entity.HasOne(d => d.IdCabinetNavigation).WithMany(p => p.Workplaces)
                .HasForeignKey(d => d.IdCabinet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_workplace_cabinet");

            entity.HasOne(d => d.IdOsNavigation).WithMany(p => p.Workplaces)
                .HasForeignKey(d => d.IdOs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_workplace_os1");
        });

        modelBuilder.Entity<Software>(entity =>
        {
            entity.HasKey(e => e.IdSoftware).HasName("PRIMARY");
            entity.ToTable("software");

            entity.HasIndex(e => e.IdManufacturer, "fk_software_manufacturer");

            entity.Property(e => e.IdSoftware).HasColumnName("id_software");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name"); ;
            entity.Property(e => e.Version)
                .HasMaxLength(100)
                .HasColumnName("version");
            entity.Property(e => e.IdManufacturer).HasColumnName("id_manufacturer");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Softwares)
                .HasForeignKey(d => d.IdManufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_software_manufacturer");
        });

        modelBuilder.Entity<InstalledSoftware>(entity =>
        {
            entity.HasKey(e => new { e.IdSoftware, e.IdWorkplace }).HasName("PRIMARY");
            entity.ToTable("installed_software");

            entity.Property(e => e.IdSoftware).HasColumnName("id_software");
            entity.Property(e => e.IdWorkplace).HasColumnName("id_workplace");

            entity.HasOne(d => d.SoftwareNavigation).WithMany(p => p.InstalledSoftware)
                .HasForeignKey(d => d.IdSoftware)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_software_installed_software");

            entity.HasOne(d => d.WorkplaceNavigation).WithMany(p => p.InstalledSoftware)
                .HasForeignKey(d => d.IdWorkplace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_workplace_installed_software");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");
            entity.ToTable("user");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login).HasColumnName("login").HasMaxLength(100);
            entity.Property(e => e.Password).HasColumnName("password").HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");
            entity.ToTable("role");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<WebMVC.Models.User> User { get; set; } = default!;
}
