using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Room
    {
        public int RoomNr { get; set; }
        public int RoomMaxPersons { get; set; }
        public int AutistSeats { get; set; }

        public Room(int roomNr, int roomMaxPersons, int autistSeats)
        {
            RoomNr = roomNr;
            RoomMaxPersons = roomMaxPersons;
            AutistSeats = autistSeats;
        }

        public Room()
        {
            
        }
    }
}
