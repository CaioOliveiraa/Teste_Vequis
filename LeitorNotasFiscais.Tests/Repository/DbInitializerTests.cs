using LeitorNotasFiscais.Repository;
using LeitorNotasFiscais.Tests.TestHelpers;
using Xunit;
using System.Linq;

namespace LeitorNotasFiscais.Tests.Repository;

public class DbInitializerTests
{
    [Fact]
    public void Deve_inserir_tres_clientes_e_tres_produtos_no_seed()
    {
        // Arrange
        var context = DbContextHelper.CreateInMemoryDbContext("SeedTest");

        // Act
        DbInitializer.Seed(context);

        // Assert
        Assert.Equal(3, context.Clientes.Count());
        Assert.Equal(3, context.Produtos.Count());
    }
}
