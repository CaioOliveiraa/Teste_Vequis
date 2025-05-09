using LeitorNotasFiscais.Repository;
using LeitorNotasFiscais.Services;

namespace LeitorNotasFiscais.Controllers;

public class AppController
{
    private readonly AppDbContext _context;

    public AppController()
    {
        _context = new AppDbContext();
    }

    public void Iniciar()
    {
        DbInitializer.Seed(_context);

        var xmlService = new XmlService(_context);
        xmlService.ProcessarXmls("xml");

        var queryService = new QueryService(_context);

        queryService.ExibirClientesProdutosTotais();
        queryService.ContarProdutosTotais();
        queryService.AplicarImpostos();
        queryService.ExibirValorTotalPorProduto();
    }
}
