using SITA.src.Model;
using System.Collections.Generic;

namespace SITA.src.Controller
{
    public static class LogEstoqueController
    {
        public static void Register(LogEstoque obj) =>
            BaseController<LogEstoque>.Register(obj, l => l.Id.ToString());

        public static LogEstoque? Get(string field, string? value) =>
            BaseController<LogEstoque>.Get(field, value);

        public static List<LogEstoque> GetAll() =>
            BaseController<LogEstoque>.GetAll();
    }
}