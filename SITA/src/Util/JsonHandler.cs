using SITA.src.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SITA.src.Util
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
    public class JsonHandler
    {
        public static void PrintClass(object obj)
        {
            string jsonString = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            System.Diagnostics.Debug.WriteLine(jsonString);
        }

        public class Root
        {
            public List<GroupClass>? groupClasses { get; set; }
        }

        public class GroupClass
        {
            public string? type { get; set; }
            public List<JsonElement>? defaults { get; set; }
        }

   

        public void LoadIntoGeneralStorage(string path, GeneralStorage<object> generalStorage)
        {
            if (!File.Exists(path)) return;
            string json = File.ReadAllText(path);
            LoadFromString(json, generalStorage);
        }

        public void LoadFromString(string json, GeneralStorage<object> generalStorage)
        {
            Root? root = JsonSerializer.Deserialize<Root>(json);
            if (root?.groupClasses == null) return;

            // Pega o dicionário de storages internos
            var storages = generalStorage.GetGeneralStorage();

            foreach (var group in root.groupClasses)
            {
                System.Diagnostics.Debug.WriteLine("SITA Debug - Buscando tipo: " + group.type);

                if (group.type == null || group.defaults == null) continue;

                // Busca o storage específico para a classe (Ex: "Aluno")
                if (!storages.TryGetValue(group.type, out var storageObj)) continue;

               
                Type storageType = storageObj.GetType();
                Type entityType = storageType.GetGenericArguments()[0];

                var addMethod = storageType.GetMethod("AddData");
                var idProp = entityType.GetProperty("Id");

                foreach (var item in group.defaults)
                {
                    var obj = item.Deserialize(entityType);
                    if (obj == null) continue;

                    
                    string? key = idProp?.GetValue(obj)?.ToString();

                    if (key == null)
                    {
                        var cpfProp = entityType.GetProperty("CPF");
                        key = cpfProp?.GetValue(obj)?.ToString();
                    }

                    if (key == null) continue;

                    System.Diagnostics.Debug.WriteLine($"SITA: Inserindo em {group.type} com chave {key}");

                    addMethod?.Invoke(storageObj, new object[] { key, obj });
                }
            }
        }
    }
}