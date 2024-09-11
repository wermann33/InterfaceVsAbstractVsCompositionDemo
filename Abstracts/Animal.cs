using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using InterfVSAbstVCompDemo.Interfaces;

namespace InterfVSAbstVCompDemo.Abstracts
{
    // Abstrakte Klasse Animal
    public abstract class Animal : ISoundableAndAgeCalculable
    {
        public string Name { get; protected set; }
        protected string Species { get; set; }
        protected DateTime BirthDate { get; set; }

        // Neues Bewegungsverhalten durch Komposition
        public IMovementBehavior MovementBehavior { get; set; }

        protected Animal(string name, string species, DateTime birthDate)
        {
            Name = name;
            Species = species;
            BirthDate = birthDate;
        }

        public virtual void Display()
        {
            Console.WriteLine("Animal: " + Name);
            Console.WriteLine("Species: " + Species);
            MovementBehavior?.Move(Name);
        }

        public int CalculateAge()
        {
            return DateTime.Now.Year - BirthDate.Year;
        }

        public abstract void MakeSound();
    }
}
