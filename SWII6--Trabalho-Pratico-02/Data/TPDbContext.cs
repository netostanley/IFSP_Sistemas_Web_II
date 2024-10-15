using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using tp2tuller.Models;

namespace tp2tuller.Data // Altere para o namespace do seu projeto
{
    public class TPDbContext : DbContext
    {
        public TPDbContext(DbContextOptions<TPDbContext> options) : base(options) { }

        public DbSet<BL> BLs { get; set; }
        public DbSet<Conteiner> Containers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento 1:N entre BL e Container
            modelBuilder.Entity<BL>()
                .HasMany(b => b.Containers)
                .WithOne(c => c.BL)
                .HasForeignKey(c => c.ID_BL);
        }
    }
}
