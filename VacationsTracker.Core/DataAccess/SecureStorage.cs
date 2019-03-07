using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace VacationsTracker.Core.DataAccess
{
    public class CustomSecureStorage : ISecureStorage
    {
        public async Task<string> GetAsync(string key)
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync(key);
                return oauthToken;
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
            return null;
        }

        public async Task SetAsync(string key, string value)
        {
            try
            {
                await SecureStorage.SetAsync(key, value);
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }

        public bool Remove(string key)
        {
            return SecureStorage.Remove(key);
        }

        public void RemoveAll()
        {
            SecureStorage.RemoveAll();
        }
    }
}
