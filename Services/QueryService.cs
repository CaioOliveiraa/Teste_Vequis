using LeitorNotasFiscais.Repository;
using Microsoft.Data.Sqlite;

namespace LeitorNotasFiscais.Services;

public class QueryService
{
    private readonly AppDbContext _context;

    public QueryService(AppDbContext context)
    {
        _context = context;
    }

    public void ExibirClientesProdutosTotais()
    {
        Console.WriteLine("\n\n=== Consulta: Cliente, Produto e Valor Total ===\n");

        var resultado = _context
            .Clientes.SelectMany(
                c => c.Produtos,
                (c, p) =>
                    new
                    {
                        Cliente = c.Nome,
                        Produto = p.Nome,
                        Total = p.Valor,
                    }
            )
            .ToList();

        foreach (var item in resultado)
        {
            Console.WriteLine(
                $"Cliente: {item.Cliente} | Produto: {item.Produto} | Valor: R$ {item.Total:N2}"
            );
        }
    }

    public void ExibirValorTotalPorProduto()
    {
        Console.WriteLine("\n\n=== Consulta: Valor total por produto ===\n");

        var agrupado = _context
            .Produtos.GroupBy(p => p.Nome)
            .Select(g => new { Produto = g.Key, Total = g.Sum(p => p.Valor) })
            .ToList();

        foreach (var item in agrupado)
        {
            Console.WriteLine($"Produto: {item.Produto} | Valor total: R$ {item.Total:N2}");
        }
    }

    public void AplicarImpostos()
    {
        Console.WriteLine("\n\n=== Consulta: Aplicando impostos simulados ===\n");

        var impostos = new Dictionary<string, decimal>
        {
            { "Notebook Dell", 0.06m },
            { "Mouse Gamer", 0.0250m },
            { "Teclado Mecânico", 0.05m },
            { "Headset Gamer", 0.04m },
            { "Monitor 24 Polegadas", 0.0275m },
            { "Monitor 27 Polegadas", 0.0275m },
            { "Mousepad XL", 0.02m },
            { "Webcam HD", 0.015m },
            { "Suporte de Notebook", 0.0250m },
            { "Hub USB", 0.01m },
        };

        var produtos = _context.Produtos.ToList();

        foreach (var produto in produtos)
        {
            var aliquota = impostos.TryGetValue(produto.Nome, out var taxa) ? taxa : 0;
            var valorComImposto = produto.Valor * (1 + aliquota);

            Console.WriteLine(
                $"Produto: {produto.Nome} | Valor original: R$ {produto.Valor:N2} | Com imposto: R$ {valorComImposto:N2}"
            );
        }
    }

    public void ContarProdutosTotais()
    {
        Console.WriteLine("\n\n=== Consulta: Quantidade total de produtos cadastrados ===\n");

        var total = _context.Produtos.Count();

        Console.WriteLine($"Total de produtos registrados: {total}");
    }
}
