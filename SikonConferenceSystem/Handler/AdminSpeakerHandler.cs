using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Persistency;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class AdminSpeakerHandler
    {
        private AdminSpeakerViewModel _adminSpeakerViewModel;

        public AdminSpeakerHandler(AdminSpeakerViewModel adminSpeakerViewModel)
        {
            _adminSpeakerViewModel = adminSpeakerViewModel;
        }

        public async void CreateSpeaker()
        {
            string speakerName = _adminSpeakerViewModel.NewSpeaker.Name;
            string speakerEmail = _adminSpeakerViewModel.NewSpeaker.Email;
            string speakerPhoneNumber = _adminSpeakerViewModel.NewSpeaker.PhoneNumber;
            string speakerPassword = _adminSpeakerViewModel.NewSpeaker.Password;

            Speaker aSpeaker = new Speaker(speakerName,speakerPhoneNumber,speakerEmail,speakerPassword,"");
            Persistency.Consumer<Speaker> AdminSpeakerFacade = new Persistency.Consumer<Speaker>("http://localhost:61467/api/Speakers");
            bool ok = await AdminSpeakerFacade.PostAsync(aSpeaker);
            ClearSpeaker();
            _adminSpeakerViewModel.AdminSpeakerSingleton.Reload(((RelayCommand)_adminSpeakerViewModel.CreateSpeakerCommand).RaiseCanExecuteChanged);


        }

        public async void DeleteSpeaker()
        {
            int speakerID = _adminSpeakerViewModel.NewSpeaker.Id;
            
            Persistency.Consumer<Speaker> AdminSpeakerFacade = new Persistency.Consumer<Speaker>(ConsumerCatalog.GetUrl<Speaker>());
            bool ok = await AdminSpeakerFacade.DeleteAsync(new[] {(speakerID)});
            ClearSpeaker();
            _adminSpeakerViewModel.AdminSpeakerSingleton.Reload(((RelayCommand)_adminSpeakerViewModel.DeleteSpeakerCommand).RaiseCanExecuteChanged);

        }

        public async void UpdateSpeaker()
        {
            string speakerName = _adminSpeakerViewModel.NewSpeaker.Name;
            string speakerEmail = _adminSpeakerViewModel.NewSpeaker.Email;
            string speakerPhoneNumber = _adminSpeakerViewModel.NewSpeaker.PhoneNumber;
            string speakerPassword = _adminSpeakerViewModel.NewSpeaker.Password;
            int speakerID = _adminSpeakerViewModel.NewSpeaker.Id;

            Speaker aSpeaker = new Speaker(speakerName, speakerPhoneNumber, speakerEmail, speakerPassword, "");
            Persistency.Consumer<Speaker> AdminSpeakerFacade = new Persistency.Consumer<Speaker>(ConsumerCatalog.GetUrl<Speaker>());
            bool ok = await AdminSpeakerFacade.PutAsync(aSpeaker, new[] { (speakerID) });
            ClearSpeaker();
            _adminSpeakerViewModel.AdminSpeakerSingleton.Reload(((RelayCommand)_adminSpeakerViewModel.UpdateSpeakerCommand).RaiseCanExecuteChanged);


        }

        public async void ClearSpeaker()
        {
            string speakerName = "";
            string speakerEmail = "";
            string speakerPhoneNumber = "";
            string speakerPassword = "";


            Speaker aspeaker = new Speaker(speakerName,speakerEmail,speakerPhoneNumber,speakerPassword,"");

            _adminSpeakerViewModel.NewSpeaker = aspeaker;
        }
    }
}
