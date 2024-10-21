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


        // Gibt alle Tiere zurück
        public IEnumerable<Animal> GetAllAnimals()
        {
            List<Animal> animals = new List<Animal>();

            using (var conn = _dbHandler.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM animal";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            string species = reader.GetString(reader.GetOrdinal("species"));
                            DateTime birthDate = reader.GetDateTime(reader.GetOrdinal("birthdate"));
                            ElementType element = Enum.Parse<ElementType>(reader.GetString(reader.GetOrdinal("element")));


                            if (species == "Cat")
                            {
                                animals.Add(new Cat(name, birthDate, new RunBehavior(), element));
                            } else if (species == "Dog")
                            {
                                animals.Add(new Dog(name, birthDate, new RunBehavior(), element));
                            }
                        }
                    }
                }
            }
            return animals;
        }


        public void AddAnimal(Animal animal)
        {
            using (var conn = _dbHandler.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())

                {
                    cmd.CommandText =
                        "INSERT INTO animal (name, species, birthdate, element) VALUES (@name, @species, @birthdate, @element)";
                    cmd.Parameters.Add(new NpgsqlParameter("name", animal.Name));
                    cmd.Parameters.Add(new NpgsqlParameter("species", animal.Species));
                    cmd.Parameters.Add(new NpgsqlParameter("birthdate", animal.BirthDate));
                    cmd.Parameters.Add(new NpgsqlParameter("element", animal.Element.ToString()));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool RemoveAnimalByName(string name)
        {
            using (var conn = _dbHandler.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM animal WHERE name = @name";
                    cmd.Parameters.Add(new NpgsqlParameter("name", name));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}