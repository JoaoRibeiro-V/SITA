using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Storage
{
    /* Classe Storage<T>:
     * Armazenamento na memória para um tipo de classe específica
     * 
     * Baseado em Dicionary<string, T> onde:
     * - Key : Identificador único (Exemplo: CPF, RA)
     * - Value : Objeto do tipo T
     * 
     * Exemplo:
     * Storage<Aluno> armazena múltiplos objetos de aluno
     */
    public class Storage<T> : Dictionary<string, T>
    {
        public Storage() { }
        // Funções exemplo, funciona da mesma maneira que um dicionário

        /*
         * Adiciona ou atualiza um item no storage
         */
        public void AddData(string key, T data)
        {
            this[key] = data;
        }
        /*
         * Retorna um objeto pelo identificador
         */
        public T? GetData(string key)
        {
            if (this.TryGetValue(key, out T data)) { return data; }
            return default;
        }
        public T? GetDataByField(string field, string value)
        {
            var property = typeof(T).GetProperty(field);
            if (property == null)
                return default;

            foreach (var item in this.Values)
            {
                var propertyValue = property.GetValue(item);

                if (propertyValue != null && propertyValue.ToString() == value)
                {
                    return item;
                }
            }

            return default;
        }
    }
}