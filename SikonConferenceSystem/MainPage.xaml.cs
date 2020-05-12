using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Display.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SikonConferenceSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const uint TopSize = 50;
        private const uint BottomSize = 200;
        private const uint NativeScreenSace = 100;

        public static uint AproxFrameHeight { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            MainPageViewModel.NavigationService = new NavigationService(ContentFrame);
            var screenHeight = DisplayInformation.GetForCurrentView().ScreenHeightInRawPixels;
            AproxFrameHeight = screenHeight - (TopSize + BottomSize + NativeScreenSace);

            UserLoginFrame.Navigate(typeof(UserLoginPage));
            ContentFrame.Navigate(typeof(SetupEventsPage));

            
        }

    }
}
