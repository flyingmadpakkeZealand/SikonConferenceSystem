﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using ModelLibrary;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class BookingEventHandler
    {
        private BookingEventViewModel _bookingEventViewModel;

        public BookingEventHandler()
        {
            _bookingEventViewModel=new BookingEventViewModel();
        }

        public async void CreateBooking()
        {
            int bookingId = _bookingEventViewModel.NewBooking.BookingID;
            DateTime bookingDate = _bookingEventViewModel.NewBooking.BookingDate;
            int Id = _bookingEventViewModel.NewBooking.Id;
            Booking aBooking = new Booking(bookingId,bookingDate,Id);
            Persistency.Consumer<Booking> bookingFacade = new Persistency.Consumer<Booking>("http://localhost:61467/api/Bookings");
            bool ok = await bookingFacade.PostAsync(aBooking);
            ClearBooking();
        }

        public async void DeleteBooking()
        {
            int bookingId = _bookingEventViewModel.NewBooking.BookingID;
            Persistency.Consumer<Booking> bookingFacade = new Persistency.Consumer<Booking>("http://localhost:61467/api/Bookings");
            bool ok = await bookingFacade.DeleteAsync(new []{(bookingId)});
            ClearBooking();
        }

        public async void AddEvent()
        {
            _bookingEventViewModel.BookedEvents.Add(_bookingEventViewModel.NewEvent);

        }
        public async void DeleteEvent()//Har brug for at ændres til at slette med Id
        {
            _bookingEventViewModel.BookedEvents.Remove(_bookingEventViewModel.NewEvent);

        }

        public async void ClearBooking()
        {
            int bookingId = -1;
            DateTime bookingDate = new DateTime();
            int Id = -1;
        }
    }
}