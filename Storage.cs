using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageTest
{
    internal class Storage<T>
    {
        public Dictionary<string, T> storages;
        public Storage()
        {
            storages = new Dictionary<string, T>();
        }
        public void AddData(string key, T data)
        {
            storages[key] = data;
        }
    }


}
