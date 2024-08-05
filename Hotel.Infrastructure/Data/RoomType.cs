using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data
{
    public class RoomType
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Type { get; set; }
    }
}
