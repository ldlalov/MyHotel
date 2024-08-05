using Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hotel.Core.Models
{
    public class HotelViewModel
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
