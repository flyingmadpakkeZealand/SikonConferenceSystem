﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SikonConferenceSystem.Persistency.Interfaces;

namespace SikonConferenceSystem.Persistency
{
    public class Consumer<T> : ConsumerBase<T, int>, IConsumer<T, int>
    {
        private string URL;

        public Consumer(string url)
        {
            URL = url;
        }

        public Consumer()
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
    }
}
