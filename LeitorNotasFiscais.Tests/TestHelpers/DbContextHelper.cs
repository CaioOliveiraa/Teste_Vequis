using LeitorNotasFiscais.Repository;
using Microsoft.EntityFrameworkCore;

namespace LeitorNotasFiscais.Tests.TestHelpers;

public static class DbContextHelper
{
    public static AppDbContext CreateInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new AppDbContext(options);
    }
}
