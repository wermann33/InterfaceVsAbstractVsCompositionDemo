using InterfVSAbstVCompDemo.Interfaces;

namespace InterfVSAbstVCompDemo.BusinessLayer.Models
{
    public class RunBehavior : IMovementBehavior
    {
        public void Move(string name)
        {
            Console.WriteLine(name + " is running!");
        }
    }
}
