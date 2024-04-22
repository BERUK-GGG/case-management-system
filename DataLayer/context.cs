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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=RB_ DataBase; Integrated Security=True; TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }   

        public DbSet<Kund> kunder {  get; set; }
        public DbSet<Mekaniker> mekaniker { get; set; }
        public DbSet<Reservdel> reservdelar { get; set; }
        public DbSet<Besök> besök { get; set; }

        public DbSet<Jornal> jornals { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
            modelBuilder.Entity<Kund>().Property(k => k.ID).UseIdentityColumn();
            modelBuilder.Entity<Besök>().Property(k => k.BesökID).UseIdentityColumn();
            modelBuilder.Entity<Mekaniker>().Property(k => k.Anställningsnummer).UseIdentityColumn();
            modelBuilder.Entity<Jornal>().Property(k => k.JornalID).UseIdentityColumn();

        }

    }
 
}
