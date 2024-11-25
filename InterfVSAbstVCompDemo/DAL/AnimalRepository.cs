using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;
using InterfVSAbstVCompDemo.BusinessLayer.Models;
using InterfVSAbstVCompDemo.Interfaces;
using Npgsql;

namespace InterfVSAbstVCompDemo.DAL
{
    // Implementierung des Repositories, das die Tiere verwaltet (hinzufügen, abrufen, löschen)
    public class AnimalRepository : IRepository
    {
        private readonly DatabaseHandler _dbHandler;

        public AnimalRepository()
        {
            _dbHandler = new DatabaseHandler();
            _dbHandler.EnsureTableExists();
        }


        public async Task<IEnumerable<Animal>> GetAllAnimalsAsync()
        {
            List<Animal> animals = new List<Animal>();

            await using var conn = (NpgsqlConnection)_dbHandler.GetConnection();
            await conn.OpenAsync();  // Verwende await, um die Verbindung asynchron zu öffnen
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM animal";
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                string name = reader.GetString(reader.GetOrdinal("name"));
                string species = reader.GetString(reader.GetOrdinal("species"));
                DateTime birthDate = reader.GetDateTime(reader.GetOrdinal("birthdate"));
                ElementType element = Enum.Parse<ElementType>(reader.GetString(reader.GetOrdinal("element")));

                switch (species)
                {
                    case "Cat":
                        animals.Add(new Cat(name, birthDate, new RunBehavior(), element));
                        break;
                    case "Dog":
                        animals.Add(new Dog(name, birthDate, new RunBehavior(), element));
                        break;
                }
            }

            return animals;
        }



        public async Task AddAnimalAsync(Animal animal)
        {
            await using var conn = (NpgsqlConnection)_dbHandler.GetConnection();
            await conn.OpenAsync();  // Asynchrones Öffnen der Verbindung
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO animal (name, species, birthdate, element) VALUES (@name, @species, @birthdate, @element)";
            cmd.Parameters.Add(new NpgsqlParameter("name", animal.Name));
            cmd.Parameters.Add(new NpgsqlParameter("species", animal.Species));
            cmd.Parameters.Add(new NpgsqlParameter("birthdate", animal.BirthDate));
            cmd.Parameters.Add(new NpgsqlParameter("element", animal.Element.ToString()));
            await cmd.ExecuteNonQueryAsync();  // SQL-Abfrage asynchron ausführen
        }

        public async Task<bool> RemoveAnimalByNameAsync(string name)
        {
            await using var conn = (NpgsqlConnection)_dbHandler.GetConnection();
            await conn.OpenAsync();  // Asynchrones Öffnen der Verbindung
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM animal WHERE name = @name";
            cmd.Parameters.Add(new NpgsqlParameter("name", name));
            int rowsAffected = await cmd.ExecuteNonQueryAsync();  // Asynchrone Ausführung
            return rowsAffected > 0;
        }
    }
}