using SITA.src.Model;
using SITA.src.Storage;

namespace SITA.src.Controller
{
    public static class UserController
    {
        static IStorage<User> ClassStorage = MauiProgram.AppStorage.GetStorage<User>();

        public static void Register(User obj)
        {
            ClassStorage.AddData(obj.Id.ToString(), obj);
        }

        public static User? Get(string field, string? value)
        {
            if (value == null) { return ClassStorage.GetData(field); }
            var storage = (GeneralStorage<User>)ClassStorage;
            return storage.GetDataByField(u =>
                field == "Email" ? u.Email == value :
                field == "CPF" ? u.CPF == value :
                u.Id.ToString() == value);
        }
    }
}