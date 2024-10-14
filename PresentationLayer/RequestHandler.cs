using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;
using InterfVSAbstVCompDemo.BusinessLayer.DTO;
using InterfVSAbstVCompDemo.BusinessLayer.Models;
using InterfVSAbstVCompDemo.DAL;
using InterfVSAbstVCompDemo.Interfaces;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using System.Xml.Linq;

namespace InterfVSAbstVCompDemo.PresentationLayer
{
    // Diese Klasse verarbeitet die eingehenden Anfragen und gibt entsprechende Antworten zurück
    public class RequestHandler()
    {
        private readonly AnimalRepository _animalRepository = new(); 
        public string HandleRequest(string request, string httpMethod, string? jsonBody)
        {
            {
                if (httpMethod == "GET" && request == "/animals")
                {
                    return GetAnimals();
                }

                // Unterscheidung zwischen Cat und Dog für POST-Anfragen
                if (httpMethod == "POST" && request.StartsWith("/addCat"))
                {
                    return HandleAnimalPost(jsonBody, "cat");
                }

                if (httpMethod == "POST" && request.StartsWith("/addDog"))
                {
                    return HandleAnimalPost(jsonBody, "dog");
                }

                if (httpMethod == "DELETE" && request.StartsWith("/deleteAnimal"))
                {
                    string? name = ExtractQueryParam(request, "name");
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        return "Error: Missing 'name' parameter for delete.";
                    }

                    var success = _animalRepository.RemoveAnimalByName(name);
                    return success ? $"Animal '{name}' deleted successfully." : $"Animal '{name}' not found.";
                }

                return "Unknown request.";
            }
        }

        private string HandleAnimalPost(string? jsonBody, string speciesType)
        {
            if (string.IsNullOrWhiteSpace(jsonBody))
            {
                return "Error: No JSON body provided.";
            }

            var newAnimalDto = JsonSerializer.Deserialize<AnimalDto>(jsonBody);

            if (string.IsNullOrWhiteSpace(newAnimalDto.Name))
            {
                return "Error: 'Name' field is required.";
            }

            ElementType elementType = ParseElementType(newAnimalDto.Element);
            IMovementBehavior movementBehavior = ParseMovementBehavior(newAnimalDto.Movement);
            DateTime birth = string.IsNullOrWhiteSpace(newAnimalDto.BirthDate) ? DateTime.Now : DateTime.Parse(newAnimalDto.BirthDate);

            if (speciesType == "cat")
            {
                _animalRepository.AddAnimal(new Cat(newAnimalDto.Name, birth, movementBehavior, elementType));
                return $"Cat '{newAnimalDto.Name}' added successfully.";
            } else
            {
                _animalRepository.AddAnimal(new Dog(newAnimalDto.Name, birth, movementBehavior, elementType));
                return $"Dog '{newAnimalDto.Name}' added successfully.";
            }
        }

        // Hilfsfunktion zur Extraktion von Query-Parametern aus der URL
        private string? ExtractQueryParam(string request, string param)
        {
            var uri = new Uri("http://localhost:8080" + request);
            var query = HttpUtility.ParseQueryString(uri.Query);
            return query.Get(param);
        }

        // Methode zur Ausgabe aller Tiere
        private string GetAnimals()
        {
            var animals = _animalRepository.GetAllAnimals();
            
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
