using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;
using InterfVSAbstVCompDemo.BusinessLayer.DTO;
using InterfVSAbstVCompDemo.BusinessLayer.Models;
using InterfVSAbstVCompDemo.DAL;
using InterfVSAbstVCompDemo.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using System.Threading.Tasks;

namespace InterfVSAbstVCompDemo.PresentationLayer
{
    // Diese Klasse verarbeitet die eingehenden Anfragen und gibt entsprechende Antworten zurück
    public class RequestHandler
    {
        private readonly IRepository _animalRepository;

        // Standardkonstruktor für die Anwendung ohne Test
        public RequestHandler() : this(new AnimalRepository()) { }

        // Konstruktor für Tests mit Dependency Injection
        public RequestHandler(IRepository repository)
        {
            _animalRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<string> HandleRequestAsync(string request, string httpMethod, string? jsonBody)
        {
            if (httpMethod == "GET" && request == "/animals")
            {
                return await GetAnimalsAsync();
            }

            // Unterscheidung zwischen Cat und Dog für POST-Anfragen
            if (httpMethod == "POST" && request.StartsWith("/addCat"))
            {
                return await HandleAnimalPostAsync(jsonBody, "cat");
            }

            if (httpMethod == "POST" && request.StartsWith("/addDog"))
            {
                return await HandleAnimalPostAsync(jsonBody, "dog");
            }

            if (httpMethod == "DELETE" && request.StartsWith("/deleteAnimal"))
            {
                string? name = ExtractQueryParam(request, "name");
                if (string.IsNullOrWhiteSpace(name))
                {
                    return "Error: Missing 'name' parameter for delete.";
                }

                var success = await _animalRepository.RemoveAnimalByNameAsync(name);
                return success ? $"Animal '{name}' deleted successfully." : $"Animal '{name}' not found.";
            }

            return "Unknown request.";
        }

        public async Task<string> HandleAnimalPostAsync(string? jsonBody, string speciesType)
        {
            if (string.IsNullOrWhiteSpace(jsonBody))
            {
                return "Error: No JSON body provided.";
            }

            var newAnimalDto = JsonSerializer.Deserialize<AnimalDto>(jsonBody);

            if (string.IsNullOrWhiteSpace(newAnimalDto?.Name))
            {
                return "Error: 'Name' field is required.";
            }

            ElementType elementType = ParseElementType(newAnimalDto.Element);
            IMovementBehavior movementBehavior = ParseMovementBehavior(newAnimalDto.Movement);
            DateTime birth = string.IsNullOrWhiteSpace(newAnimalDto.BirthDate) ? DateTime.Now : DateTime.Parse(newAnimalDto.BirthDate);

            if (speciesType == "cat")
            {
                await _animalRepository.AddAnimalAsync(new Cat(newAnimalDto.Name, birth, movementBehavior, elementType));
                return $"Cat '{newAnimalDto.Name}' added successfully.";
            } else
            {
                await _animalRepository.AddAnimalAsync(new Dog(newAnimalDto.Name, birth, movementBehavior, elementType));
                return $"Dog '{newAnimalDto.Name}' added successfully.";
            }
        }

        // Hilfsfunktion zur Extraktion von Query-Parametern aus der URL
        public string? ExtractQueryParam(string request, string param)
        {
            var uri = new Uri("http://localhost:8080" + request);
            var query = HttpUtility.ParseQueryString(uri.Query);
            return query.Get(param);
        }

        // Methode zur Ausgabe aller Tiere
        private async Task<string> GetAnimalsAsync()
        {
            var animals = await _animalRepository.GetAllAnimalsAsync();

            var animalDtos = animals.Select(animal => new AnimalDto
            {
                Name = animal.Name,
                Species = animal.Species,
                Element = animal.Element.ToString(),  // Enum als String
                Movement = animal.MovementType,       // Bewegungstyp als String
                BirthDate = animal.BirthDate.ToString("yyyy-MM-dd")  // Datumsformat
            }).ToList();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }  // Enum-Konvertierung
            };

            return JsonSerializer.Serialize(animalDtos, options);
        }

        // Hilfsfunktion zur Konvertierung von Element-Strings zu ElementType
        private ElementType ParseElementType(string? element)
        {
            return element?.ToLower() switch
            {
                "fire" => ElementType.FIRE,
                "water" => ElementType.WATER,
                "earth" => ElementType.EARTH,
                "air" => ElementType.AIR,
                _ => ElementType.NORMAL,
            };
        }

        // Hilfsfunktion zur Konvertierung von Bewegungs-Strings zu IMovementBehavior
        private IMovementBehavior ParseMovementBehavior(string? movement)
        {
            return movement?.ToLower() switch
            {
                "run" => new RunBehavior(),
                "swim" => new SwimBehavior(),
                "fly" => new FlyBehavior(),
                _ => new RunBehavior(),
            };
        }
    }
}
