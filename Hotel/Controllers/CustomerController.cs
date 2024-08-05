using Hotel.Core;
using Hotel.Core.Models;
using Hotel.Core.Services.Interfaces;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService service;
        public CustomerController(ICustomerService _service)
        {
            service = _service;
        }

        // GET: CustomerController
        public async Task<IActionResult> AllCustomers()
        {
            var customers = await service.GetCustomers();
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult CreateCustomer()
        {
            var customer = new CustomerViewModel();
            return View(customer);
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(CustomerViewModel customerViewModel)
        {
            try
            {
                var customer = new Customer();
                customer.FirstName = customerViewModel.FirstName;
                customer.LastName = customerViewModel.LastName;
                await service.AddCustomer(customer);
                return RedirectToAction("AllCustomers", "Customer");
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<IActionResult> EditCustomer(int id)
        {
            var customer = await service.GetCustomer(id);
            var customerModel = new CustomerViewModel()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
            };
            return View(customerModel);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(CustomerViewModel customerModel)
        {
            try
            {
                var customer = new Customer
                {
                    Id= customerModel.Id,
                    FirstName = customerModel.FirstName,
                    LastName = customerModel.LastName,
                };
                await service.UpdateCustomer(customer);
                return RedirectToAction(nameof(AllCustomers));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteCustomer(id);
            return RedirectToAction("AllCustomers", "Customer");
        }
    }
}
