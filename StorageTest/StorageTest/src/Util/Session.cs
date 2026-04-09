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
        private AccessType AccessType = new AccessType();
        public void InitSession(AccessType NewAccess)
        {
            Instance.AccessType = NewAccess;
            Console.WriteLine("Changed access to: " + Instance.AccessType.Name);
        }
        public AccessType GetInstanceAccess()
        {
            return Instance.AccessType;
        }
    }
}
