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

    public virtual DbSet<AdminFieldSetting> AdminFieldSettings { get; set; }

    public virtual DbSet<AssetStatus> AssetStatuses { get; set; }

    public virtual DbSet<BankDetail> BankDetails { get; set; }

    public virtual DbSet<BloodGroup> BloodGroups { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EmployeeEmergencyContact> EmployeeEmergencyContacts { get; set; }

    public virtual DbSet<EmployeeFamilyDetail> EmployeeFamilyDetails { get; set; }

    public virtual DbSet<EmployeeHistory> EmployeeHistories { get; set; }

    public virtual DbSet<EmployeeSale> EmployeeSales { get; set; }

    public virtual DbSet<FieldPermission> FieldPermissions { get; set; }

    public virtual DbSet<JobHistory> JobHistories { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<MandatoryField> MandatoryFields { get; set; }

    public virtual DbSet<MenuMaster> MenuMasters { get; set; }

    public virtual DbSet<MenuRoleMaster> MenuRoleMasters { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RoleMaster> RoleMasters { get; set; }

    public virtual DbSet<Smtpsetting> Smtpsettings { get; set; }

    public virtual DbSet<Tsproject> Tsprojects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserForm> UserForms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BOOK-S7RB36TA7N\\PRAVEEN;Database=Timesheet;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminFieldSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdminFie__3214EC071FDA629F");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<AssetStatus>(entity =>
        {
            entity.HasKey(e => e.AssetStatusId).HasName("PK__AssetSta__E63EE4F66B16D7A6");

            entity.ToTable("AssetStatus");

            entity.HasIndex(e => e.AssetStatusName, "UQ__AssetSta__CBB291E01F4B8D47").IsUnique();

            entity.Property(e => e.AssetStatusId).HasColumnName("AssetStatusID");
            entity.Property(e => e.AssetStatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<BankDetail>(entity =>
        {
            entity.HasKey(e => e.BankDetailId).HasName("PK__BankDeta__1741077C2F396B4E");

            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.BankName).HasMaxLength(200);
            entity.Property(e => e.BranchAddress).HasMaxLength(300);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<BloodGroup>(entity =>
        {
            entity.ToTable("BloodGroup");

            entity.HasIndex(e => e.BloodGroupName, "UQ_BloodGroup_BloodGroupName").IsUnique();

            entity.Property(e => e.BloodgroupId).HasColumnName("BloodgroupID");
            entity.Property(e => e.BloodGroupName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971C4C7134FDBB");

            entity.ToTable("Company");

            entity.HasIndex(e => e.CompanyCode, "UQ__Company__11A0134BDC6968C8").IsUnique();

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

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD3C7806BD");

            entity.ToTable("Department");

            entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC34889070FB").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<EmployeeEmergencyContact>(entity =>
        {
            entity.HasKey(e => e.EmergencyContactId).HasName("PK__Employee__E8A61DAEB0398CFF");

            entity.ToTable("EmployeeEmergencyContact");

            entity.Property(e => e.EmergencyContactId).HasColumnName("EmergencyContactID");
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.AlternatePhone).HasMaxLength(20);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.ContactName).HasMaxLength(150);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.Relationship).HasMaxLength(100);
        });

        modelBuilder.Entity<EmployeeFamilyDetail>(entity =>
        {
            entity.HasKey(e => e.FamilyId).HasName("PK__Employee__41D82F6BF42CDA02");

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.Relationship).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Employee__4D7B4ABD436DF64D");

            entity.ToTable("EmployeeHistory");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.Employer).HasMaxLength(200);
            entity.Property(e => e.JobTitle).HasMaxLength(150);
            entity.Property(e => e.LastCtc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("LastCTC");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ReasonForLeaving).HasMaxLength(300);
            entity.Property(e => e.Website).HasMaxLength(200);
        });

        modelBuilder.Entity<EmployeeSale>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SalesAmount).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<FieldPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FieldPer__3213E83FBB098CA0");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressRequired).HasColumnName("addressRequired");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FirstNameRequired).HasColumnName("firstNameRequired");
            entity.Property(e => e.LastNameRequired).HasColumnName("lastNameRequired");
            entity.Property(e => e.MobileRequired).HasColumnName("mobileRequired");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<JobHistory>(entity =>
        {
            entity.HasKey(e => e.JobHistoryId).HasName("PK__JobHisto__A809D9F415E89DAB");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocumentPath).HasMaxLength(500);
            entity.Property(e => e.EmpCode)
                .HasMaxLength(100)
                .HasDefaultValue("");
            entity.Property(e => e.Employer).HasMaxLength(200);
            entity.Property(e => e.JobTitle).HasMaxLength(200);
            entity.Property(e => e.LastCtc)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("LastCTC");
            entity.Property(e => e.Reason)
                .HasMaxLength(500)
                .HasDefaultValue("");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .HasDefaultValue("");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Logs__3214EC074496EDA8");

            entity.Property(e => e.Level).HasMaxLength(128);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<MandatoryField>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mandator__3214EC079F4E75A9");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FieldName).HasMaxLength(100);
            entity.Property(e => e.ModuleName).HasMaxLength(100);
        });

        modelBuilder.Entity<MenuMaster>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__MenuMast__C99ED2500BCCEA29");

            entity.ToTable("MenuMaster");

            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.MenuName).HasMaxLength(100);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ParentMenuId).HasColumnName("ParentMenuID");
            entity.Property(e => e.Url).HasMaxLength(255);
        });

        modelBuilder.Entity<MenuRoleMaster>(entity =>
        {
            entity.HasKey(e => e.MenuRoleId).HasName("PK__MenuRole__880F2CC1078F2F84");

            entity.ToTable("MenuRoleMaster");

            entity.HasIndex(e => new { e.RoleId, e.MenuId }, "UQ__MenuRole__9663231E1074FDFA").IsUnique();

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
            entity.HasKey(e => e.RegionId).HasName("PK__Region__ACD84443B945B273");

            entity.ToTable("Region");

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
                .HasConstraintName("FK_Region_Company");
        });

        modelBuilder.Entity<RoleMaster>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__RoleMast__8AFACE3AD0E6DB4D");

            entity.ToTable("RoleMaster");

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

        modelBuilder.Entity<Smtpsetting>(entity =>
        {
            entity.HasKey(e => e.Smtpid).HasName("PK__SMTPSett__5C3339B8021502F0");

            entity.ToTable("SMTPSettings");

            entity.Property(e => e.Smtpid).HasColumnName("SMTPId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FromEmail).HasMaxLength(255);
            entity.Property(e => e.FromName).HasMaxLength(255);
            entity.Property(e => e.Host).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Security).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<Tsproject>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__TSProjec__761ABED029EF8B16");

            entity.ToTable("TSProjects");

            entity.HasIndex(e => e.ProjectCode, "UQ__TSProjec__2F3A49480B0F44E1").IsUnique();

            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.HourlyRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.ProjectCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProjectManagerId).HasColumnName("ProjectManagerID");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACCCD7B497");

            entity.HasIndex(e => e.EmployeeCode, "UQ__Users__1F64254893B4272D").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105343C625490").IsUnique();

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
                .HasConstraintName("FK_Users_Company");

            entity.HasOne(d => d.Region).WithMany(p => p.Users)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Region");
        });

        modelBuilder.Entity<UserForm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userForm__3213E83F0CA95FCA");

            entity.ToTable("userForm");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mobile");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
