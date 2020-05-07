using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModelLibrary;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.Model;

namespace SikonConferenceSystem.ViewModel
{
    public class AdminRoomViewModel:INotifyPropertyChanged
    {
        private Room _newRoom;
        public AdminRoomHandler AdminRoomHandler { get; set; }
        public int SelectedRoomIndex { get; set; }
        public CatalogSingleton<Room> AdminRoomSingleton { get; set; }

        public AdminRoomViewModel()
        {
            AdminRoomSingleton=CatalogSingleton<Room>.Instance;
            _newRoom=new Room();
            AdminRoomHandler=new AdminRoomHandler(this);
            CreateRoomCommand=new RelayCommand(AdminRoomHandler.CreateRoom);
            DeleteRoomCommand=new RelayCommand(AdminRoomHandler.DeleteRoom);
            UpdateRoomCommand=new RelayCommand(AdminRoomHandler.UpdateRoom);
            ClearRoomCommand=new RelayCommand(AdminRoomHandler.ClearRoom);
        }


        public Room NewRoom
        {
            get { return _newRoom; }
            set
            {
                if (value!=null)
                {
                    _newRoom = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TransitonRoomNr));
                    OnPropertyChanged(nameof(TransitonMaxPerson));
                    OnPropertyChanged(nameof(TransitonAutistSeats));
                    ((RelayCommand)DeleteRoomCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)CreateRoomCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)UpdateRoomCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public int TransitonRoomNr
        {
            get { return NewRoom.RoomNr; }
            set
            {
                NewRoom.RoomNr = value;
                ((RelayCommand)CreateRoomCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateRoomCommand).RaiseCanExecuteChanged();
            }
        }
        public int TransitonMaxPerson
        {
            get { return NewRoom.RoomMaxPersons; }
            set
            {
                NewRoom.RoomMaxPersons = value;
                ((RelayCommand)CreateRoomCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateRoomCommand).RaiseCanExecuteChanged();
            }
        }
        public int TransitonAutistSeats
        {
            get { return NewRoom.AutistSeats; }
            set
            {
                NewRoom.AutistSeats = value;
                ((RelayCommand)CreateRoomCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateRoomCommand).RaiseCanExecuteChanged();
            }
        }
        public ICommand CreateRoomCommand { get; set; }
        public ICommand DeleteRoomCommand { get; set; }
        public ICommand UpdateRoomCommand { get; set; }
        public ICommand ClearRoomCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
