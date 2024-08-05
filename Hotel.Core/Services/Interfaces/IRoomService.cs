using Hotel.Core.Models;
using Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services.Interfaces
{
    public interface IRoomService
    {
            Task<List<Room>> GetRooms();
            Task AddRoom(Room newRoom);
            Task UpdateRoom(Room currentRoom);
            Task<Room> GetRoom(int currentRoomid);
            Task DeleteRoom(int currentRoomid);
            Task<List<Room>> GetRoomsInHotel(int hotelId);
    }
}
