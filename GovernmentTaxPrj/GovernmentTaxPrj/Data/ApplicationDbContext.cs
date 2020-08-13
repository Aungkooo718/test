using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GovernmentTaxPrj.Models;

namespace GovernmentTaxPrj.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IncomeType>().ToTable("IncomeType");
            modelBuilder.Entity<Region>().ToTable("Region");
            modelBuilder.Entity<TaxAccount>().ToTable("TaxAccount");
            modelBuilder.Entity<TaxPayer>().ToTable("TaxPayer");
            modelBuilder.Entity<TaxTransaction>().ToTable("TaxTransaction");
            modelBuilder.Entity<Township>().ToTable("Township");
            modelBuilder.Entity<Township>().HasOne(t => t.Region).WithMany(r => r.Townships).HasForeignKey(t => t.RegionId).OnDelete(DeleteBehavior.Restrict);
        }
        public virtual DbSet<IncomeType> IncomeTypes { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<TaxAccount> TaxAccounts { get; set; }
        public virtual DbSet<TaxPayer> TaxPayers { get; set; }
        public virtual DbSet<TaxTransaction> TaxTransactions { get; set; }
        public virtual DbSet<Township> Townships { get; set; }
    }
}
