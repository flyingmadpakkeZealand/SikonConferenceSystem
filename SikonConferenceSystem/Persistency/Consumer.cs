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

        public async Task<List<T>> Get()
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponse<List<T>>(() => client.GetAsync(URL));
            }
        }

        public async Task<T> GetOne(int[] ids)
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponse<T>(() => client.GetAsync(URL + RouteIds(ids)));
            }
        }

        public async Task<bool> Post(T item)
        {
            StringContent content = EncodeContent(item);
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponse<bool>(() => client.PostAsync(URL, content));
            }
        }

        public async Task<bool> Put(T item, int[] ids)
        {
            StringContent content = EncodeContent(item);
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponse<bool>(() => client.PutAsync(URL + RouteIds(ids), content));
            }
        }

        public async Task<bool> Delete(int[] ids)
        {
            using (HttpClient client = new HttpClient())
            {
                return await HandleHTTPResponse<bool>(()=>client.DeleteAsync(URL + RouteIds(ids)));
            }
        }

        private async Task<TB> HandleHTTPResponse<TB>(Func<Task<HttpResponseMessage>> clientResponse)
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
