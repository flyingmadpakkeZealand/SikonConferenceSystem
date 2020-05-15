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
        private ManageGenericWithLambda<SimpleType<int>> MessagesManager =
            DataBases.Access<SimpleType<int>>(BaseNames.SikonDatabase, "BookedEvents");



        // GET: api/Messages
        [Route("api/Messages/{EventID}")]
        public List<int> Get(int EventID)
        {
           
            var fillInts = Fillables.CreateFillSimpleType<int>("BookingId");
            IEnumerable<SimpleType<int>> listToConvert = MessagesManager.GetSelection(fillInts, EventsController.PrimaryKeys(EventID));
            List<int> bookingsToMessage = new List<int>();
            foreach (SimpleType<int> booking in listToConvert)
            {
                bookingsToMessage.Add(booking.Variable);
            }

            return bookingsToMessage;
            
        }

        // GET: api/Messages/5
        public string Get(string id)
        {
            return "value";
        }

        // POST: api/Messages
        public void Post([FromBody]Message message)
        {
            //string content = message.ToString();
            //string[] splitContent = content.Split(new [] {','},2);
            //string idString = splitContent[0].Remove(0, splitContent[0].IndexOf(":")+2);
            //string messageString = splitContent[1].Remove(0, splitContent[1].IndexOf(":")+2);
            //messageString = messageString.TrimEnd(new char[]{'}', '\r', '\n'}).Trim('"');
            //MessagesManager.Post(Extractables.ExtractMessage(Convert.ToInt32(idString), messageString));

            MessagesManager.Post(Extractables.ExtractMessage(message));

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
