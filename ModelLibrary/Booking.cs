using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime BookingDate { get; set; }
        public int Id { get; set; }

        public Booking(int bookingId, DateTime bookingDate, int id)
        {
            BookingID = bookingId;
            BookingDate = bookingDate;
            Id = id;
        }

        public Booking()
        {
            
        }

        public override string ToString()
        {
            return $"BookingId: {BookingID}";
        }
    }
}
