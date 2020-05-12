using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikonConferenceSystem.Persistency.Interfaces;

namespace SikonConferenceSystem.Persistency
{
    public static class CommonConsumerFactory
    {
        public static CommonConsumer<T,TB> Create<T, TB>(IConsumer<T, TB> consumer)
        {
            CommonConsumer<T,TB> commonConsumer = new CommonConsumer<T, TB>(consumer);
            return commonConsumer;
        }
    }
}
