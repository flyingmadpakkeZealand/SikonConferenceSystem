using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DBUtility;
using ModelLibrary;
using RestAPISCS.App_Start;
using RestAPISCS.DatabaseUtility;

namespace RestAPISCS.Controllers
{
    public class RoomsController : ApiController
    {
        private ManageGenericWithLambda<Room> roomManager = DataBases.Access<Room>(BaseNames.SikonDatabase, "Room");

        private static Dictionary<string, object> PrimaryKeys(int roomNr)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("RoomNr", roomNr);
            return lookupDictionary;
        }

        // GET: api/Rooms
        public IEnumerable<Room> Get()
        { 
            return roomManager.Get(Fillables.FillRoom); 
        }

        // GET: api/Rooms/5
        public Room Get(int roomNr)
        {
            return roomManager.GetOne(Fillables.FillRoom, PrimaryKeys(roomNr));
        }

        // POST: api/Rooms
        public bool Post([FromBody]Room room)
        {
            return roomManager.Post(Extractables.ExtractRoom(room));
        }

        // PUT: api/Rooms/5
        public bool Put(int roomNr, [FromBody]Room room)
        {
            return roomManager.Put(Extractables.ExtractRoom(room), PrimaryKeys(roomNr));
        }

        // DELETE: api/Rooms/5
        public bool Delete(int roomNr)
        {
            return roomManager.Delete(PrimaryKeys(roomNr));
        }

        //Der mangler CheckNoDuplicate og RetrieveId

    }
}
