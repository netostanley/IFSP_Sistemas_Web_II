using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using tp2tuller.Models;

namespace tp2tuller.Data
{
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Database=tp03;User=root;Password=root;",
                    new MySqlServerVersion(new Version(8, 0, 33)));
            }
        }
    }
}
