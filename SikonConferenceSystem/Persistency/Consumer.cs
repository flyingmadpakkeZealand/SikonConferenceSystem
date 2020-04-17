using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SikonConferenceSystem.Persistency
{
    public class Consumer<T>
    {
        private string URL;

        public Consumer(string url)
        {
            URL = url;
        }

        public async Task<List<T>> GetAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<List<T>>(() => client.GetAsync(URL));
            }
        }

        public async Task<T> GetOneAsync(int[] ids)
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

        public async Task<bool> PutAsync(T item, int[] ids)
        {
            StringContent content = EncodeContent(item);
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<bool>(() => client.PutAsync(URL + RouteIds(ids), content));
            }
        }

        public async Task<bool> DeleteAsync(int[] ids)
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponseAsync<bool>(()=>client.DeleteAsync(URL + RouteIds(ids)));
            }
        }

        private async Task<TB> HandleHTTPResponseAsync<TB>(Func<Task<HttpResponseMessage>> clientResponse)
        {
            HttpResponseMessage response = await clientResponse();
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                TB content = JsonConvert.DeserializeObject<TB>(jsonString);
                return content;
            }
            throw new HttpRequestException(response.StatusCode.ToString());
        }

        private string RouteIds(int[] ids)
        {
            string route = "";
            foreach (int id in ids)
            {
                route += "/" + id;
            }

            return route;
        }

        private StringContent EncodeContent(T item)
        {
            string jsonStr = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
