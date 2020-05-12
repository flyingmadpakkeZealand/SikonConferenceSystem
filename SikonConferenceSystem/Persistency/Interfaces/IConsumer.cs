using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SikonConferenceSystem.Persistency.Interfaces
{
    public interface IConsumer<T,in TB>
    {
        Task<List<T>> GetAsync();

        Task<T> GetOneAsync(TB[] ids);

        Task<bool> PostAsync(T item);

        Task<bool> PutAsync(T item, TB[] ids);

        Task<bool> DeleteAsync(TB[] ids);
    }
}
