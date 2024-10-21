using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;
using InterfVSAbstVCompDemo.BusinessLayer.Models;

namespace InterfVSAbstVCompDemo.Test
{
    [TestFixture]
    public class AnimalTests
    {
        private Cat _cat;
        private Dog _dog;

        [SetUp]
        public void Setup()
        {
            _cat = new Cat("Whiskers", DateTime.Now, new RunBehavior(), ElementType.EARTH);
            _dog = new Dog("Buddy", DateTime.Now, new RunBehavior(), ElementType.FIRE);
        }

        // Test für die Cat-Klasse: Überprüfen der MakeSound Methode
        [Test]
        public void MakeSound_ShouldReturnMeow_WhenCalled()
        {
            // Act
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            _cat.MakeSound();
            var result = stringWriter.ToString().Trim();

            // Assert
            Assert.That(result, Is.EqualTo("Whiskers says: Meow!"));
        }

        // Test für die Dog-Klasse: Überprüfen der MakeSound Methode
        [Test]
        public void MakeSound_ShouldReturnWoof_WhenCalled()
        {
            // Act
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            _dog.MakeSound();
            var result = stringWriter.ToString().Trim();

            // Assert
            Assert.That(result, Is.EqualTo("Buddy says: Woof Woof!"));
        }

        // Test für die CalculateAge Methode
        [Test]
        public void CalculateAge_ShouldReturnCorrectAge()
        {
            // Arrange
            var birthDate = new DateTime(2018, 1, 1);
            var cat = new Cat("Kitty", birthDate, new RunBehavior(), ElementType.FIRE);

            // Act
            var age = cat.CalculateAge();

            // Assert
            Assert.That(age, Is.EqualTo(DateTime.Now.Year - birthDate.Year));
        }

        // Test für DisplayType Methode
        [Test]
        public void DisplayType_ShouldReturnCorrectElement_WhenCalled()
        {
            // Arrange
            const ElementType elementType = ElementType.WATER;
            var cat = new Cat("Splash", DateTime.Now, new SwimBehavior(), elementType);

            // Act
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            cat.DisplayType();
            var result = stringWriter.ToString().Trim();

            // Assert
            Assert.That(result, Is.EqualTo("Splash is a Cat of WATER element"));
        }

        // Test für ungültiges Alter
        [Test]
        public void CalculateAge_ShouldThrowException_WhenBirthDateInFuture()
        {
            // Arrange
            var birthDate = DateTime.Now.AddYears(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Cat("FutureCat", birthDate, new RunBehavior(), ElementType.FIRE));
        }

        // Test für Animal Name null oder leer
        [Test]
        public void Constructor_ShouldThrowException_WhenNameIsNullOrEmpty()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new Cat(null, DateTime.Now, new RunBehavior(), ElementType.FIRE));
            Assert.Throws<ArgumentException>(() => new Cat(string.Empty, DateTime.Now, new RunBehavior(), ElementType.FIRE));
        }

        // Test für Display Methode
        [Test]
        public void Display_ShouldOutputCorrectInformation_WhenCalled()
        {
            // Arrange
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            _cat.Display();
            var result = stringWriter.ToString().Trim();

            // Assert
            Assert.That(result, Does.Contain("Animal: Whiskers"));
            Assert.That(result, Does.Contain("Species: Cat"));
        }
    }
}