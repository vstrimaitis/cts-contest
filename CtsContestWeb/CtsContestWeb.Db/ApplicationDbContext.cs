﻿using CtsContestWeb.Db.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CtsContestWeb.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>()
                .HasIndex(p => new { p.UserId, p.PrizeId })
                .IsUnique(true);

            modelBuilder.Entity<Solution>()
                .HasIndex(p => new { p.UserId, p.TaskId })
                .IsUnique(true);
        }

        public override int SaveChanges()
        {
            SetMetadata();
            return base.SaveChanges();
        }

        private void SetMetadata()
        {
            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                var auditable = entity.Entity as IAuditable;
                if (auditable != null)
                {
                    auditable.Created = DateTime.Now;
                }
            }
        }
    }
}
