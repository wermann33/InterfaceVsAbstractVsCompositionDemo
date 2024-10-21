using InterfVSAbstVCompDemo.BusinessLayer.Models;

namespace InterfVSAbstVCompDemo.Test
{
    [TestFixture]
    public class MovementBehaviorTests
    {
        private RunBehavior _runBehavior;
        private SwimBehavior _swimBehavior;
        private FlyBehavior _flyBehavior;

        [SetUp]
        public void Setup()
        {
            _runBehavior = new RunBehavior();
            _swimBehavior = new SwimBehavior();
            _flyBehavior = new FlyBehavior();
        }

        // Test für RunBehavior
        [Test]
        public void RunBehavior_ShouldReturnRunningMessage_WhenCalled()
        {
            // Arrange
            const string name = "Max";

            // Act
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            _runBehavior.Move(name);
            var result = stringWriter.ToString().Trim();

            // Assert
            Assert.That(result, Is.EqualTo("Max is running!"));
        }

        // Test für SwimBehavior
        [Test]
        public void SwimBehavior_ShouldReturnSwimmingMessage_WhenCalled()
        {
            // Arrange
            const string name = "Nemo";

            // Act
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            _swimBehavior.Move(name);
            var result = stringWriter.ToString().Trim();

            // Assert
            Assert.That(result, Is.EqualTo("Nemo is swimming!"));
        }

        // Test für FlyBehavior
        [Test]
        public void FlyBehavior_ShouldReturnFlyingMessage_WhenCalled()
        {
            // Arrange
            const string name = "Tweety";

            // Act
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            _flyBehavior.Move(name);
            var result = stringWriter.ToString().Trim();

            // Assert
            Assert.That(result, Is.EqualTo("Tweety is flying!"));
        }
    }
}
