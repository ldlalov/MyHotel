using Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomers();
        Task AddCustomer(Customer customer);
        Task<Customer> GetCustomer(int id);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int customerId);

    }
}
