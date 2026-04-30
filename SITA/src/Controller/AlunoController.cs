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
        static Storage<Aluno> ClassStorage = MauiProgram.AppStorage.GetStorage<Aluno>();
        public static void Register(Aluno obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }
        public static Aluno? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            return ClassStorage.GetDataByField(field, value);
        }
    }
}
