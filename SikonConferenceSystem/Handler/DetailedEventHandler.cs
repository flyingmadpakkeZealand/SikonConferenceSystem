﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class DetailedEventHandler
    {
        public Action<bool> OnClickBook { get; set; }

        private DetailedEventViewModel _detailedEventViewModel;

        public DetailedEventHandler(DetailedEventViewModel detailedEventViewModel)
        {
            _detailedEventViewModel = detailedEventViewModel;
        }
        public void LoadEvent(Event eventToLoad)
        {
            new LoadEventsHandler(_detailedEventViewModel).LoadEvent(eventToLoad);
        }

        public void BookEvent()
        {
            OnClickBook?.Invoke(true); //setting IsChecked to true is part of the OnClickBook method, it doesn't work with OnPropertyChanged(), presumably because of focus loss due to the context flyout.
        }
    }
}
