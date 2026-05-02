using System;
using System.Collections.Generic;
using System.Linq;

namespace SITA.src.Storage
{
    public class GeneralStorage<T> : IStorage<T> where T : class
    {
      
        private Dictionary<string, T> _instanciaData = new Dictionary<string, T>();

        
        public Dictionary<string, object> ProgramStorage = new Dictionary<string, object>();

        public void AddData(string id, T data)
        {
            if (!string.IsNullOrEmpty(id) && data != null)
                _instanciaData[id] = data;
        }

        public T GetData(string id)
        {
            return _instanciaData.ContainsKey(id) ? _instanciaData[id] : null;
        }

       
        public List<T> GetAllData()
        {
            return _instanciaData.Values.ToList();
        }

        public T GetDataByField(Func<T, bool> predicate)
        {
            return _instanciaData.Values.FirstOrDefault(predicate);
        }

        public void AddStorage<TType>() where TType : class
        {
            if (!ProgramStorage.ContainsKey(typeof(TType).Name))
                ProgramStorage.Add(typeof(TType).Name, new GeneralStorage<TType>());
        }

        public GeneralStorage<TType> GetStorage<TType>() where TType : class
        {
            return (GeneralStorage<TType>)ProgramStorage[typeof(TType).Name];
        }

        public Dictionary<string, object> GetGeneralStorage() => ProgramStorage;
    }
}