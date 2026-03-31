using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static StorageTest.Program;

namespace StorageTest
{
    /*
     * Essa classe tem como função única (por enquanto) e principal de cadastrar
     * os dados na memória em suas respectivas Storages relacionadas a uma classe
     * a partir de um arquivo JSON, esse arquivo deve obrigatóriamente possuir:
     * 
     * "groupClasses" = array onde se distingue diferentes tipos de classe para loopar num for
     * - "type" = identificador da classe para adicionar no Storage
     * - "defaults" = array onde se coloca usuários/dados padrão do sistema
     * 
     * EXEMPLO:

        "type": "Aluno",
			"defaults": [
				{
					"Name": "Beltrano",
					"Idade": 5,
					"AccessType": {
						"Level": 0,
						"Name": "Aluno(a)"
					}
				},
				{
					"Name": "Fulano",
					"Idade": 8,
					"AccessType": {
						"Level": 0,
						"Name": "Aluno(a)"
					}
				}
			]
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
