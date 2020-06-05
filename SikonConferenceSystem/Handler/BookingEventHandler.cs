using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using ModelLibrary;
using SikonConferenceSystem.Persistency;
using SikonConferenceSystem.ViewModel;
using SikonConferenceSystem.ViewModel.Interfaces;

namespace SikonConferenceSystem.Handler
{
    public class BookingEventHandler
    {
        public Action<bool> OnClickedBook { get; set; }

        private BookingEventViewModel _bookingEventViewModel;

        public BookingEventHandler(BookingEventViewModel bookingEventViewModel)
        {
            _bookingEventViewModel = bookingEventViewModel;
        }

        

        public async void CreateBooking()
        {
            //int bookingId = _bookingEventViewModel.NewBooking.BookingID;
            //DateTime bookingDate = _bookingEventViewModel.NewBooking.BookingDate.Date;
            //int id = _bookingEventViewModel.NewBooking.Id;

            //Booking aBooking = new Booking(bookingId,bookingDate, id);
            //Consumer<Booking> bookingFacade = new Consumer<Booking>("http://localhost:61467/api/Bookings");
            //bool ok = await bookingFacade.PostAsync(aBooking);
            //ClearBooking();

            OnClickedBook?.Invoke(true);
        }

        public async void DeleteBooking()
        {
            int bookingId = _bookingEventViewModel.NewBooking.UserId;
            Persistency.Consumer<Booking> bookingFacade = new Persistency.Consumer<Booking>("http://localhost:61467/api/Bookings");
            bool ok = await bookingFacade.DeleteAsync(new []{(bookingId)});
            //ClearBooking();
        }

        //public async void AddEvent()
        //{
        //    _bookingEventViewModel.BookedEvents.Add(_bookingEventViewModel.NewEvent);

        //}
        //public async void DeleteEvent()//Har brug for at ændres til at slette med Id
        //{
        //    _bookingEventViewModel.BookedEvents.Remove(_bookingEventViewModel.NewEvent);

        //}

        //public async void ClearBooking()
        //{
        //    int bookingId = -1;
        //    DateTime bookingDate = new DateTime();
        //    int Id = -1;
        //}
    }
}
