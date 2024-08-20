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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=GENTLEMANNPC\\KENNYSERVER;Database=P0_Kenny_bankingDB;integrated security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("pk_username_admin");

            entity.ToTable("AdminUser");

            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
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

            entity.Property(e => e.AccNo)
                .ValueGeneratedNever()
                .HasColumnName("accNo");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
