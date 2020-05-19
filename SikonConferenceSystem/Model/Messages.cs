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


            Consumer<Event> GetTitleFacade = new Consumer<Event>("http://localhost:61467/api/Messages/");
            Event aEvent = await GetTitleFacade.GetOneAsync(new[] { eventId });
            string description = aEvent.Abstract;
            string indexString = description.Split(';', 2)[0];
            string pureAbstract = description.Remove(0, indexString.Length + 1);
            int headerLength = Convert.ToInt32(indexString);
            string AbstractHeader = pureAbstract.Substring(0, headerLength);

            
            foreach (int Id in messageList)
            {
                Message aMessage = new Message(Id, $"The event {AbstractHeader} has been canceled");
                bool ok = await SendMessageFacade.PostAsync(aMessage);
            }
        }

        public async void SendChangedMessages(int eventId)
        {
            Consumer<List<int>> MessageFacade = new Consumer<List<int>>("http://localhost:61467/api/Messages/");
            Consumer<Message> SendMessageFacade = new Consumer<Message>("http://localhost:61467/api/Messages/");
            List<int> messageList = await MessageFacade.GetOneAsync(new[] { eventId });


            Consumer<Event> GetTitleFacade = new Consumer<Event>("http://localhost:61467/api/Messages/");
            Event aEvent = await GetTitleFacade.GetOneAsync(new[] { eventId });
            string description = aEvent.Abstract;
            string indexString = description.Split(';', 2)[0];
            string pureAbstract = description.Remove(0, indexString.Length + 1);
            int headerLength = Convert.ToInt32(indexString);
            string AbstractHeader = pureAbstract.Substring(0, headerLength);


            
            foreach (int Id in messageList)
            {
                Message aMessage = new Message(Id, $"The event {AbstractHeader} has been changed");
                bool ok = await SendMessageFacade.PostAsync(aMessage);
            }
        }
    }
}
