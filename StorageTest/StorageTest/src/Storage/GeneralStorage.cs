using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageTest.src.Storage
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
    internal class GeneralStorage
    {
        public Dictionary<string, object> generalStorage;
        public GeneralStorage()
        {
            // Inicializa o armazenamento de dicionários
            generalStorage = new Dictionary<string, object>();
        }
        // Registra um novo tipo de armazenamento
        public void AddStorage<T>()
        {
            generalStorage.Add(typeof(T).Name, new Storage<T>());
        }
        // Retorna o storage de um tipo específico
        public Storage<T> GetStorage<T>()
        {
            return (Storage<T>)generalStorage.GetValueOrDefault(typeof(T).Name);
        }
        // Retorna o dicionário geral
        public Dictionary<string, object> GetGeneralStorage()
        {
            return generalStorage;
        }

    }
}