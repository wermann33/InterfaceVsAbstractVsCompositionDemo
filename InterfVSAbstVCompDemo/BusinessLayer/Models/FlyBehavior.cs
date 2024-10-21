using InterfVSAbstVCompDemo.Interfaces;

namespace InterfVSAbstVCompDemo.BusinessLayer.Models
{
    public class FlyBehavior : IMovementBehavior
    {
        public void Move(string name)
        {
            Console.WriteLine(name + " is flying!");
        }
    }
}
