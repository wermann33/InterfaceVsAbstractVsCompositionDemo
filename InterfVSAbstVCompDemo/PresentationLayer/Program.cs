namespace InterfVSAbstVCompDemo.PresentationLayer;

public class Program
{
    public static async Task Main(string[] args)
    {
        var server = new Server();
        await server.StartAsync();
    }
}