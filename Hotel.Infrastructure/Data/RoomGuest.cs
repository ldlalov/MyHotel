using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data
{
    public class RoomGuest
    {
        [Key]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
    }
}
