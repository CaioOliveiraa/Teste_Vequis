using LeitorNotasFiscais.Data;

class Program
{
    static void Main(string[] args)
    {
        using var context = new AppDbContext();

        // Garante que o banco existe e aplica o seed inicial (clientes e produtos)
        DbInitializer.Seed(context);

        Console.WriteLine("Banco criado e dados iniciais inseridos com sucesso.");
    }
}
