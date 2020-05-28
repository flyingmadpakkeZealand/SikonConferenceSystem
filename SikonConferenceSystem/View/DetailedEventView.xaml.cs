using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ModelLibrary;
using SikonConferenceSystem.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SikonConferenceSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailedEventView : Page
    {
        public DetailedEventView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Event eEvent = e.Parameter as Event;
            DetailedEventViewModel.Handler.LoadEvent(eEvent);
            base.OnNavigatedTo(e);
        }

        private Event _loadedEvent;

        public Event LoadedEvent(Event eEvent)
        {
            eEvent = _loadedEvent;
            return eEvent;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.IsChecked = !checkBox.IsChecked;
                ConfirmFrame.Navigate(typeof(BookingEventPage), new Action<bool>(eventBooked =>
                    {
                        checkBox.IsChecked = eventBooked;
                        checkBox.ContextFlyout.Hide();
                    }));
                checkBox.ContextFlyout.ShowAt(checkBox);
            }
        }
    }
}
