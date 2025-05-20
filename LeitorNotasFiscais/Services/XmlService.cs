using System.Globalization;
using System.Xml.Linq;
using LeitorNotasFiscais.Models;
using LeitorNotasFiscais.Repository;

namespace LeitorNotasFiscais.Services;

public class XmlService
{
    private readonly AppDbContext _context;

    public XmlService(AppDbContext context)
    {
        _context = context;
    }

    public void ProcessarXmls(string pasta)
    {
        var arquivos = Directory.GetFiles(pasta, "*.xml");

        foreach (var caminho in arquivos)
        {
            Console.WriteLine($"\nLendo arquivo: {Path.GetFileName(caminho)}");

            var doc = XDocument.Load(caminho);
            var nota = doc.Element("NotaFiscal");

            if (nota is null)
                continue;

            var clienteXml = nota.Element("Cliente");
            var produtosXml = nota.Element("Produtos")?.Elements("Produto");

            if (clienteXml == null || produtosXml == null)
                continue;

            var nome = clienteXml.Element("Nome")?.Value ?? "";
            var cidade = clienteXml.Element("Cidade")?.Value ?? "";
            var estado = clienteXml.Element("Estado")?.Value ?? "";
            var pais = clienteXml.Element("Pais")?.Value ?? "";

            var clienteExistente = _context.Clientes.FirstOrDefault(c =>
                c.Nome == nome && c.Cidade == cidade
            );

            var cliente =
                clienteExistente
                ?? new Cliente
                {
                    Nome = nome,
                    Cidade = cidade,
                    Estado = estado,
                    Pais = pais,
                };

            if (clienteExistente == null)
                _context.Clientes.Add(cliente);

            _context.SaveChanges();

            decimal total = 0;
            int novosProdutos = 0;
            var produtosParaExibir = new List<(string Nome, decimal Valor, bool Novo)>();

            foreach (var prod in produtosXml)
            {
                var nomeProd = prod.Element("Nome")?.Value ?? "";
                var valorProd = decimal.Parse(
                    prod.Element("Valor")?.Value ?? "0",
                    CultureInfo.InvariantCulture
                );
                total += valorProd;

                bool produtoJaExiste = _context.Produtos.Any(p =>
                    p.ClienteId == cliente.Id && p.Nome == nomeProd && p.Valor == valorProd
                );

                if (!produtoJaExiste)
                {
                    var produto = new Produto
                    {
                        Nome = nomeProd,
                        Valor = valorProd,
                        ClienteId = cliente.Id,
                    };

                    _context.Produtos.Add(produto);
                    novosProdutos++;
                    produtosParaExibir.Add((nomeProd, valorProd, true));
                }
                else
                {
                    produtosParaExibir.Add((nomeProd, valorProd, false));
                }
            }

            _context.SaveChanges();

            Console.WriteLine($"Cliente: {cliente.Nome}");
            Console.WriteLine("Produtos:");
            foreach (var (nomeProd, valorProd, novo) in produtosParaExibir)
            {
                var flag = novo ? "(novo)" : "(já existente)";
                Console.WriteLine($" - {nomeProd} - R$ {valorProd:N2} {flag}");
            }

            Console.WriteLine($"Valor total da compra: R$ {total:N2}");
        }
    }
}
