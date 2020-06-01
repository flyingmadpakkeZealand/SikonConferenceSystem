using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SikonConferenceSystem.Common;
using SikonConferenceSystem.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SikonConferenceSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventsPage : Page
    {
        private Type _eventDestination;
        private string _eventDestinationButtonText;
        private SpecialCase _specialCase;
        public uint MaxHeightForGrid { get; set; }
        
        public EventsPage()
        {
            MaxHeightForGrid = MainPage.AproxFrameHeight;
            this.InitializeComponent();
            //TestCVS.Source = CreateGroups2();

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                object data;

                switch (_specialCase)
                {
                    case SpecialCase.OnSpeakerEdit:
                    {
                        data = new[] { button.CommandParameter, SpecialCase.OnSpeakerEdit };
                    } break;
                    case SpecialCase.OnAdminEdit:
                    {
                        data = new[] {button.CommandParameter, SpecialCase.OnAdminEdit};
                    } break;
                    default:
                    {
                        data = button.CommandParameter;
                    } break;
                }
                //if (_specialCase == SpecialCase.OnSpeakerEdit)
                //{
                //    object[] data = new[] {button.CommandParameter, SpecialCase.OnSpeakerEdit};
                //    Frame.Navigate(_eventDestination, data);
                //}
                //else
                //{
                //    Frame.Navigate(_eventDestination, button.CommandParameter);
                //}

                Frame.Navigate(_eventDestination, data);
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Content = _eventDestinationButtonText;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is SpecialCase specialCase)
            {
                switch (specialCase)
                {
                    case SpecialCase.OnSpeakerEdit:
                    {
                        EventsPageVm.Handler.ApplySpeakerEditFilter();
                    } break;
                    case SpecialCase.OnAdminEdit:
                    {

                    } break;
                }
                _eventDestination = typeof(SetupEventsPage);
                _eventDestinationButtonText = "Edit Event";
                _specialCase = specialCase;
            }
            else
            {
                _eventDestination = typeof(DetailedEventView);
                _eventDestinationButtonText = "See Event";
            }
            base.OnNavigatedTo(e);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            EventsPageVm.FilterVms.Add(new FilterVM());
            FilterListView.Height = double.NaN;
        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            if (FilterListView.ActualHeight>92)
            {
                FilterListView.Height = 92;
            }
            else
            {
                FilterListView.Height = double.NaN;
            }
        }

        private void ButtonBase_OnClick3(object sender, RoutedEventArgs e)
        {
            EventsPageVm.Handler.ApplyFilter();

            int day = EventsPageVm.SelectedDayIndex;
            EventsPageVm.SelectedDayIndex = day != 0 ? 0 : 1;
            EventsPageVm.SelectedDayIndex = day;
        }
    }
}
