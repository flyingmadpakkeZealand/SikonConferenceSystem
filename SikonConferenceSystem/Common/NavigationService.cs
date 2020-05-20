﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using SikonConferenceSystem.Common.Interfaces;
using SikonConferenceSystem.View;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Common
{ //Initial Commit. //Initial Commit - CreateEvents_Draft. //Initial Commit - ProfilesTab_Draft.
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
