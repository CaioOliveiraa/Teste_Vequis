using LeitorNotasFiscais.Controllers;

class Program
{
    static void Main(string[] args)
    {
        var controller = new AppController();
        controller.Iniciar();
    }
}
