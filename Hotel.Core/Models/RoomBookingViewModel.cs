using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Models
{
    public class RoomBookingViewModel
    {
        public IEnumerable<Hotel.Infrastructure.Data.Room> Rooms { get; set; }
        public IEnumerable<Hotel.Infrastructure.Data.Booking> Bookings { get; set; }
    }
}
