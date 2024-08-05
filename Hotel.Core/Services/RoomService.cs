using Hotel.Core.Services.Interfaces;
using Hotel.Infrastructure.Data.Repositories;
using Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hotel.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicatioDbRepository repo;
        private List<Room> rooms = new List<Room>();
        public RoomService(ApplicatioDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddRoom(Room newRoom)
        {
            rooms.AddRange(await repo.All<Room>().Include(r => r.RoomType).ToListAsync());
            if (!rooms.Any(room => room.RoomId == newRoom.RoomId && room.HotelId == newRoom.HotelId))
            {
            await repo.AddAsync(newRoom);
            await repo.SaveChangesAsync();
            }
        }
        public async Task UpdateRoom(Room currentRoom)
        {
            var selectedRoom = await repo.GetByIdAsync<Room>(currentRoom.Id);
            selectedRoom.HotelId = currentRoom.HotelId;
            selectedRoom.RoomId = currentRoom.RoomId;
            selectedRoom.RoomTypeId = currentRoom.RoomTypeId;
            selectedRoom.Price = currentRoom.Price;
            selectedRoom.IsCleaned = currentRoom.IsCleaned;
            selectedRoom.IsFree = currentRoom.IsFree;
            selectedRoom.IsReserved = currentRoom.IsReserved;
            await repo.SaveChangesAsync();
        }

        public async Task<Room> GetRoom(int currentRoomid)
        {
            var result = await repo.All<Room>().Include(r => r.RoomType).FirstOrDefaultAsync(r => r.Id == currentRoomid);
            return result;
        }

        public async Task<List<Room>> GetRooms()
        {
            rooms.AddRange(await repo.All<Room>().Include(r => r.RoomType).ToListAsync());
            return rooms;
        }

        public async Task<List<Room>> GetRoomsInHotel(int hotelId)
        {
            var result = new List<Room>();
            result.AddRange(await repo.All<Room>().Where(rooms => rooms.HotelId == hotelId).ToListAsync());
            return result;
        }

        

        public async Task DeleteRoom(int currentRoomid)
        {
            await repo.DeleteAsync<Room>(currentRoomid);
            await repo.SaveChangesAsync();
        }
    }
}
