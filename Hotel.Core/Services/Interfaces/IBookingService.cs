using Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> AllBookings();
        Task<List<Booking>> GetBookings(int hotelId);
        Task AddBooking(Booking booking);
        Task<Booking> GetBooking(Guid id);
        Task UpdateBooking(Booking booking);
        Task DeleteBooking(Guid currentBooking);
    }
}
