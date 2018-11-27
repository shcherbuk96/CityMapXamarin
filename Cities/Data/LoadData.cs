using MonkeyCache.LiteDB;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cities.Data
{
    class LoadData : ILoadData
    {
        public async Task<IEnumerable<City>> GetDataAsync()
        {
            Barrel.ApplicationId = Constants.NameDataBase;
            Barrel.EncryptionKey = Constants.KEY;
            //You are online, notify the user

            if (!CrossConnectivity.Current.IsConnected)
            {
                //You are offline, notify the user
                return Barrel.Current.Get<IEnumerable<City>>(key: Constants.URL);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.URL);
                var jsonResponce = await client.GetStringAsync(client.BaseAddress);
                var responce = JsonConvert.DeserializeObject<ListCities>(jsonResponce);

                if (responce != null)
                {
                    Barrel.Current.Add(key: Constants.URL, data: responce.Photos, expireIn: TimeSpan.FromDays(1));
                    return responce.Photos;
                }
                else { return null; }
            }
            
        }
     }
}