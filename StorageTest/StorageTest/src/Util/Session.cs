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
        private static Session Instance { get; set; }
        public AccessType AccessType { get; set; }
        public Session()
        {
            AccessType NonUser = new AccessType();
            NonUser.Name = "Nao Logado";
            NonUser.Level = -1;
            Instance = new Session();
            Instance.AccessType = NonUser;
        }
        public static void InitSession(AccessType NewAccess)
        {
            Instance.AccessType = NewAccess;
            Console.WriteLine("Changed access to: ", NewAccess.Name);
        }
    }
}
