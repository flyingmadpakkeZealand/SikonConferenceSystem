using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using SikonConferenceSystem.View;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Common
{ //Initial Commit. //Initial Commit - CreateEvents_Draft. //Initial Commit - ProfilesTab_Draft.
    public class NavigationService
    {
        private readonly Frame _frame;

        public NavigationService(object frameObject)
        {
            if (frameObject is Frame frame)
            {
                _frame = frame;
            }
            else
            {
                throw new ArgumentException("Unsupported object " + frameObject);
            }
        }

        public NavigationService(Frame frame)
        {
            _frame = frame;
        }

        public object EventsPage { get { return typeof(EventsPage); } }
        public object DetailedEventPage { get { return typeof(DetailedEventView); } }

        public void GoBack()
        {
            if(_frame.CanGoBack)
                _frame.GoBack();
        }

        public void GoForward()
        {
            if(_frame.CanGoForward)
                _frame.GoForward();
        }

        public bool Navigate<T>(object parameter = null)
        {
            var type = typeof(T);

            return Navigate(type, parameter);
        }
        public bool Navigate(Type source, object parameter = null)
        {
            return _frame.Navigate(source, parameter);
        }
    }
}
