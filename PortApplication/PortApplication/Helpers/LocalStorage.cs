using System.Threading.Tasks;
using System;
using Akavache;
using System.Reactive.Linq;
using System.Linq;

namespace PortApplication.Helpers
{
    public static class LocalStorage
    {
        private const string PortKey = "PortKey";

        public static async Task Insert<T>(string key, T data)
        {
            await BlobCache.UserAccount.InsertObject(key, data);
        }

        public static async Task InsertSecure<T>(string key, T data)
        {
            await BlobCache.Secure.InsertObject(key, data);
        }

        public static async Task<T> GetSecure<T>(string key)
        {
            return await BlobCache.Secure.GetObject<T>(key);
        }

        public static async Task<T> Get<T>(string key)
        {
            var data = default(T);
            try
            {
                var c = await BlobCache.UserAccount.GetAllKeys();
                if (c.Contains(key))
                {
                    return await BlobCache.UserAccount.GetObject<T>(key);
                }
            }
            catch (Exception ex)
            {
                App.LogCrash(ex, "LocalStorage.Get<T>");
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return data;
        }

        public static async Task Remove(string key)
        {
            try
            {
                await BlobCache.UserAccount.Invalidate(key);
            }
            catch (Exception ex)
            {
                App.LogCrash(ex, "LocalStorage.Remove");
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task RemoveAll()
        {
            await BlobCache.UserAccount.InvalidateAll();
            await BlobCache.Secure.InvalidateAll();
        }

        public static void Shutdown()
        {
            BlobCache.Shutdown().Wait();
        }
    }
}