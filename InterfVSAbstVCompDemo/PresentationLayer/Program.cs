namespace InterfVSAbstVCompDemo.PresentationLayer;

public class Program
{
    public static void Main(string[] args)
    {
        //// Katze mit Laufverhalten
        //Animal cat = new Cat("Whiskers", new DateTime(2019, 6, 10), new RunBehavior(), ElementType.EARTH);
        //cat.Display();
        //cat.MakeSound();
        //cat.DisplayType();

        //Console.WriteLine();

        //// Hund mit Schwimmverhalten
        //Animal dog = new Dog("Buddy", new DateTime(2017, 3, 15), new SwimBehavior(), ElementType.WATER);
        //dog.Display();
        //dog.MakeSound();
        //dog.DisplayType();

        // Startet den TCP-Server, um auf Verbindungen zu lauschen.
        var server = new Server();
        server.Start();
    }
}