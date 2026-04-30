using SITA.src.Model;
using SITA.src.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Controller
{
    public static class AlunoController
    {
        /* Classe estática para controle de alunos
         * 
         * Esta classe fornece métodos estáticos para registrar e obter alunos.
         * Utiliza a classe Storage para persistência de dados.
         */
        static Storage<Aluno> ClassStorage = MauiProgram.AppStorage.GetStorage<Aluno>();

        // Registra um novo aluno no armazenamento
        public static void Register(Aluno obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }

        /* Método para obter um aluno específico do armazenamento
         * Caso o segundo parâmetro seja nulo, retorna o aluno com a chave igual ao primeiro parâmetro (Por padrão, o Id).
         * Caso contrário, retorna o aluno que tenha o valor do campo especificado igual ao segundo parâmetro.
         * 
         * EXEMPLO:
         * 
         * AlunoController.Get("RA", "12345678900") -> Retorna o aluno com RA correspondente
         * AlunoController.Get("Id", "123e4567-e89b-12d3-a456-426614174000") -> Retorna o aluno com Id correspondente
         */
        public static Aluno? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            return ClassStorage.GetDataByField(field, value);
        }
    }
}