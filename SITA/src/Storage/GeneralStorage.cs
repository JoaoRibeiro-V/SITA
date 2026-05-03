using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Storage
{
    /* Classe GeneralStorage:
     * Container central que gerencia múltiplos Storages
     * 
     * Estrutura interna:
     *  - Dicionary<string, object>
     *      - Key : Nome da classe (Exemplo: "Aluno")
     *      - Value : Objeto de Storage<T>
     *      
     * Permite acesso dinâmico para diferentes tipos de dados
     */
    public class GeneralStorage
    {
        public Dictionary<string, object> ProgramStorage = new Dictionary<string, object>();
        // Registra um novo tipo de armazenamento
        public void AddStorage<T>()
        {
            ProgramStorage.Add(typeof(T).Name, new Storage<T>());
        }
        /*
         * Retorna um storage especificado pela classe
         * Caso não for encontrado é criado um storage da classe providenciada
         */

        public Storage<T> TryGetOrAddStorage<T>()
        {
            if (ProgramStorage.TryGetValue(typeof(T).Name, out var storage))
            {
                return (Storage<T>)storage;
            }
            else
            {
                this.AddStorage<T>();
                return this.GetStorage<T>();
            }
        }
        /* Retorna um storage especificado por classe com restrição de ele já estar cadastrado
         * Caso não for encontrado é exibido um erro
         */
        public Storage<T> GetStorage<T>()
        {
            return (Storage<T>) ProgramStorage[typeof(T).Name];
        }
        // Retorna o dicionário geral
        public Dictionary<string, object> GetGeneralStorage()
        {
            return ProgramStorage;
        }

    }
}