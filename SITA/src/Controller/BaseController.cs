using SITA.src.Storage;

namespace SITA.src.Controller
{
    public static class BaseController<T> where T : class
    {
        private static Storage<T> Storage => MauiProgram.AppStorage.GetStorage<T>();

        public static void Register(T obj, Func<T, string> getId)
        {
            Storage.AddData(getId(obj), obj);
        }

        public static void Delete(T obj, Func<T, string> getId)
        {
            Storage.Remove(getId(obj));
        }
        public static T? Get(string field, string? value)
        {
            if (value == null)
                return Storage.GetData(field);

            return Storage.GetDataByField(field, value);
        }
        public static List<T> GetAll()
        {
            return Storage.Values.ToList();
        }
        public static T? Copy(T obj)
        {
            return Storage.Copy(obj);
        }
    }
}