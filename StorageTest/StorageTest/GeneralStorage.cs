using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageTest
{
    /* Classe GeneralStorage:
     * Tem como principal função armazenar diferentes tipos de classe Storage através
     * de um dicionário, identificado por uma string e valor genérico objeto
     */
    internal class GeneralStorage
    {
        public Dictionary<string, object> generalStorage;
        public GeneralStorage()
        {
            this.generalStorage = new Dictionary<string, object>();
        }
        public void AddStorage<T>()
        {
            this.generalStorage.Add(typeof(T).Name, new Storage<T>());
        }
        public Storage<T> GetStorage<T>()
        {
            return (Storage<T>)this.generalStorage.GetValueOrDefault(typeof(T).Name);
        }
        public Dictionary<string, object> GetGeneralStorage()
        {
            return this.generalStorage;
        }

    }
}