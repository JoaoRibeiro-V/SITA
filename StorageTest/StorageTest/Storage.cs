using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageTest
{
    internal class Storage<T> : Dictionary<string, T>
    {
        public void AddData(string key, T data)
        {
            this[key] = data;
        }
        public T GetData(string key){
            return this[key];
        }
    }


}