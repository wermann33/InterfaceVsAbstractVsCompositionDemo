using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
