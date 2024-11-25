using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;

namespace InterfVSAbstVCompDemo.DAL
{
    // Interface definiert grundlegende Methoden zur Verwaltung von Tieren im Repository
    public interface IRepository
    {
        Task<IEnumerable<Animal>> GetAllAnimalsAsync();  

        Task AddAnimalAsync(Animal animal);  

        Task<bool> RemoveAnimalByNameAsync(string name);  
    }
}
