using System;
using System.Collections.Generic;

namespace SITA.src.Storage
{
    public interface IStorage<T>
    {
        void AddData(string id, T data);
        T GetData(string id);
        List<T> GetAllData();

        // Adicionei esta linha para os Controllers enxergarem o método
        T GetDataByField(Func<T, bool> predicate);
    }
}