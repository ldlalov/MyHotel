using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data
{
    public class Booking
    {
        public Guid Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int HotelId { get; set; }
        public Building Hotel { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
