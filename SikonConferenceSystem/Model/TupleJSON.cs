using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SikonConferenceSystem.Model
{
    public class TupleJSON
    {
        public object m_Item1 { get; set; }

        public object m_Item2 { get; set; }

        public object m_Item3 { get; set; }

        public object m_Item4 { get; set; }

        public object m_Item5 { get; set; }

        public object m_Item6 { get; set; }

        public object m_Item7 { get; set; }

        public object m_Item8 { get; set; }

        public object m_Item9 { get; set; }

        private List<MethodInfo> _allItems;

        public TupleJSON()
        {
            Type classType = GetType();
            PropertyInfo[] props = classType.GetProperties();
            _allItems = new List<MethodInfo>(props.Length);
            foreach (PropertyInfo prop in props)
            {
                _allItems.Add(prop.GetMethod);
            }
        }

        public T GetItem<T>(int itemPointer)
        {
            if (itemPointer>9||itemPointer<1)
            {
                throw new ArgumentException("itemPointer must be number between 1(inc.) and 9(inc.)");
            }

            object rawData = _allItems[itemPointer - 1].Invoke(this, null);

            return rawData != null ? JsonConvert.DeserializeObject<T>(rawData.ToString()) : default;
        }
    }
}
