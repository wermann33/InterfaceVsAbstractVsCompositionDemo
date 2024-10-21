using InterfVSAbstVCompDemo.Interfaces;

namespace InterfVSAbstVCompDemo.BusinessLayer.Models
{

    public class SwimBehavior : IMovementBehavior
    {
        public void Move(string name)
        {
            Console.WriteLine(name + " is swimming!");
        }
    }
}
