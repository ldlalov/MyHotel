using Hotel.Core.Models;
using Hotel.Core.Services.Interfaces;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    public class RoomTypeController : BaseControler
    {
        private readonly IRoomTypeService roomTypeService;

        public RoomTypeController(IRoomTypeService _roomTypeService)
        {
            roomTypeService = _roomTypeService;   
        }

        // GET: RoomType

        // GET: RoomType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoomType/Create
        public ActionResult CreateType()
        {
            return View();
        }

        // POST: RoomType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateType(RoomTypeViewModel roomTypeViewModel)
        {

            try
            {
                var roomType = new RoomType();
                roomType.Type = roomTypeViewModel.Type;
                await roomTypeService.AddRoomType(roomType);

                return RedirectToAction(nameof(CreateType));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomType/Edit/5
        public async Task<IActionResult> EditType(int Id)
        {
            var roomType = await roomTypeService.GetRoomType(Id);
            var roomTypeModel = new RoomTypeViewModel()
            {
                Id = roomType.Id,
                Type = roomType.Type,
            };
            return View(roomTypeModel);
        }

        // POST: RoomType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  EditType(RoomTypeViewModel roomTypeViewModel)
        {
            try
            {
                var roomType = new RoomType
                {
                    Id = roomTypeViewModel.Id,
                    Type = roomTypeViewModel.Type,
                };
                await roomTypeService.UpdateRoomType(roomType);
                return RedirectToAction("RoomTypes","Room");
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
