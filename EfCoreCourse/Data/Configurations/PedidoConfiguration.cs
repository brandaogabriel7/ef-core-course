using EfCoreCourse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreCourse.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        private const string TABLE_NAME = "Pedidos";
        private const int OBSERVACAO_MAX_SIZE = 512;

        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable(TABLE_NAME);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.IniciadoEm).HasDefaultValueSql($"{SqlConstants.GETDATE}()").ValueGeneratedOnAdd();
            builder.Property(p => p.Status).HasConversion<string>();
            builder.Property(p => p.TipoFrete).HasConversion<int>();
            builder.Property(p => p.Observacao).HasColumnType($"{SqlConstants.VARCHAR}({OBSERVACAO_MAX_SIZE})");

            builder.HasMany(p => p.Items)
                .WithOne(p => p.Pedido)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}