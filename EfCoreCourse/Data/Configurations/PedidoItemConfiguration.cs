using EfCoreCourse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreCourse.Data.Configurations
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        private const string TABLE_NAME = "PedidoItens";
        private const int QUANTIDADE_DEFAULT_VALUE = 1;

        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable(TABLE_NAME);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Quantidade).HasDefaultValue(QUANTIDADE_DEFAULT_VALUE).IsRequired();
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.Desconto).IsRequired();
        }
    }
}