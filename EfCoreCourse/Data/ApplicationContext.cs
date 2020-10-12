using System;
using System.Linq;
using EfCoreCourse.Data.Configurations;
using EfCoreCourse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfCoreCourse.Data
{
    public class ApplicationContext : DbContext
    {
        private const int MAX_RETRY_COUNT = 2;
        private const int RETRY_DELAY_IN_SECONDS = 5;
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        private const string ConnectionString = "Host=localhost;Port=5432;Pooling=true;Database=ef_core_course;User ID=admin;Password=admin";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseNpgsql(ConnectionString, p => p.EnableRetryOnFailure(
                    maxRetryCount: MAX_RETRY_COUNT,
                    maxRetryDelay: TimeSpan.FromSeconds(RETRY_DELAY_IN_SECONDS),
                    errorCodesToAdd: null).MigrationsHistoryTable("curso_ef_core"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder) {
            foreach(var entity in modelBuilder.Model.GetEntityTypes()) {
                var properties = entity.GetProperties().Where((p) => p.ClrType == typeof(string));

                foreach(var property in properties) {
                    if(string.IsNullOrEmpty(property.GetColumnType())
                        && !property.GetMaxLength().HasValue)
                    {
                        // property.SetMaxLength(100);
                        property.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}