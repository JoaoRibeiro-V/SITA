using SITA.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Controller
{
    public static class ReceitaController
    {
        public static void Register(Receita obj) => BaseController<Receita>.Register(obj, f => f.Id.ToString());
        public static void Delete(Receita obj) => BaseController<Receita>.Delete(obj, f => f.Id.ToString());
        public static Receita? Get(string field, string? value) => BaseController<Receita>.Get(field, value);
        public static List<Receita> GetAll() => BaseController<Receita>.GetAll();
    }
}
