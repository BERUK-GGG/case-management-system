using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using RB_Ärendesystem.Entities;

namespace RB_Ärendesystem.Datalayer
{
    public class RB_context : DbContext
    {
        public RB_context() : base()
        {
        }

        public RB_context(DbContextOptions<RB_context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=rb-sqlserver,1433;Database=RBCaseManagement;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;");
            }
            base.OnConfiguring(optionsBuilder);
        }   

        public DbSet<Kund> kunder {  get; set; }
        public DbSet<Mekaniker> mekaniker { get; set; }
        public DbSet<Reservdel> reservdelar { get; set; }
        public DbSet<Besök> besök { get; set; }

        public DbSet<Journal> jornals { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

            modelBuilder.Entity<Kund>().Property(k => k.ID).UseIdentityColumn();
            modelBuilder.Entity<Besök>().Property(k => k.ID).UseIdentityColumn();
            modelBuilder.Entity<Mekaniker>().Property(k => k.Id).UseIdentityColumn();
            modelBuilder.Entity<Journal>().Property(k => k.ID).UseIdentityColumn();
        }

    }
 
}
