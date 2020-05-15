using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using SikonConferenceSystem.Common;

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
                    SetupEventsPageVm.ImagePath = value;
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
            SetupEventsPageVm.Handler.TriggerOverrideDialogOnSaveEvent = DisplayOverrideConfirmation;
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
            deleteButton.Visibility =
                SetupEventsPageVm.DisableAdminControls ? Visibility.Collapsed : Visibility.Visible;
            deleteButton.Command = SetupEventsPageVm.PressSpeakersInEventDeleteCommand;
        }

        private void EventImage_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (SetupEventsPageVm.ImagePath != null)
            {
                SetImagePath = SetupEventsPageVm.ImagePath;
            }
        }

        private async Task<bool> DisplayOverrideConfirmation()
        {
            ContentDialog overrideEventDialog = new ContentDialog()
            {
                Title = "Confirm override",
                Content = "This event already exist, do you want to override it?",
                PrimaryButtonText = "Override",
                SecondaryButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };

            ContentDialogResult result = await overrideEventDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                return true;
            }

            return false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is object[] data)
            {
                if (data.Length == 2 && data[0] is Event eventToLoad && data[1] is SpecialCase.OnSpeakerEdit)
                {
                    SetupEventsPageVm.LoadOrSetupEvent(eventToLoad);
                    SetupEventsPageVm.DisableAdminControls = true;
                }
            }
            
            base.OnNavigatedTo(e);
        }
    }
}
