using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using InterfVSAbstVCompDemo.Interfaces;



namespace InterfVSAbstVCompDemo.BusinessLayer.Abstracts
{
    public enum ElementType
    {
        FIRE, WATER, EARTH, AIR, NORMAL
    }

    // Abstrakte Klasse Animal
    /// <summary>
    /// Abstrakte KLasse für Animals
    /// </summary>
    public abstract class Animal : ISoundableAndAgeCalculable
    {
        public string Name { get; protected set; }
        public string Species { get; protected set; }
        protected DateTime BirthDate { get; set; }
        public ElementType Element { get; set; }

        // Neues Bewegungsverhalten durch Komposition
        public IMovementBehavior MovementBehavior { get; set; }

        /// <summary>
        /// ANimal-Konstruktor
        /// </summary>
        /// <param name="name">Name des Animals (string)</param>
        /// <param name="species">Species des Animals (string)</param>
        /// <param name="birthDate">Geburtsdatum des Animals (DateTime)</param>
        /// <param name="element">Element des Animals als Enum</param>
        protected Animal(string name, string species, DateTime birthDate, ElementType element)
        {
            Name = name;
            Species = species;
            BirthDate = birthDate;
            Element = element;
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

        public void DisplayType()
        {
            Console.WriteLine($"{Name} is a {Species} of {Element} element");
        }
    }
}
