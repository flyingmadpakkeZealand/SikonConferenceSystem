using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace SikonConferenceSystem.Handler.Interfaces
{
    interface ILoadEvent
    {
        void LoadEvent(Event eventToLoad);
    }
}
