using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Common;

namespace SikonConferenceSystem.ViewModel
{
    public class MainPageViewModel: INotifyPropertyChanged
    {

        public NavigationService NavigationService { get; set; }

        public MainPageViewModel(/*NavigationService navService*/)
        {
            //_navigationService = navService;
            NavToPageCommand = new RelayCommand(NavigateToPage);
            NavBackCommand = new RelayCommand(GoBack);
            NavForwardCommand = new RelayCommand(GoForward);
        }

        private void NavigateToPage(object data)
        {
            if (data is object[] dataArray)
            {
                NavigationService.Navigate((Type) dataArray[0], dataArray[1]);
            }
            else
            {
                NavigationService.Navigate((Type)data);
            }
        }

        private void GoBack()
        {
            NavigationService.GoBack();
        }

        private void GoForward()
        {
            NavigationService.GoForward();
        }

        public ICommand NavToPageCommand { get; set; }
        public ICommand NavBackCommand { get; set; }
        public ICommand NavForwardCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
