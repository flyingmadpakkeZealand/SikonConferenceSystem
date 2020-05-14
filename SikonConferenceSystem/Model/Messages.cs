using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Persistency;

namespace SikonConferenceSystem.Model
{
    public class ControlMessages
    {



        public async void SendCancelMessages(int eventId)
        {
            Consumer<List<int>> MessageFacade = new Consumer<List<int>>("http://localhost:61467/api/Messages/");
            Consumer<Message> SendMessageFacade = new Consumer<Message>("http://localhost:61467/api/Messages/");
            List<int> messageList = await MessageFacade.GetOneAsync(new[] {eventId});
            foreach (int Id in messageList)
            {
                Message aMessage = new Message(Id, "The event has been canceled");
                bool ok = await SendMessageFacade.PostAsync(aMessage);
            }
        }
    }
}
