using Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public bool IsCleaned { get; set; }
        public bool IsReserved { get; set; }
        public bool IsFree { get; set; }
        public int HotelId { get; set; }
        public Building Hotel { get; set; }
        public List<RoomType> roomTypes { get; set; }
    }
}
