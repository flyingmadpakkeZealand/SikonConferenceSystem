using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime BookingDate { get; set; }
        public int CustomerPhoneNumber { get; set; }

        public Booking(int bookingId, DateTime bookingDate, int customerPhoneNumber)
        {
            BookingID = bookingId;
            BookingDate = bookingDate;
            CustomerPhoneNumber = customerPhoneNumber;
        }

        public Booking()
        {
            
        }
    }
}
