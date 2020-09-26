using EfCoreCourse.Data.Extensions;
using EfCoreCourse.Domain;
using Microsoft.EntityFrameworkCore;

namespace EfCoreCourse.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        private const string ConnectionString = "Host=localhost;Port:5432;Pooling=true;Database=ef_core_course;User ID=admin;Password=admin";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.CreateClienteModel();
            modelBuilder.CreateProdutoModel();
            modelBuilder.CreatePedidoModel();
            modelBuilder.CreatePedidoItemModel();
        }
    }
}