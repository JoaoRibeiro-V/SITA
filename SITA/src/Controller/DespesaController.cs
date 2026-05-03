using SITA.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Controller
{
    public static class DespesaController
    {
        public static void Register(Despesa obj) => BaseController<Despesa>.Register(obj, f => f.Id.ToString());
        public static void Delete(Despesa obj) => BaseController<Despesa>.Delete(obj, f => f.Id.ToString());
        public static Despesa? Get(string field, string? value) => BaseController<Despesa>.Get(field, value);
        public static List<Despesa> GetAll() => BaseController<Despesa>.GetAll();
    }
}
