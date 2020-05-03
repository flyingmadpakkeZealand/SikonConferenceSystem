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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ModelLibrary;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SikonConferenceSystem.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SetupEventsPage : Page
    {
        public string SetImagePath
        {
            set
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(value));
                    EventImage.Source = bitmap;
                }
                catch (FormatException fe)
                {

                }
            }
            get { return ""; }
        }

        public SetupEventsPage()
        {
            this.InitializeComponent();
        }

        private void SpeakersInEventSuggestBox_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                SpeakersInEventSuggestBox.ItemsSource = SetupEventsPageVm.Handler.SuggestBoxSpeakers(sender.Text);
            }
        }

        private void SpeakersInEventSuggestBox_OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            Speaker chosenSpeaker = args.ChosenSuggestion as Speaker;
            if (chosenSpeaker != null)
            {
                SetupEventsPageVm.SpeakersInEvent.Add(chosenSpeaker);
            }
            else
            {
                bool addedOk = SetupEventsPageVm.Handler.AddUsingText(args.QueryText);
                if (!addedOk)
                {
                    sender.ContextFlyout.ShowAt(sender);
                }
            }
        }

        private void SpeakerViewDeleteButton_OnLoaded(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            deleteButton.Command = SetupEventsPageVm.PressSpeakersInEventDeleteCommand;
        }
    }
}
