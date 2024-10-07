using InterfVSAbstVCompDemo.BusinessLayer.Abstracts;
using InterfVSAbstVCompDemo.BusinessLayer.Models;
using InterfVSAbstVCompDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfVSAbstVCompDemo.BusinessLayer.DTO
{
    public class AnimalDto
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Element { get; set; }
        public string Movement { get; set; }
        public string BirthDate { get; set; }
    }
}
