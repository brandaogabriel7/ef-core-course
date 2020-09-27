using EfCoreCourse.Domain;
using Microsoft.EntityFrameworkCore;

namespace EfCoreCourse.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void CreateProdutoModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>((p) =>
            {
                p.ToTable("Produtos");
                p.HasKey(p => p.Id);
                p.Property(p => p.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
                p.Property(p => p.Descricao).HasColumnType("VARCHAR(60)");
                p.Property(p => p.Valor).IsRequired();
                p.Property(p => p.TipoProduto).HasConversion<string>();
            });
        }

        public static void CreatePedidoModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>((p) =>
            {
                p.ToTable("Pedidos");
                p.HasKey(p => p.Id);
                p.Property(p => p.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
                p.Property(p => p.Status).HasConversion<string>();
                p.Property(p => p.TipoFrete).HasConversion<int>();
                p.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

                p.HasMany(p => p.Items)
                    .WithOne(p => p.Pedido)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }

        public static void CreatePedidoItemModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoItem>((p) =>
            {
                p.ToTable("PedidoItens");
                p.HasKey(p => p.Id);
                p.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
                p.Property(p => p.Valor).IsRequired();
                p.Property(p => p.Desconto).IsRequired();
            });
        }
    }
}