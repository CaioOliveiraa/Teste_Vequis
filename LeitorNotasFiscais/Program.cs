using LeitorNotasFiscais.Controllers;
using LeitorNotasFiscais.Repository;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=notas.db")
            .Options;

        var context = new AppDbContext(options);
        var controller = new AppController(context);

        controller.Iniciar();
    }
}
