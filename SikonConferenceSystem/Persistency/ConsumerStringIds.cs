using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SikonConferenceSystem.Persistency.Interfaces;

namespace SikonConferenceSystem.Persistency
{
    public class ConsumerStringIds<T> : ConsumerBase<T, string>, IConsumer<T, string>
    {
        private string URL;

        public ConsumerStringIds(string url)
        {
            URL = url;
        }

        public ConsumerStringIds()
        {
            URL = ConsumerCatalog.GetUrl<T>();
        }

        public async Task<List<T>> GetAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<List<T>>(() => client.GetAsync(URL));
            }
        }

        public async Task<T> GetOneAsync(string[] ids)
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<T>(() => client.GetAsync(URL + RouteIds(ids)));
            }
        }

        public async Task<bool> PostAsync(T item)
        {
            StringContent content = EncodeContent(item);
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<bool>(() => client.PostAsync(URL, content));
            }
        }

        public async Task<bool> PutAsync(T item, string[] ids)
        {
            StringContent content = EncodeContent(item);
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<bool>(() => client.PutAsync(URL + RouteIds(ids), content));
            }
        }

        public async Task<bool> DeleteAsync(string[] ids)
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<bool>(() => client.DeleteAsync(URL + RouteIds(ids)));
            }
        }

        protected override string RouteIds(string[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                ids[i] = ids[i].Replace('.', ' ');
            }
            return base.RouteIds(ids);
        }
    }
}
