using EfCoreCourse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreCourse.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        private const string TABLE_NAME = "Clientes";
        private const int NOME_MAX_SIZE = 80;
        private const int TELEFONE_SIZE = 11;
        private const int CEP_SIZE = 8;
        private const int ESTADO_SIZE = 2;
        private const int CIDADE_MAX_SIZE = 60;
        private const string TELEFONE_INDEX_NAME = "idx_cliente_telefone";

        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable(TABLE_NAME);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasColumnType($"{SqlConstants.VARCHAR}({NOME_MAX_SIZE})").IsRequired();
            builder.Property(p => p.Telefone).HasColumnType($"{SqlConstants.CHAR}({TELEFONE_SIZE})");
            builder.Property(p => p.CEP).HasColumnType($"{SqlConstants.CHAR}({CEP_SIZE})").IsRequired();
            builder.Property(p => p.Estado).HasColumnType($"{SqlConstants.CHAR}({ESTADO_SIZE})").IsRequired();
            builder.Property(p => p.Cidade).HasMaxLength(CIDADE_MAX_SIZE).IsRequired();

            builder.HasIndex(i => i.Telefone).HasName(TELEFONE_INDEX_NAME);
        }
    }
}