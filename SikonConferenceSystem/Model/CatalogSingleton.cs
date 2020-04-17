using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikonConferenceSystem.Persistency;

namespace SikonConferenceSystem.Model
{
    public class CatalogSingleton<T>
    {
        private static CatalogSingleton<T> instance;

        public static CatalogSingleton<T> Instance
        {
            get { return instance; }
        }

        private ObservableCollection<T> _catalog;

        public ObservableCollection<T> Catalog
        {
            get { return _catalog; }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
        }

        private event Action _finalActions;

        private CatalogSingleton()
        {
            _catalog = new ObservableCollection<T>();
            _isLoading = true;
            LoadItems();
        }

        private async Task LoadItems()
        {
            Consumer<T> consumer = null;
            try
            {
                consumer = new Consumer<T>(ConsumerCatalog.GetUrl<T>());
            }
            catch (ArgumentException ae)
            {
                _isLoading = false;
                return;
            }

            List<T> loadedItems = await consumer.Get();

            foreach (T loadedItem in loadedItems)
            {
                _catalog.Add(loadedItem);
            }

            _finalActions?.Invoke();
            _isLoading = false;
            _finalActions = null;
        }

        public void Subscribe(Action action)
        {
            _finalActions += action;
        }

        public void Add(T item)
        {
            _catalog.Add(item);
        }

        public void Delete(int index)
        {
            _catalog.RemoveAt(index);
        }
    }
}
