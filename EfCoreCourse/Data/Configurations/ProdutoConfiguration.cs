using EfCoreCourse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreCourse.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        private const string TABLE_NAME = "Produtos";
        private const int CODIGO_BARRAS_MAX_SIZE = 14;
        private const int DESCRICAO_MAX_SIZE = 60;

        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable(TABLE_NAME);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CodigoBarras).HasColumnType($"{SqlConstants.VARCHAR}({CODIGO_BARRAS_MAX_SIZE})").IsRequired();
            builder.Property(p => p.Descricao).HasColumnType($"{SqlConstants.VARCHAR}({DESCRICAO_MAX_SIZE})");
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.TipoProduto).HasConversion<string>();
        }
    }
}