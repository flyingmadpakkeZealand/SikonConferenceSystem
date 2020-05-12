using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikonConferenceSystem.Persistency.Interfaces;

namespace SikonConferenceSystem.Persistency
{
    public class CommonConsumer<T, TB>
    {
        private IConsumer<T, TB> _consumer;

        public CommonConsumer(IConsumer<T, TB> consumer)
        {
            _consumer = consumer;
        }

        public async Task<List<T>> GetAsync()
        {
            return await _consumer.GetAsync();
        }

        public async Task<T> GetOneAsync(TB[] ids)
        {
            return await _consumer.GetOneAsync(ids);
        }

        public async Task<bool> PostAsync(T item)
        {
            return await _consumer.PostAsync(item);
        }

        public async Task<bool> PutAsync(T item, TB[] ids)
        {
            return await _consumer.PutAsync(item, ids);
        }

        public async Task<bool> DeleteAsync(TB[] ids)
        {
            return await _consumer.DeleteAsync(ids);
        }
    }
}
