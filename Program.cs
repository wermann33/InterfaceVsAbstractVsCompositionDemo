namespace InterfVSAbstVCompDemo;

public class Program
{
    public static void Main(string[] args)
    {
        // Katze mit Laufverhalten
        Animal cat = new Cat("Whiskers", new DateTime(2019, 6, 10), new RunBehavior());
        cat.Display();
        cat.MakeSound();

        Console.WriteLine();

        // Hund mit Schwimmverhalten
        Animal dog = new Dog("Buddy", new DateTime(2017, 3, 15), new SwimBehavior());
        dog.Display();
        dog.MakeSound();
    }
}