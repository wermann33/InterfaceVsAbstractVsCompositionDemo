using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;

namespace InterfVSAbstVCompDemo.DAL
{
    // Interface definiert grundlegende Methoden zur Verwaltung von Tieren im Repository
    public interface IRepository
    {
        // Gibt eine Liste aller Tiere zurück
        IEnumerable<Animal> GetAllAnimals();

        // Fügt ein neues Tier hinzu
        void AddAnimal(Animal animal);

        // Entfernt ein Tier basierend auf dem Namen
        bool RemoveAnimalByName(string name);
    }
}
