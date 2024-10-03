using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;
using InterfVSAbstVCompDemo.BusinessLayer.Models;
using InterfVSAbstVCompDemo.DAL;
using InterfVSAbstVCompDemo.Interfaces;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace InterfVSAbstVCompDemo.PresentationLayer
{
    // Diese Klasse verarbeitet die eingehenden Anfragen und gibt entsprechende Antworten zurück
    public class RequestHandler(IRepository animalRepository)
    {
        public string HandleRequest(string request, string httpMethod)
        {
            // Unterscheidung nach HTTP-Methode
            if (httpMethod == "GET" && request == "/animals")
            {
                return GetAnimals();
            }

            if (httpMethod == "POST" && request.StartsWith("/addCat"))
            {
                // Extrahiere Parameter (Name, Element, Bewegung, Geburtsdatum)
                string? name = ExtractQueryParam(request, "name");
                string? element = ExtractQueryParam(request, "element");
                string? movement = ExtractQueryParam(request, "movement");
                string? birthDate = ExtractQueryParam(request, "birthdate");

                if (string.IsNullOrWhiteSpace(name))
                {
                    return "Error: Missing 'name' parameter for cat.";
                }

                ElementType elementType = ParseElementType(element);
                IMovementBehavior movementBehavior = ParseMovementBehavior(movement);
                DateTime birth = string.IsNullOrWhiteSpace(birthDate) ? DateTime.Now : DateTime.Parse(birthDate);

                animalRepository.AddAnimal(new Cat(name, birth, movementBehavior, elementType));
                return $"Cat '{name}' added successfully.";
            }

            if (httpMethod == "POST" && request.StartsWith("/addDog"))
            {
                // Extrahiere Parameter (Name, Element, Bewegung, Geburtsdatum)
                string? name = ExtractQueryParam(request, "name");
                string? element = ExtractQueryParam(request, "element");
                string? movement = ExtractQueryParam(request, "movement");
                string? birthDate = ExtractQueryParam(request, "birthdate");

                if (string.IsNullOrWhiteSpace(name))
                {
                    return "Error: Missing 'name' parameter for dog.";
                }

                ElementType elementType = ParseElementType(element);
                IMovementBehavior movementBehavior = ParseMovementBehavior(movement);
                DateTime birth = string.IsNullOrWhiteSpace(birthDate) ? DateTime.Now : DateTime.Parse(birthDate);

                animalRepository.AddAnimal(new Dog(name, birth, movementBehavior, elementType));
                return $"Dog '{name}' added successfully.";
            }

            if (httpMethod == "DELETE" && request.StartsWith("/deleteAnimal"))
            {
                // Extrahiere den Namen aus der Anfrage (z.B. /deleteAnimal?name=Whiskers)
                string? name = ExtractQueryParam(request, "name");
                if (string.IsNullOrWhiteSpace(name))
                {
                    return "Error: Missing 'name' parameter for delete.";
                }

                var success = animalRepository.RemoveAnimalByName(name);
                return success ? $"Animal '{name}' deleted successfully." : $"Animal '{name}' not found.";
            }

            return "Unknown request.";
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
            var animals = animalRepository.GetAllAnimals();
            if (!animals.Any())
            {
                return "No animals found.";
            }

            string result = "Animals:\n";
            foreach (var animal in animals)
            {
                result += $"{animal.GetType().Name}: {animal.Name}, Age: {animal.CalculateAge()} years, " +
                          $"Element: {animal.Element}, Movement: {animal.MovementBehavior.GetType().Name}\n";
            }
            return result;
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
