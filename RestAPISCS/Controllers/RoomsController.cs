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
        public Room Get(int id)
        {
            return roomManager.GetOne(Fillables.FillRoom, PrimaryKeys(id));
        }

        // POST: api/Rooms
        public bool Post([FromBody]Room room)
        {
            return roomManager.Post(Extractables.ExtractRoom(room));
        }

        // PUT: api/Rooms/5
        public bool Put(int id, [FromBody]Room room)
        {
            return roomManager.Put(Extractables.ExtractRoom(room), PrimaryKeys(id));
        }

        // DELETE: api/Rooms/5
        public bool Delete(int id)
        {
            return roomManager.Delete(PrimaryKeys(id));
        }

        //Der mangler CheckNoDuplicate og RetrieveId

    }
}
