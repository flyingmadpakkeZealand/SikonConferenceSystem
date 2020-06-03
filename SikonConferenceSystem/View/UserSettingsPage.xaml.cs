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
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Persistency;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SikonConferenceSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserSettingsPage : Page
    {
        public UserSettingsPage()
        {
            this.InitializeComponent();
        }

        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                passwordBox1.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                passwordBox1.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }

        private async void MessageCheckBoxOn(object sender, RoutedEventArgs e)
        {
            Consumer<User> SettingsFacade = new Consumer<User>("http://localhost:61467/api/MessageSettings");

            
        }

        private void MessageCheckBoxOff(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
