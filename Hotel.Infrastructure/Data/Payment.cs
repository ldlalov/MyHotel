using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Data
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
