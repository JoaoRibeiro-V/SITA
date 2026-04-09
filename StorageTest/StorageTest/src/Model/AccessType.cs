using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageTest.src.Model
{
    internal class AccessType
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public AccessType()
        {
            Level = -1;
            Name = "Não Logado";
        }
    }
}
