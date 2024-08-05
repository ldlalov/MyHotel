using Hotel.Core.Models;
using Hotel.Core.Services;
using Hotel.Core.Services.Interfaces;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class BookingController : BaseControler
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBookingService service;
        private readonly ICustomerService customerService;
        private readonly IRoomService roomService;
        public BookingController(IBookingService _service, ICustomerService _customerService, IRoomService _roomService, IHttpContextAccessor _httpContextAccessor)
        {
            service = _service;
            customerService = _customerService;
            roomService = _roomService;
            httpContextAccessor = _httpContextAccessor;
        }
        // GET: BookingController
        public async Task<IActionResult> BookingsInHotel(int hotelId)
        {
            if (hotelId != 0)
            {
                HttpContext.Session.SetString("CurrentHotel", hotelId.ToString());//To use it from the "Create"
            }

            hotelId = int.Parse(httpContextAccessor.HttpContext.Session.GetString("CurrentHotel"));
            var bookings = await service.GetBookings(hotelId);
            foreach (var booking in bookings)
            {
                var room = await roomService.GetRoom(booking.RoomId);
            }
            //return View("BookingsInHotel",bookings);
            return Ok(bookings);
        }


        // GET: BookingController/Create
        public async Task<IActionResult> CreateBooking(int roomId, int hotelId)
        {
            if (hotelId != 0)
            {
                HttpContext.Session.SetString("CurrentHotel", hotelId.ToString());//To use it from the "Create"
            }

            if (roomId != 0)
            {
                HttpContext.Session.SetString("CurrentRoom", roomId.ToString());//To use it from the "Create"
            }

            hotelId = int.Parse(httpContextAccessor.HttpContext.Session.GetString("CurrentHotel"));
            roomId = int.Parse(httpContextAccessor.HttpContext.Session.GetString("CurrentRoom"));
            
            var booking = new BookingViewModel
            {
                BookingDate = DateTime.Now,
                HotelId = hotelId,
                RoomId = roomId,
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(1),
                customers = await customerService.GetCustomers()
            };
            return View(booking);
        }

        // POST: BookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking(BookingViewModel bookingViewModel)
        {
            bookingViewModel.customers = await customerService.GetCustomers();
            ModelState.Remove("Hotel");
            ModelState.Remove("Room");
            ModelState.Remove("Customer");
            ModelState.Remove("customers");

            try
            {
                var booking = new Booking();
                booking.BookingDate = DateTime.Now;
                booking.CustomerId = bookingViewModel.CustomerId;
                booking.RoomId = bookingViewModel.RoomId;
                booking.HotelId = bookingViewModel.HotelId;
                booking.DateFrom = bookingViewModel.DateFrom;
                booking.DateTo = bookingViewModel.DateTo;
                var bookings = await service.AllBookings();
                var currentRoomBookings = bookings.Where(b => b.RoomId == booking.RoomId);
                if (!ModelState.IsValid)
                {
                    return View(bookingViewModel);

                }
                if(currentRoomBookings.Any(b => (booking.DateFrom >= b.DateFrom && booking.DateFrom < b.DateTo)
                    || (booking.DateTo > b.DateFrom && booking.DateTo < b.DateTo)
                    )
                    || (booking.DateTo <= booking.DateFrom))
                {
                    TempData["ErrorMessageBookingExists"] = "Room not free for selected period!";
                    return View(bookingViewModel);
                }
                await service.AddBooking(booking);
                return RedirectToAction("RoomsAndBookingsInHotel", "Room");

            }
            catch
            {
                return View();
            }
        }

        // GET: BookingController/Edit/5
        public async Task<IActionResult> EditBooking(Guid id)
        {
            var booking = await service.GetBooking(id);
            var bookingView = new BookingViewModel {
                RoomId = booking.RoomId,
                HotelId = booking.HotelId,
                DateFrom = booking.DateFrom,
                DateTo = booking.DateTo,
                CustomerId = booking.CustomerId,
                Customer = await customerService.GetCustomer(booking.CustomerId),
                customers = await customerService.GetCustomers()
                };
            return View("EditBooking",bookingView);
        }

        // POST: BookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBooking(BookingViewModel bookingViewModel)
        {
            bookingViewModel.customers = await customerService.GetCustomers();
            ModelState.Remove("Hotel");
            ModelState.Remove("Room");
            ModelState.Remove("Customer");
            ModelState.Remove("customers");
            try
            {
                Booking booking = new Booking();
                booking.Id = bookingViewModel.Id;
                booking.BookingDate = DateTime.Now;
                booking.DateFrom = bookingViewModel.DateFrom;
                booking.DateTo = bookingViewModel.DateTo;
                booking.RoomId = bookingViewModel.RoomId;
                booking.CustomerId = bookingViewModel.CustomerId;
                booking.Customer = bookingViewModel.Customer = await customerService.GetCustomer(bookingViewModel.CustomerId);
                var bookings = await service.AllBookings();
                var currentRoomBookings = bookings.Where(b => b.RoomId == booking.RoomId && b.Id != booking.Id);
                if (!ModelState.IsValid
                    || currentRoomBookings.Any(b => (booking.DateFrom >= b.DateFrom && booking.DateFrom < b.DateTo)
                    || (booking.DateTo > b.DateFrom && booking.DateTo < b.DateTo)
                    )
                    || (booking.DateTo <= booking.DateFrom))
                {
                    return View(bookingViewModel);
                }
                await service.UpdateBooking(booking);
                return RedirectToAction("RoomsAndBookingsInHotel","Room");

            }
            catch
            {
                return View();
            }
        }

        // GET: BookingController/Delete/5
        public async Task<IActionResult> Delete(Guid Id)
        {
            await service.DeleteBooking(Id);
            return RedirectToAction("RoomsAndBookingsInHotel","Room");
        }
    }
}
