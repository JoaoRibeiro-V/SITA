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
        public static void Register(User obj) {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }
        public static User? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            return ClassStorage.GetDataByField(field, value);
        }
    }
}
