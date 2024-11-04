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
        public DateTime BirthDate { get; protected set; }
        public ElementType? Element { get; set; }

        // Neues Bewegungsverhalten durch Komposition
        public IMovementBehavior? MovementBehavior { get; set; }

        public string MovementType => MovementBehavior?.GetType().Name ?? "Unknown";


        /// <summary>
        /// Animal-Konstruktor
        /// </summary>
        /// <param name="name">Name des Animals (string)</param>
        /// <param name="species">Species des Animals (string)</param>
        /// <param name="birthDate">Geburtsdatum des Animals (DateTime)</param>
        /// <param name="element">Element des Animals als Enum</param>
        protected Animal(string name, string species, DateTime birthDate, ElementType element)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            }
            if (birthDate > DateTime.Now)
            {
                throw new ArgumentException("Birth date cannot be in the future", nameof(birthDate));
            }

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
            if (BirthDate > DateTime.Now)
            {
                throw new ArgumentException("Birth date cannot be in the future", nameof(BirthDate));
            }

            return DateTime.Now.Year - BirthDate.Year;
        }

        public abstract void MakeSound();

        public void DisplayType()
        {
            Console.WriteLine($"{Name} is a {Species} of {Element} element");
        }
    }
}
