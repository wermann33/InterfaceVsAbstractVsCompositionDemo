using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;
using InterfVSAbstVCompDemo.Interfaces;

namespace InterfVSAbstVCompDemo.BusinessLayer.Models
{
    // Spezifische Implementierung für Cat
    public class Cat : Animal
    {
        public Cat(string name, DateTime birthDate, IMovementBehavior movementBehavior, ElementType element)
            : base(name, "Cat", birthDate, element)
        {
            MovementBehavior = movementBehavior;
        }

        public override void MakeSound()
        {
            Console.WriteLine(Name + " says: Meow!");
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine("Age: " + CalculateAge() + " years");
        }
    }
}
