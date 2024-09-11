using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfVSAbstVCompDemo.Abstracts;
using InterfVSAbstVCompDemo.Interfaces;

namespace InterfVSAbstVCompDemo.Classes
{
    // Spezifische Implementierung für Cat
    public class Cat : Animal
    {
        public Cat(string name, DateTime birthDate, IMovementBehavior movementBehavior)
            : base(name, "Cat", birthDate)
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
