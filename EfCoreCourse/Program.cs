using System;
using System.Linq;
using EfCoreCourse.Data;
using EfCoreCourse.Domain;
using EfCoreCourse.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EfCoreCourse
{
    class Program
    {
        static void Main(string[] args)
        {
            // InserirDados();
            // InserirDadosEmMassa();
            ConsultarDados();
        }

        private static void ConsultarDados()
        {
            using var dbContext = new ApplicationContext();

            // var consultaPorSintaxe = (from c in dbContext.Clientes where c.Id > 0 select c).ToList();
            // Se tivesse sido feita com AsNoTracking(), não guardaria cópia em memória, portanto o Find teria que buscar no bd
            var consultaPorMetodo = dbContext.Clientes.Where(c => c.Id > default(int))
                .OrderBy(c => c.Id)
                .ToList();
            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando cliente: {cliente.Id}");
                // Procura primeiro em memória
                // dbContext.Clientes.Find(cliente.Id);
                dbContext.Clientes.FirstOrDefault(c => c.Id == cliente.Id);
            }
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Eu mesmo",
                CEP = "34685974",
                Cidade = "Betim",
                Estado = "MG",
                Telefone = "31999999999"
            };

            using var dbContext = new ApplicationContext();
            dbContext.AddRange(produto, cliente);

            var registros = dbContext.SaveChanges();
            Console.WriteLine(registros);
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var dbContext = new ApplicationContext();
            dbContext.Produtos.Add(produto);
            dbContext.Set<Produto>().Add(produto);
            dbContext.Entry(produto).State = EntityState.Added;
            dbContext.Add(produto);

            var registros = dbContext.SaveChanges();
            Console.WriteLine(registros);
        }
    }
}
