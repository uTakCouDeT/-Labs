using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab12New;

public partial class Industrial : DbContext
{
    public Industrial()
    {
    }

    public Industrial(DbContextOptions<Industrial> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companys { get; set; }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<Recipient> Recipients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Industrial;User ID=sa;Password=Kyalayum;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Country).IsFixedLength();
            entity.Property(e => e.Name).IsFixedLength();
            entity.Property(e => e.WayOfDelevery).IsFixedLength();
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).IsFixedLength();

            entity.HasOne(d => d.IdCompanyNavigation).WithMany(p => p.Goods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Goods_Companys");

            entity.HasOne(d => d.IdRecipientNavigation).WithMany(p => p.Goods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Goods_Recipients");
        });

        modelBuilder.Entity<Recipient>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).IsFixedLength();
            entity.Property(e => e.Country).IsFixedLength();
            entity.Property(e => e.FullName).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
