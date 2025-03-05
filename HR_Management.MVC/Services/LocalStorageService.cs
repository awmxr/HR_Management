using HR_Management.MVC.Contracts;
using Hanssens.Net;
namespace HR_Management.MVC.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly LocalStorage _storage;
        public LocalStorageService()
        {
            var configs = new LocalStorageConfiguration()
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "HR.LEVEMGMT"
            };
            _storage = new LocalStorage(configs);
        }
        public void ClearStorage(List<string> keys)
        {
            foreach(var key in keys)
            {
                _storage.Remove(key);
            }
        }

        public bool Exist(string key)
        {
            return _storage.Exists(key);
        }

        public T GetStorageValue<T>(string key)
        {
            return _storage.Get<T>(key);
        }

        public void SetStorageValue<T>(string key, T value)
        {
            _storage.Store(key, value);
            _storage.Persist();
        }
    }
}
