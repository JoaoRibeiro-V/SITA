using StorageTest.src.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static StorageTest.Program;

namespace StorageTest.src.Util
{
    /*
     * JsonHandler
     * --------------------------------------------
     * Responsável por:
     * - Ler um arquivo JSON
     * - Interpretar sua estrutura
     * - Inserir os dados nos storages corretos
     * 
     * Estrutura obrigatória do JSON:
     * 
     * {
     *   "groupClasses": [
     *     {
     *       "type": "NomeDaClasse",
     *       "defaults": [ { objeto }, { objeto } ]
     *     }
     *   ]
     * }
     * 
   * Funcionamento:
     * - "type" deve corresponder ao nome da classe registrada no GeneralStorage
     * - Cada item em "defaults" é convertido dinamicamente para o tipo correto
     * - Os dados são inseridos via reflexão no Storage correspondente
   */
    internal class JsonHandler
    {
        public class Root
        {
            public List<GroupClass>? groupClasses { get; set; }
        }

        public class GroupClass
        {
            public string? type { get; set; }
            public List<JsonElement>? defaults { get; set; }
        }
        // Carrega dados do JSON e distribui nos storages
        public void LoadIntoGeneralStorage(string path, GeneralStorage generalStorage)
        {
            string json = File.ReadAllText(path);
            Root? root = JsonSerializer.Deserialize<Root>(json);

            if (root?.groupClasses == null) return;

            var storages = generalStorage.GetGeneralStorage();

            foreach (var group in root.groupClasses)
            {
                if (group.type == null || group.defaults == null) continue;

                if (!storages.TryGetValue(group.type, out var storageObj)) continue;

                Type storageType = storageObj.GetType();
                // Obtém o tipo genérico do Storage<T> (ex: Aluno)
                Type entityType = storageType.GetGenericArguments()[0];

                var addMethod = storageType.GetMethod("AddData");
                var nameProp = entityType.GetProperty("Name");

                foreach (var item in group.defaults)
                {
                    var obj = item.Deserialize(entityType);
                    if (obj == null) continue;

                    string key = nameProp?.GetValue(obj)?.ToString();

                    addMethod?.Invoke(storageObj, new object[] { key, obj });

                    Console.WriteLine($"Added {group.type}: {JsonSerializer.Serialize(obj)}");
                }
            }
        }
    }
}
