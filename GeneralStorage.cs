using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageTest
{
    internal class GeneralStorage
    {
        public Dictionary<string, object> generalStorage;
        public GeneralStorage() {
            this.generalStorage = new Dictionary<string, object>();
        }
        public void AddStorage<T>()
        {
            var newStorage = new Storage<T>();
            this.generalStorage.Add(typeof(T).Name, newStorage);
        }
        public Storage<T> GetStorage<T>(string name)
        {
            return (Storage<T>) this.generalStorage.GetValueOrDefault(typeof(T).Name);
        }
        public Dictionary<string, object> GetGeneralStorage()
        {
            return this.generalStorage;
        }

    }
}
