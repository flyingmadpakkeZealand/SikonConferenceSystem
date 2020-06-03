using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Booking
    {
        public int UserId { get; set; }
        public bool ReceiveMessages { get; set; }
        public List<int> BookedEventsId { get; set; }

        public Booking(int userId, bool receiveMessages)
        {
            UserId = userId;
            ReceiveMessages = receiveMessages;
        }

        public Booking()
        {
            
        }

    }
}
