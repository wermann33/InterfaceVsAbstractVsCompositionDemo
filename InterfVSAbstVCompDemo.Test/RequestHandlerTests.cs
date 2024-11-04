using NUnit.Framework;
using NSubstitute;
using InterfVSAbstVCompDemo.DAL;
using InterfVSAbstVCompDemo.PresentationLayer;
using InterfVSAbstVCompDemo.BusinessLayer.Models;
using System;
using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;

namespace InterfVSAbstVCompDemo.Test
{
    [TestFixture]
    public class RequestHandlerTests
    {
        private IRepository _mockRepository;
        private RequestHandler _requestHandler;

        [SetUp]
        public void SetUp()
        {
            // Mock des Repositories erstellen
            _mockRepository = Substitute.For<IRepository>();

            // RequestHandler mit Mock-Repository initialisieren
            _requestHandler = new RequestHandler(_mockRepository);
        }

        [Test]
        public void AddAnimal_ShouldCallAddAnimalOnRepository_WhenValidAnimalIsProvided()
        {
            // Arrange
            var jsonBody = "{\"Name\": \"Whiskers\", \"Species\": \"Cat\", \"Element\": \"EARTH\", \"Movement\": \"Run\", \"BirthDate\": \"2019-06-10\"}";

            // Act
            _requestHandler.HandleRequest("/addCat", "POST", jsonBody);

            // Assert
            _mockRepository.Received(1).AddAnimal(Arg.Any<Animal>());
        }

        [Test]
        public void RemoveAnimal_ShouldCallRemoveAnimalOnRepository_WhenAnimalNameIsProvided()
        {
            // Arrange
            var animalName = "Whiskers";

            // Act
            _requestHandler.HandleRequest($"/deleteAnimal?name={animalName}", "DELETE", null);

            // Assert
            _mockRepository.Received(1).RemoveAnimalByName(animalName);
        }

        [Test]
        public void GetAllAnimals_ShouldReturnAllAnimalsFromRepository()
        {
            // Arrange
            var animals = new Animal[]
            {
                new Cat("Whiskers", new DateTime(2019, 6, 10), new RunBehavior(), ElementType.EARTH),
                new Dog("Buddy", new DateTime(2018, 4, 15), new SwimBehavior(), ElementType.WATER)
            };
            _mockRepository.GetAllAnimals().Returns(animals);

            // Act
            var result = _requestHandler.HandleRequest("/animals", "GET", null);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain("Whiskers"));
            Assert.That(result, Does.Contain("Buddy"));
        }

        [Test]
        public void HandleRequest_ShouldReturnUnknownRequest_WhenHttpMethodIsInvalid()
        {
            // Act
            var result = _requestHandler.HandleRequest("/animals", "PATCH", null);

            // Assert
            Assert.That(result, Is.EqualTo("Unknown request."));
        }

        [Test]
        public void HandleAnimalPost_ShouldReturnError_WhenJsonBodyIsMissing()
        {
            // Act
            var result = _requestHandler.HandleAnimalPost(null, "cat");

            // Assert
            Assert.That(result, Is.EqualTo("Error: No JSON body provided."));
        }

        [Test]
        public void HandleAnimalPost_ShouldReturnError_WhenNameIsMissingInJson()
        {
            // Arrange
            string jsonBody = "{\"Movement\":\"run\",\"Element\":\"earth\"}";

            // Act
            var result = _requestHandler.HandleAnimalPost(jsonBody, "cat");

            // Assert
            Assert.That(result, Is.EqualTo("Error: 'Name' field is required."));
        }

        [Test]
        public void ExtractQueryParam_ShouldReturnCorrectValue_WhenParameterIsPresent()
        {
            // Arrange
            string request = "/deleteAnimal?name=Whiskers";

            // Act
            string? result = _requestHandler.ExtractQueryParam(request, "name");

            // Assert
            Assert.That(result, Is.EqualTo("Whiskers"));
        }

        [Test]
        public void ExtractQueryParam_ShouldReturnNull_WhenParameterIsMissing()
        {
            // Arrange
            string request = "/deleteAnimal";

            // Act
            string? result = _requestHandler.ExtractQueryParam(request, "name");

            // Assert
            Assert.That(result, Is.Null);
        }

    }
}
