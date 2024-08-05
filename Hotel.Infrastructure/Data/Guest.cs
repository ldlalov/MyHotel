using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string FifrsName { get; set; }

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }
        public string PersonalIdNumber { get; set; }
        public string? PersonalNumber { get; set; }

    }
}
