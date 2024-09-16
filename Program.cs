using InterfVSAbstVCompDemo.Abstracts;
using InterfVSAbstVCompDemo.Classes;

namespace InterfVSAbstVCompDemo;

public class Program
{
    public static void Main(string[] args)
    {
        // Katze mit Laufverhalten
        Animal cat = new Cat("Whiskers", new DateTime(2019, 6, 10), new RunBehavior(), ElementType.EARTH);
        cat.Display();
        cat.MakeSound();
        cat.DisplayType();

        Console.WriteLine();

        // Hund mit Schwimmverhalten
        Animal dog = new Dog("Buddy", new DateTime(2017, 3, 15), new SwimBehavior(), ElementType.WATER);
        dog.Display();
        dog.MakeSound();
        dog.DisplayType();
    }
}