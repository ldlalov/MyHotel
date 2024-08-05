using Hotel.Core;
using Hotel.Infrastructure.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Hotel.Controllers
{
    public class HotelController : BaseControler
    {
        private readonly IHotelService hotelService;
        private List<Building> hotels;

        public HotelController(IHotelService _hotelService)
        {
            hotelService = _hotelService;
        }
        // GET: HotelController
        public async Task<IActionResult> Index()
        {
            var userID = HttpContext.User.Identity.GetUserId();
            hotels = await hotelService.GetHotels();
            return View(hotels.Where(h => h.UserId == userID));
        }

        // GET: HotelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HotelController/Create
        public ActionResult CreateHotel()
        {
            return View();
        }

        // POST: HotelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHotel(string Name)
        {
            try
            {
                hotels = await hotelService.GetHotels();
                Building hotel = new Building();
                hotel.Name = Name;
                if (hotels.FirstOrDefault(h => h.Name == Name ) != null)
                {
                    TempData["ErrorMessageHotelExists"] = "Hotel with that name already exists!";
                    return RedirectToAction(nameof(Index));
                }

                hotel.UserId = HttpContext.User.Identity.GetUserId();
                hotel.Rooms = new List<Room>();
                await hotelService.AddHotel(hotel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HotelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HotelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HotelController/Delete/5
        public async Task<IActionResult> Delete(int hotelId)
        {
            await hotelService.DeleteHotel(hotelId);

            return RedirectToAction("Index");
        }

    }
}
