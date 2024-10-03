using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;
using InterfVSAbstVCompDemo.BusinessLayer.Models;

namespace InterfVSAbstVCompDemo.DAL
{
    // Implementierung des Repositories, das die Tiere verwaltet (hinzufügen, abrufen, löschen)
    public class AnimalRepository : IRepository
    {
        private static List<Animal> _animals = new List<Animal>
        {
            new Cat("Whiskers", new DateTime(2019, 6, 10), new RunBehavior(), ElementType.EARTH),
            new Dog("Buddy", new DateTime(2017, 3, 15), new SwimBehavior(), ElementType.WATER)
        };

        // Gibt alle Tiere zurück
        public IEnumerable<Animal> GetAllAnimals()
        {
            return _animals;
        }

        // Fügt ein neues Tier hinzu
        public void AddAnimal(Animal animal)
        {
            _animals.Add(animal);
        }

        // Entfernt ein Tier nach Namen, falls es existiert
        public bool RemoveAnimalByName(string name)
        {
            var animalToRemove = _animals.FirstOrDefault(a => a.Name == name);
            if (animalToRemove != null)
            {
                _animals.Remove(animalToRemove);
                return true;
            }
            return false;
        }
    }
}