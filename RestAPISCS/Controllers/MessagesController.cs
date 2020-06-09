using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;
using DBUtility;
using RestAPISCS.App_Start;
using RestAPISCS.DatabaseUtility;
using Message = ModelLibrary.Message;

namespace RestAPISCS.Controllers
{
    public class MessagesController : ApiController
    {
        private ManageGenericWithLambda<SimpleType<int>> bookedEventsManager =
            DataBases.Access<SimpleType<int>>(BaseNames.SikonDatabase, "BookedEvents");

        private ManageGenericWithLambda<Message> messageManager =
            DataBases.Access<Message>(BaseNames.SikonDatabase, "Message");

        //private ManageGenericWithLambda<SimpleType<int>> SettingsMessagesManager =
        //    DataBases.Access<SimpleType<int>>(BaseNames.SikonDatabase, "MessageSetting");



        // GET: api/Messages
        [Route("api/Messages/{EventID}")]
        public List<int> Get(int EventID)
        {
            var fillInts = Fillables.CreateFillSimpleType<int>("UserId");
            List<SimpleType<int>> containedUserIds = bookedEventsManager.GetCustomQuery(fillInts,
                "Select BookedEvents.UserId from BookedEvents Inner Join BookingSettings on BookingSettings.UserId = BookedEvents.UserId where BookingSettings.ReceiveMessages = 1 and BookedEvents.EventId = " + EventID);

            List<int> userIds = new List<int>(containedUserIds.Count);
            foreach (SimpleType<int> containedUserId in containedUserIds)
            {
                userIds.Add(containedUserId.Variable);
            }

            return userIds;
        }

        // GET: api/Messages/5
        public string Get(string id)
        {
            return "value";
        }

        // POST: api/Messages
        public bool Post([FromBody]Message message)
        {
            //string content = message.ToString();
            //string[] splitContent = content.Split(new [] {','},2);
            //string idString = splitContent[0].Remove(0, splitContent[0].IndexOf(":")+2);
            //string messageString = splitContent[1].Remove(0, splitContent[1].IndexOf(":")+2);
            //messageString = messageString.TrimEnd(new char[]{'}', '\r', '\n'}).Trim('"');
            //MessagesManager.Post(Extractables.ExtractMessage(Convert.ToInt32(idString), messageString));

            return messageManager.Post(Extractables.ExtractMessage(message));

        }

        // PUT: api/Messages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Messages/5
        public void Delete(int id)
        {
        }
    }
}
