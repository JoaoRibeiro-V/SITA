using SITA.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Controller
{
    public static class SalarioController
    {
        public static void Register(Salario obj) => BaseController<Salario>.Register(obj, f => f.Id.ToString());
        public static void Delete(Salario obj) => BaseController<Salario>.Delete(obj, f => f.Id.ToString());
        public static Salario? Get(string field, string? value) => BaseController<Salario>.Get(field, value);
        public static List<Salario> GetAll() => BaseController<Salario>.GetAll();
    }
}
