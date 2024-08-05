using Hotel.Core.Services.Interfaces;
using Hotel.Infrastructure.Data;
using Hotel.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicatioDbRepository repo;
        private List<Customer> customers = new List<Customer>();
        public CustomerService(ApplicatioDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddCustomer(Customer customer)
        {
            await repo.AddAsync(customer);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteCustomer(int customerId)
        {
            await repo.DeleteAsync<Customer>(customerId);
            await repo.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            await GetCustomers();
            var result = customers.FirstOrDefault(c => c.Id == id);
            return result;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            customers.Clear();
            customers.AddRange(await repo.All<Customer>().ToListAsync());
            return customers;
            
        }

        public async Task UpdateCustomer(Customer customer)
        {
            var selectedCustomer = await GetCustomer(customer.Id);
            selectedCustomer.FirstName = customer.FirstName;
            selectedCustomer.LastName = customer.LastName;
            await repo.SaveChangesAsync();
        }
    }
}
