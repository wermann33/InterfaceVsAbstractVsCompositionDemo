using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;
using InterfVSAbstVCompDemo.Interfaces;

namespace InterfVSAbstVCompDemo.BusinessLayer.Models
{
    // Spezifische Implementierung für Dog
    public class Dog : Animal
    {
        public Dog(string name, DateTime birthDate, IMovementBehavior? movementBehavior, ElementType element)
            : base(name, "Dog", birthDate, element)
        {
            MovementBehavior = movementBehavior;
        }

        public override void MakeSound()
        {
            Console.WriteLine(Name + " says: Woof Woof!");
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("Age: " + CalculateAge() + " years");
        }
    }
}
