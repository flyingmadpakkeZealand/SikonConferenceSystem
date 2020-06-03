using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using SikonConferenceSystem.Common.Interfaces;
using SikonConferenceSystem.View;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Common
{ //Initial Commit. //Initial Commit - CreateEvents_Draft. //Initial Commit - ProfilesTab_Draft. //Initial commit - EventsPage_UserFilter. //Initial Commit - Master_Backup.
    public enum Contents
    {
        MainPageContent,
        UserLoginContent
    }

    public class NavigationService : INavigationService
    {
        

        private static Dictionary<Contents, Frame> _usedFrames = new Dictionary<Contents, Frame>();

        public static NavigationService SetupService(Frame frame, Contents content)
        {
            if (_usedFrames.TryAdd(content, frame))
            {
                return new NavigationService(_usedFrames[content]);
            }
            return null;
        }

        public static NavigationService SetupService(object frameObject, Contents content)
        {
            if (frameObject is Frame frame)
            {
                if (_usedFrames.TryAdd(content, frame))
                {
                    return new NavigationService(_usedFrames[content]);
                }

                return null;
            }
            else
            {
                throw new ArgumentException("Unsupported object " + frameObject);
            }
        }

        public static NavigationService GetService(Contents content)
        {
            if (_usedFrames.ContainsKey(content))
            {
                return new NavigationService(_usedFrames[content]);
            }

            return null;
        }



        private readonly Frame _frame;

        private NavigationService(Frame frame)
        {
            _frame = frame;
        }

        public Type EventsPage { get { return typeof(EventsPage); } }
        public Type DetailedEventPage { get { return typeof(DetailedEventView); } }
        public Type UserLoginProfileMenu { get { return typeof(UserLoginProfileMenu); } }
        public Type SetupEventsPage { get { return typeof(SetupEventsPage); } }
        public Type UserLoginSignUpMenu { get { return typeof(UserLoginSignupMenu); } }
        public Type CreateSpeakerPage { get { return typeof(CreateSpeaker); } }
        public Type CreateRoomsPage { get { return typeof(CreateRooms); } }
        public Type UserSettingsPage { get { return typeof(UserSettingsPage); } }

        public bool CanGoBack
        {
            get { return _frame.CanGoBack; }
        }

        public bool CanGoForward
        {
            get { return _frame.CanGoForward; }
        }

        public void GoBack()
        {
            if(CanGoBack)
                _frame.GoBack();
        }

        public void GoForward()
        {
            if(CanGoForward)
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
