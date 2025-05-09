using LeitorNotasFiscais.Models;

namespace LeitorNotasFiscais.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Clientes.Any())
        {
            context.Clientes.Add(
                new Cliente
                {
                    Id = 99,
                    Nome = "Dummy",
                    Cidade = "-",
                    Estado = "-",
                    Pais = "-",
                }
            );
            context.SaveChanges();

            context.Clientes.RemoveRange(context.Clientes.Where(c => c.Id == 99));
            context.SaveChanges();

            context.Clientes.AddRange(
                new Cliente
                {
                    Nome = "Cliente 1",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Pais = "Brasil",
                },
                new Cliente
                {
                    Nome = "Cliente 2",
                    Cidade = "Curitiba",
                    Estado = "PR",
                    Pais = "Brasil",
                },
                new Cliente
                {
                    Nome = "Cliente 3",
                    Cidade = "Rio de Janeiro",
                    Estado = "RJ",
                    Pais = "Brasil",
                }
            );
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("Clientes já existentes. Seed ignorado.");
        }

        if (!context.Produtos.Any())
        {
            var cliente1Id = context.Clientes.First().Id;

            context.Produtos.Add(
                new Produto
                {
                    Id = 230,
                    Nome = "Dummy",
                    Valor = 0,
                    ClienteId = cliente1Id,
                }
            );
            context.SaveChanges();
            context.Produtos.RemoveRange(context.Produtos.Where(p => p.Id == 230));
            context.SaveChanges();

            context.Produtos.AddRange(
                new Produto
                {
                    Nome = "Produto 1",
                    Valor = 100m,
                    ClienteId = cliente1Id,
                },
                new Produto
                {
                    Nome = "Produto 2",
                    Valor = 200m,
                    ClienteId = cliente1Id,
                },
                new Produto
                {
                    Nome = "Produto 3",
                    Valor = 300m,
                    ClienteId = cliente1Id,
                }
            );
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("Produtos já existentes. Seed ignorado.");
        }
    }
}
