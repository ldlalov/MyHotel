using Hotel.Core;
using Hotel.Core.Models;
using Hotel.Core.Services;
using Hotel.Core.Services.Interfaces;
using Hotel.Infrastructure.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Claims;
using System.Text;

namespace Hotel.Controllers
{
    public class RoomController : BaseControler
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IHotelService hotelService;
        private readonly IRoomService service;
        private readonly IBookingService bookingService;
        private readonly IRoomTypeService roomTypeService;
        private List<RoomType> roomTypes;

        public RoomController(IHotelService _hotelService, IRoomService _service, IRoomTypeService _roomTypeService, IHttpContextAccessor _httpContextAccessor, IBookingService _bookingService)
        {
            hotelService = _hotelService;
            service = _service;
            roomTypeService = _roomTypeService;
            httpContextAccessor = _httpContextAccessor;
            bookingService = _bookingService;
        }
        //I need this action for my bookings table
        public async Task<IActionResult> RoomsAndBookingsInHotel(int hotelId)
        {
            var hotels = await hotelService.GetHotels();
            if (hotels.FirstOrDefault(h => h.Id == hotelId) == null && hotelId !=0)
            {
                return RedirectToAction("Index", "Hotel");
            }
                if (hotelId != 0)
            {
                HttpContext.Session.SetString("CurrentHotel", hotelId.ToString());//To use it from the "Create"
            }
            hotelId = int.Parse(HttpContext.Session.GetString("CurrentHotel"));
            var currentUser = User.Identity.GetUserId();
            var currentHotel = await hotelService.GetHotel(hotelId);
            if (currentHotel.UserId == currentUser)
            {
                var rooms = await service.GetRoomsInHotel(hotelId);
                var bookings = await bookingService.GetBookings(hotelId);

                var viewModel = new RoomBookingViewModel
                {
                    Rooms = rooms,
                    Bookings = bookings
                };

                return View(viewModel);

            }
            else
            {
                return RedirectToAction("Index", "Hotel");
            }
        }
        // GET: RoomController/Create
        public async Task<IActionResult> Create()
        {
            var hotelId = int.Parse(httpContextAccessor.HttpContext.Session.GetString("CurrentHotel"));
            roomTypes = await roomTypeService.GetRoomTypes();
            var roomViewModel = new RoomViewModel();
            roomViewModel.HotelId = hotelId;
            roomViewModel.roomTypes = roomTypes;
            roomViewModel.IsCleaned = true;
            roomViewModel.IsReserved = false;
            roomViewModel.IsFree = true;
            return View(roomViewModel);
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomViewModel roomViewModel)
        {
            roomViewModel.roomTypes = await roomTypeService.GetRoomTypes();
            ModelState.Remove("Hotel");
            ModelState.Remove("RoomType");
            ModelState.Remove("RoomTypes");
            try
            {
                var room = new Room
                {
                    Id = roomViewModel.Id,
                    Price = roomViewModel.Price,
                    RoomId = roomViewModel.RoomId,
                    RoomTypeId = roomViewModel.RoomTypeId,
                    RoomType = roomViewModel.RoomType,
                    IsCleaned = roomViewModel.IsCleaned,
                    IsFree = roomViewModel.IsFree,
                    IsReserved = roomViewModel.IsReserved,
                    HotelId = roomViewModel.HotelId,
                    Hotel = roomViewModel.Hotel

                };
                if (!ModelState.IsValid)
                {
                    return View(roomViewModel);
                }
                await service.AddRoom(room);

                // Redirect to a success page or return a success message
                return RedirectToAction("RoomsAndBookingsInHotel", roomViewModel.HotelId);//new { hotelId = roomViewModel.HotelId });

                // return RedirectToAction(nameof(RoomsInHotel));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Edit/5
        public async Task<IActionResult> EditRoom(int Id)
        {
            if (Id != 0 )
            {
                HttpContext.Session.SetString("roomId", Id.ToString());
            }
            Id = int.Parse(HttpContext.Session.GetString("roomId"));
            var room = await service.GetRoom(Id);
            var roommodel = new RoomViewModel
            {
                Id = room.Id,
                Price = room.Price,
                RoomId = room.RoomId,
                RoomTypeId = room.RoomTypeId,
                RoomType = room.RoomType,
                IsCleaned = room.IsCleaned,
                IsFree = room.IsFree,
                IsReserved = room.IsReserved,
                HotelId = room.HotelId,
                Hotel = room.Hotel,
                roomTypes = await roomTypeService.GetRoomTypes()
            };
            
            if (roommodel == null)
            {
                return NotFound(); // Or handle the case where the room is not found
            }

            return View("EditRoom",roommodel);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoom(RoomViewModel roomViewModel)
        {
            roomViewModel.roomTypes = await roomTypeService.GetRoomTypes();
            ModelState.Remove("Hotel");
            ModelState.Remove("RoomType");
            ModelState.Remove("RoomTypes");
            var room = new Room
            {
                Id = roomViewModel.Id,
                Price = roomViewModel.Price,
                RoomId = roomViewModel.RoomId,
                RoomTypeId = roomViewModel.RoomTypeId,
                IsCleaned = roomViewModel.IsCleaned,
                IsFree = roomViewModel.IsFree,
                IsReserved = roomViewModel.IsReserved,
                HotelId = roomViewModel.HotelId,
                Hotel = roomViewModel.Hotel
            };
            //Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(roomViewModel); // Return the view with validation errors
            }

            await service.UpdateRoom(room);

            // Redirect to a success page or return a success message
            return RedirectToAction("RoomsAndBookingsInHotel", new { hotelId = roomViewModel.HotelId }); // Redirect to the index page
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: RoomController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteRoom(id);
            return RedirectToAction("RoomsAndBookingsInHotel","Room");
        }

        // POST: RoomController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //I desided to work with RoomType in RoomController
        // GET: RoomType/Create
        public async Task<IActionResult> RoomTypes()
        {
            roomTypes = await roomTypeService.GetRoomTypes();
            return View(roomTypes);
        }

        // POST: RoomType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoomTypes(string newRoomType)
        {

            try
            {
                var roomType = new RoomType();
                roomType.Type = newRoomType;
                await roomTypeService.AddRoomType(roomType);
                roomTypes = await roomTypeService.GetRoomTypes();
                return View(roomTypes);
            }
            catch
            {
                roomTypes = await roomTypeService.GetRoomTypes();
                return View(roomTypes);
            }
        }

        // GET: RoomType/Edit/
        public async Task<IActionResult> EditType(int typeId)
        {
            var type = await roomTypeService.GetRoomType(typeId);

            return View("EditType",type);
        }

        // POST: RoomType/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditType(RoomType roomType)
        {
            try
            {
                await roomTypeService.UpdateRoomType(roomType);
                return RedirectToAction("RoomsInHotel");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> DeleteType(int id)
        {
            var roomsWithType = service.GetRooms().Result.ToList();
            if (roomsWithType.Any(room => room.RoomTypeId == id))
            {
                //must decline Type deleting
                roomTypes = await roomTypeService.GetRoomTypes();
                return RedirectToAction("RoomTypes", roomTypes);

            }
            await roomTypeService.DeleteRoomType(id);
            roomTypes = await roomTypeService.GetRoomTypes();
            return RedirectToAction("RoomTypes",roomTypes);
        }


    }
}
