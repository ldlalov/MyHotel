using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Infrastructure.Data
{
    public class Building
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Display(Name ="Name")]
        public string Name { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
