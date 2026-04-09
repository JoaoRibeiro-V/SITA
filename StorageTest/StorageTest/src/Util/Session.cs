using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageTest.src.Model;

namespace StorageTest.src.Util
{
    internal class Session
    {
        private static Session Instance = new Session();
        public AccessType AccessType = new AccessType();
        public void InitSession(AccessType NewAccess)
        {
            Console.WriteLine(NewAccess.ToString());
            Instance.AccessType = NewAccess;
            Console.WriteLine("Changed access to: ", NewAccess.Name);
        }
        public AccessType GetInstanceAccess()
        {
            return Instance.AccessType;
        }
    }
}
