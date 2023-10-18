using System;
using Gestion_Parc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Gestion_Parc.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace Gestion_Parc.DataDbContext
{
	public class AppDbContxt : IdentityDbContext<ApplicationUser>
	{
		public AppDbContxt(DbContextOptions<AppDbContxt> options) :base(options)
		{
		}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Computer>().HasKey(x => x.MaterialId);
            builder.Entity<Computer>().Property(x => x.MaterialId).HasDefaultValueSql("(newid())");

            builder.Entity<Printer>().HasKey(x => x.MaterialId);
            builder.Entity<Printer>().Property(x => x.MaterialId).HasDefaultValueSql("(newid())");

            builder.Entity<Other>().HasKey(x => x.MaterialId);
            builder.Entity<Other>().Property(x => x.MaterialId).HasDefaultValueSql("(newid())");

            builder.Entity<LogComputer>().HasKey(x => x.Id);
            builder.Entity<LogComputer>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<LogPrinter>().HasKey(x => x.Id);
            builder.Entity<LogPrinter>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<LogOther>().HasKey(x => x.Id);
            builder.Entity<LogOther>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<Server>().HasKey(x => x.MaterialId);
            builder.Entity<Server>().Property(x => x.MaterialId).HasDefaultValueSql("(newid())");

            builder.Entity<LogServer>().HasKey(x => x.Id);
            builder.Entity<LogServer>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<BreakDown>().HasKey(x => x.Id);
            builder.Entity<BreakDown>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            builder.Entity<Maintenance>().HasKey(x => x.Id);
            builder.Entity<Maintenance>().Property(x => x.Id).HasDefaultValueSql("(newid())");


            /*builder.Entity<ListViewModel>(entity =>
             {
                 entity.HasNoKey();
             });
             builder.Entity<NewRegisterViewModel>(entity =>
             {
                 entity.HasNoKey();
             });

             builder.Entity<RegisterViewModel>(entity =>
             {
                 entity.HasNoKey();
             });


             builder.Entity<EditViewModel>(entity =>
             {
                 entity.HasNoKey();
             });
            
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProviderKey });
            });
            */
            builder.Entity<ViewUser>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ViewUsers");
            });

        }

        public DbSet<RegisterViewModel>? RegisterViewModel { get; set; }
        public DbSet<ViewUser> ViewUsers { get; set; }
        public DbSet<Other> Materials { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<LogOther> LogMaterials { get; set; }
        public DbSet<LogPrinter> LogPrinters { get; set; }
        public DbSet<LogServer> LogServers { get; set; }
        public DbSet<LogComputer> LogComputers { get; set; }
        public DbSet<LogBrand> LogBrands { get; set; }
        public DbSet<BreakDown> BreakDowns { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
    }
}

