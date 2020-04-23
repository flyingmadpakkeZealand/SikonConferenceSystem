using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class AdminSpeakerHandler
    {
        private AdminSpeakerViewModel adminSpeakerViewModel;

        public AdminSpeakerHandler(AdminSpeakerViewModel adminSpeakerViewModel)
        {
            adminSpeakerViewModel = adminSpeakerViewModel;
        }

        public async void CreateSpeaker()
        {
            string speakerName = adminSpeakerViewModel.NewSpeaker.Name;
            string speakerEmail = adminSpeakerViewModel.NewSpeaker.Email;
            string speakerPhoneNumber = adminSpeakerViewModel.NewSpeaker.PhoneNumber;
            string speakerPassword = adminSpeakerViewModel.NewSpeaker.Password;

            Speaker aSpeaker = new Speaker(speakerName,speakerPhoneNumber,speakerEmail,speakerPassword,"");
            Persistency.Consumer<Speaker> AdminSpeakerFacade = new Persistency.Consumer<Speaker>("http://localhost:61467/api/Speakers");
            bool ok = await AdminSpeakerFacade.PostAsync(aSpeaker);

        }

        public async void DeleteSpeaker()
        {

        }

        public async void UpdateSpeaker()
        {

        }

        public async void ClearSpeaker()
        {

        }
    }
}
