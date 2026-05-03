using SITA.src.Model;
using SITA.src.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITA.src.Controller
{
    public static class UserController
    {
        static Storage<User> ClassStorage = MauiProgram.AppStorage.GetStorage<User>();
        public static void Register(User obj) => BaseController<User>.Register(obj, u => u.Id.ToString());
        public static void Delete(User obj) => BaseController<User>.Delete(obj, u => u.Id.ToString());
        public static User? Get(string field, string? value) => BaseController<User>.Get(field, value);
        public static List<User> GetAll() => BaseController<User>.GetAll();
    }
}
