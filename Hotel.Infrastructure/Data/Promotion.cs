using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data
{
    public class Promotion
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public string Type { get; set; }
        public int Period { get; set; }

    }
}
