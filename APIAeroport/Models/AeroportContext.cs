using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAeroport.Models
{
    public class AeroportContext : DbContext
    {
        public AeroportContext(DbContextOptions options) : base(options)
        {

        }

        public AeroportContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>()
                .HasKey(s => new { s.VolCeduleId, s.NumTel });

            modelBuilder.Entity<Notification>()
                .Property(n => n.VolCeduleId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Notification>()
                .Property(n => n.NumTel)
                .HasMaxLength(20);

            modelBuilder.Entity<VolGenerique>()
                .Property(v => v.VolGeneriqueId)
                .HasMaxLength(10);

            modelBuilder.Entity<Aeroport>()
                .Property(a => a.AeroportId).HasMaxLength(10);

            modelBuilder.Entity<Compagnie>()
                .Property(c => c.CompagnieId).HasMaxLength(10);
        }

        public DbSet<Aeroport> Aeroports { get; set; }
        public DbSet<Compagnie> Compagnies { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<VolGenerique> VolGeneriques { get; set; }
        public DbSet<VolCedule> VolCedules { get; set; }
    }
}
