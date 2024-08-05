using Hotel.Core.Models;
using Hotel.Infrastructure.Data;

namespace Hotel.Core
{
    public interface IHotelService
    {
        Task<List<Building>> GetHotels();
        Task<List<Room>> GetRoomsInHotel(int hotel);
        Task AddHotel(Building newHotel);
        Task<Building> GetHotel(int hotel);
        Task<Building> GetHotelByName(string hotelName);
        Task DeleteHotel(int id);
    }
}