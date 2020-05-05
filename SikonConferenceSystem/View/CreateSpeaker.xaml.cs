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
using SikonConferenceSystem.Handler;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SikonConferenceSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateSpeaker : Page
    {
        public CreateSpeaker()
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

        private async void Searchbox_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                sender.ItemsSource = await AdminSpeakerViewModel.AdminSpeakerHandler.FilterBoxAsync(sender.Text);
            }
        }

        private async void Searchbox_OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            Speaker chosenSpeaker = args.ChosenSuggestion as Speaker;
            if (chosenSpeaker != null)
            {
                AdminSpeakerViewModel.NewSpeaker = chosenSpeaker;
            }

            else
            {
                await AdminSpeakerViewModel.AdminSpeakerHandler.SearchResultAsync(args.QueryText);
            }
        }
    }
}
