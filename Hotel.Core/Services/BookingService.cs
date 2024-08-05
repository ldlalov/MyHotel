using Hotel.Core.Services.Interfaces;
using Hotel.Infrastructure.Data;
using Hotel.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicatioDbRepository repo;
        private List<Booking> bookings = new List<Booking>();
        public BookingService(ApplicatioDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddBooking(Booking booking)
        {
            await repo.AddAsync(booking);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteBooking(Guid currentBooking)
        {
            await repo.DeleteAsync<Booking>(currentBooking);
            await repo.SaveChangesAsync();
        }

        public async Task<Booking> GetBooking(Guid id)
        {
            bookings.AddRange(await repo.All<Booking>().ToListAsync());
            var booking =  bookings.FirstOrDefault(b => b.Id == id);
            return booking;
        }
        public async Task<List<Booking>> AllBookings()
        {
            bookings.AddRange(await repo.All<Booking>().ToListAsync());
            return bookings;
        }


        public async Task<List<Booking>> GetBookings(int hotelId)
        {
            bookings.AddRange(await repo.All<Booking>().Where(b => b.HotelId == hotelId).ToListAsync());
            return bookings;
        }

        public async Task UpdateBooking(Booking booking)
        {
            var selectedBooking = await repo.GetByIdsAsync<Booking>(new object[] { booking.Id });
            selectedBooking.CustomerId = booking.CustomerId;
            selectedBooking.DateFrom = booking.DateFrom;
            selectedBooking.DateTo = booking.DateTo;
            await repo.SaveChangesAsync();
        }
    }
}
