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
using SikonConferenceSystem.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SikonConferenceSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventsPage : Page
    {
        public uint MaxHeightForGrid { get; set; }
        private List<User> users;
        public EventsPage()
        {
            MaxHeightForGrid = MainPage.AproxFrameHeight;
            this.InitializeComponent();
            //TestCVS.Source = CreateGroups2();
        }

        //Formatting Work around... Not sure about it, but should be fine...
        private void DurationTxt_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (textBlock.DataContext is EventAdapter @event)
                textBlock.Text = @event.FormattedDuration();
        }
    }
}
