﻿using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                //Primary Key
                entity.HasKey(p => p.ProductId);

                entity
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired(true) //by default is true.
                .IsUnicode(true);

                entity
                .Property(p => p.Description)
                .HasMaxLength(250)
                .IsRequired(false)
                .IsUnicode(true)
                .HasDefaultValue("No description");

                entity
                .Property(p => p.Quantity)
                .IsRequired(true);

                entity
                .Property(p => p.Price)
                .IsRequired(true);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerId);

                entity
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(true);

                entity
                .Property(c => c.Email)
                .HasMaxLength(80)
                .IsRequired(true)
                .IsUnicode(false);

                entity
                .Property(c => c.CreditCardNumber)
                .IsRequired(true)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(s => s.StoreId);

                entity
                .Property(s => s.Name)
                .HasMaxLength(80)
                .IsRequired(true)
                .IsUnicode(true);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(s => s.SaleId);

                entity
                .Property(s => s.Date)
                .IsRequired(true)
                .HasColumnType("DATETIME2")
                .HasDefaultValueSql("GETDATE()");

                //Table Relations,One-To-Many HasForeignKey has to finish as same as HasOne property 1/2/1
                //Many-To-One is 1/2/2
                entity
                .HasOne(s => s.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductId);

                entity
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

                entity
                .HasOne(s => s.Store)
                .WithMany(st => st.Sales)
                .HasForeignKey(s => s.StoreId);
            });
        }
    }
}
