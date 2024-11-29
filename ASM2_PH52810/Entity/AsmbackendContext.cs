using System;
using System.Collections.Generic;
using ASM2_PH52810.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM2_PH52810.Entity;

public partial class AsmbackendContext : DbContext
{
    public AsmbackendContext()
    {
    }

    public AsmbackendContext(DbContextOptions<AsmbackendContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Weapon> Weapons { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__727E838B10BCF8E8");

            entity.Property(e => e.ItemName).HasMaxLength(100);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Players__4A4E74C8AF6E9F02");

            entity.Property(e => e.Mode).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PlayerName).HasMaxLength(100);
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__6B0A6BBEAB3EA45E");

            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

            entity.HasOne(d => d.Item).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Purchases__ItemI__440B1D61");

            entity.HasOne(d => d.Player).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__Purchases__Playe__4316F928");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasName("PK__Resource__4ED1816F3A029302");

            entity.Property(e => e.ResourceName).HasMaxLength(100);
            entity.Property(e => e.ResourceType).HasMaxLength(50);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B823ED5A1");

            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.HasOne(d => d.Item).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Transacti__ItemI__403A8C7D");

            entity.HasOne(d => d.Player).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__Transacti__Playe__3F466844");
        });

        modelBuilder.Entity<Weapon>(entity =>
        {
            entity.HasKey(e => e.WeaponId).HasName("PK__Weapons__541D0AF1AFFC9CDE");

            entity.Property(e => e.WeaponName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
