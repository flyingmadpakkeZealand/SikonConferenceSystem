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

        //private readonly NavigationService _navigationService;

        //private static MainPageViewModel _instance;
        //public static MainPageViewModel Instance
        //{ get { return _instance; } }



        public ICommand NavToPageCommand { get; set; }
        public ICommand NavBackCommand { get; set; }
        public ICommand NavForwardCommand { get; set; }

        // Lock in 
        //Under login skal login knappen ændre sig 

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
