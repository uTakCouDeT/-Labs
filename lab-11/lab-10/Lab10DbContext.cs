using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace lab_10;

public partial class Lab10DbContext : DbContext
{
    public Lab10DbContext()
    {
    }

    public Lab10DbContext(DbContextOptions<Lab10DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Ticker> Tickers { get; set; }

    public virtual DbSet<TodaysCondition> TodaysConditions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=../../../../lab-10/lab-10-db.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Price>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Price1).HasColumnName("price");
            entity.Property(e => e.TickerId).HasColumnName("tickerid");

            entity.HasOne(d => d.Ticker).WithMany(p => p.Prices).HasForeignKey(d => d.TickerId);
        });

        modelBuilder.Entity<Ticker>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ticker1).HasColumnName("ticker");
        });

        modelBuilder.Entity<TodaysCondition>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.TickerId).HasColumnName("tickerid");

            entity.HasOne(d => d.Ticker).WithMany(p => p.TodaysConditions).HasForeignKey(d => d.TickerId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}