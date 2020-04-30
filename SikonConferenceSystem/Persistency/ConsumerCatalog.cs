using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace SikonConferenceSystem.Persistency
{
    public static class ConsumerCatalog
    {
        private static Dictionary<Type, string> _urlDictionary = new Dictionary<Type, string>()
        {
            {typeof(User), "http://localhost:61467/api/Users" },
            {typeof(Admin), "http://localhost:61467/api/Admins" },
            {typeof(Speaker), "http://localhost:61467/api/Speakers" },
            {typeof(Event), "http://localhost:61467/api/Events" },
            {typeof(Booking), "http://localhost:61467/api/Bookings" },
            {typeof(Room), "http://localhost:61467/api/Rooms" }
        };

        public static string GetUrl<T>()
        {
            if (_urlDictionary.ContainsKey(typeof(T)))
            {
                return _urlDictionary[typeof(T)];
            }
            throw new ArgumentException($"The type {typeof(T)} has no Url associated");
        }
    }
}
