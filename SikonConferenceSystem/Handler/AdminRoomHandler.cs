using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Persistency;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class AdminRoomHandler
    {
        private AdminRoomViewModel _adminRoomViewModel;
        public AdminRoomHandler(AdminRoomViewModel adminRoomViewModel)
        {
            _adminRoomViewModel = adminRoomViewModel;
        }

        public async void CreateRoom()
        {
            int roomNr = _adminRoomViewModel.NewRoom.RoomNr;
            int maxPersons = _adminRoomViewModel.NewRoom.RoomMaxPersons;
            int autistSeats = _adminRoomViewModel.NewRoom.AutistSeats;
            Room aRoom=new Room(roomNr,maxPersons,autistSeats);
            Consumer<Room> adminRoomFacade = new Persistency.Consumer<Room>("http://localhost:61467/api/Rooms");
            bool ok = await adminRoomFacade.PostAsync(aRoom);
            if (!ok)
            {
                MessageDialogHelper.Show("Der skete en fejl", $"Rum {aRoom.RoomNr} blev ikke oprettet");
            }
            else
            {
                MessageDialogHelper.Show("Alt gik godt", $"Rum {aRoom.RoomNr} blev oprettet");
                ClearRoom();
                _adminRoomViewModel.AdminRoomSingleton.Reload(((RelayCommand)_adminRoomViewModel.CreateRoomCommand).RaiseCanExecuteChanged);
            }
            
        }

        public async void DeleteRoom()
        {
            int roomNr = _adminRoomViewModel.NewRoom.RoomNr;
            Consumer<Room> adminRoomFacade = new Persistency.Consumer<Room>("http://localhost:61467/api/Rooms");
            bool ok = await adminRoomFacade.DeleteAsync(new[] {(roomNr)});

            if (!ok)
            {
                MessageDialogHelper.Show("Der skete en fejl", $"Rum {roomNr} blev ikke slettet");
            }
            else
            {
                MessageDialogHelper.Show("Alt gik godt", $"Rum {roomNr} blev slettet");
                ClearRoom();
                _adminRoomViewModel.AdminRoomSingleton.Reload(((RelayCommand)_adminRoomViewModel.DeleteRoomCommand).RaiseCanExecuteChanged);
            }
        }


            

        public async void UpdateRoom()
        {
            int roomNr = _adminRoomViewModel.NewRoom.RoomNr;
            int maxPersons = _adminRoomViewModel.NewRoom.RoomMaxPersons;
            int autistSeats = _adminRoomViewModel.NewRoom.AutistSeats;
            Room aRoom = new Room(roomNr, maxPersons, autistSeats);
            Consumer<Room> adminRoomFacade = new Persistency.Consumer<Room>("http://localhost:61467/api/Rooms");
            bool ok = await adminRoomFacade.PutAsync(aRoom, new[] {(roomNr)});
            if (!ok)
            {
                MessageDialogHelper.Show("Der skete en fejl", $"Rum {aRoom.RoomNr} blev ikke opdateret");
            }
            else
            {
                MessageDialogHelper.Show("Alt gik godt", $"Rum {aRoom.RoomNr} blev opdateret");
                ClearRoom();
                _adminRoomViewModel.AdminRoomSingleton.Reload(((RelayCommand)_adminRoomViewModel.UpdateRoomCommand).RaiseCanExecuteChanged);
            }
            
        }

        public async void ClearRoom()
        {
            int roomNr = 0;
            int maxPersons = 0;
            int autistSeats = 0;
            Room aRoom = new Room(roomNr,maxPersons,autistSeats);
            _adminRoomViewModel.NewRoom = aRoom;
        }
    }
}
