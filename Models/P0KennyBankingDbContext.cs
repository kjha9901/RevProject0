using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project_0.Models;

public partial class P0KennyBankingDbContext : DbContext
{
    public P0KennyBankingDbContext()
    {
    }

    public P0KennyBankingDbContext(DbContextOptions<P0KennyBankingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminUser> AdminUsers { get; set; }

    public virtual DbSet<CustomerTransaction> CustomerTransactions { get; set; }

    public virtual DbSet<CustomerUser> CustomerUsers { get; set; }

    public virtual DbSet<RequestToAdmin> RequestToAdmins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=GENTLEMANNPC\\KENNYSERVER;Database=P0_Kenny_bankingDB;integrated security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.AdminAccNo).HasName("PK__AdminUse__B58B32E5367A2568");

            entity.ToTable("AdminUser");

            entity.HasIndex(e => e.Username, "UQ__AdminUse__F3DBC5729A8059D1").IsUnique();

            entity.Property(e => e.AdminAccNo).HasColumnName("adminAccNo");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<CustomerTransaction>(entity =>
        {
            entity.HasKey(e => e.TrNo).HasName("pk_trNo");

            entity.Property(e => e.TrNo).HasColumnName("trNo");
            entity.Property(e => e.AccNo).HasColumnName("accNo");
            entity.Property(e => e.TrAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("trAmount");
            entity.Property(e => e.TrType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("trType");
            entity.Property(e => e.TransferAcc).HasColumnName("transferAcc");

            entity.HasOne(d => d.AccNoNavigation).WithMany(p => p.CustomerTransactions)
                .HasForeignKey(d => d.AccNo)
                .HasConstraintName("fk_accNo");
        });

        modelBuilder.Entity<CustomerUser>(entity =>
        {
            entity.HasKey(e => e.AccNo).HasName("pk_accNo");

            entity.ToTable("CustomerUser");

            entity.HasIndex(e => e.AccUsername, "UQ__Customer__30DCFEAE40C53889").IsUnique();

            entity.Property(e => e.AccNo).HasColumnName("accNo");
            entity.Property(e => e.AccBalance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("accBalance");
            entity.Property(e => e.AccIsActive).HasColumnName("accIsActive");
            entity.Property(e => e.AccName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("accName");
            entity.Property(e => e.AccPassword)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("accPassword");
            entity.Property(e => e.AccType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("accType");
            entity.Property(e => e.AccUsername)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("accUsername");
        });

        modelBuilder.Entity<RequestToAdmin>(entity =>
        {
            entity.HasKey(e => e.RqNo).HasName("pk_rqNo");

            entity.ToTable("RequestToAdmin");

            entity.Property(e => e.RqNo).HasColumnName("rqNo");
            entity.Property(e => e.AccNo).HasColumnName("accNo");
            entity.Property(e => e.RqComplete)
                .HasDefaultValue(false)
                .HasColumnName("rqComplete");
            entity.Property(e => e.RqPassword)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("rqPassword");
            entity.Property(e => e.RqType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("rqType");

            entity.HasOne(d => d.AccNoNavigation).WithMany(p => p.RequestToAdmins)
                .HasForeignKey(d => d.AccNo)
                .HasConstraintName("fk_rqAccNo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
