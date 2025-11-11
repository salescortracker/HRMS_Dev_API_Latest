using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

public partial class HRMSContext : DbContext
{
    public HRMSContext()
    {
    }

    public HRMSContext(DbContextOptions<HRMSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<MenuMaster> MenuMasters { get; set; }

    public virtual DbSet<MenuRoleMaster> MenuRoleMasters { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RoleMaster> RoleMasters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-I445813;Initial Catalog=HRMS_DB;User Id=sa;Password=Password@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971C4C974EDCEC");

            entity.ToTable("Company", "UM");

            entity.HasIndex(e => e.CompanyCode, "UQ__Company__11A0134B951640C5").IsUnique();

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Headquarters)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IndustryType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.DocumentTypeId).HasName("PK__Document__DBA390C194ACA35F");

            entity.ToTable("DocumentType", "UM");

            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DocumentTypeName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<MenuMaster>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__MenuMast__C99ED250D3547D77");

            entity.ToTable("MenuMaster", "UM");

            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MenuName).HasMaxLength(100);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ParentMenuId).HasColumnName("ParentMenuID");
            entity.Property(e => e.Url).HasMaxLength(255);
        });

        modelBuilder.Entity<MenuRoleMaster>(entity =>
        {
            entity.HasKey(e => e.MenuRoleId).HasName("PK__MenuRole__880F2CC1AF10B706");

            entity.ToTable("MenuRoleMaster", "UM");

            entity.HasIndex(e => new { e.RoleId, e.MenuId }, "UQ_MenuRole").IsUnique();

            entity.Property(e => e.MenuRoleId).HasColumnName("MenuRoleID");
            entity.Property(e => e.CanAdd).HasDefaultValue(false);
            entity.Property(e => e.CanApprove).HasDefaultValue(false);
            entity.Property(e => e.CanDelete).HasDefaultValue(false);
            entity.Property(e => e.CanEdit).HasDefaultValue(false);
            entity.Property(e => e.CanView).HasDefaultValue(false);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuRoleMasters)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuRoleMaster_Menu");

            entity.HasOne(d => d.Role).WithMany(p => p.MenuRoleMasters)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuRoleMaster_Role");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PK__Region__ACD84443247AD8F3");

            entity.ToTable("Region", "UM");

            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.RegionName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Regions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Region__CompanyI__5BE2A6F2");
        });

        modelBuilder.Entity<RoleMaster>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__RoleMast__8AFACE3A67ACBC57");

            entity.ToTable("RoleMaster", "UM");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.RoleDescription).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC45456CCE");

            entity.ToTable("Users", "UM");

            entity.HasIndex(e => e.EmployeeCode, "UQ__Users__1F6425485EF5F0DF").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105344FC457C0").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RefreshTokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.RoleId).HasDefaultValueSql("('Employee')");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__CompanyID__571DF1D5");

            entity.HasOne(d => d.Region).WithMany(p => p.Users)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RegionID__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
