using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Infrastructure.Data
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(10)]
        public string RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public bool IsCleaned { get; set; }
        public bool IsReserved { get; set; }
        public bool IsFree { get; set; }
        public int HotelId { get; set; }
        public Building Hotel { get; set; }
        public ICollection<Booking> bookings { get; set; } = new List<Booking>();

    }
}
