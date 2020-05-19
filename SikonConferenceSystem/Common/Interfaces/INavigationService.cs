using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SikonConferenceSystem.Common.Interfaces
{
    public interface INavigationService
    {
        void GoBack();

        void GoForward();

        bool Navigate<T>(object parameter = null);

        bool Navigate(Type source, object parameter = null);
    }
}
