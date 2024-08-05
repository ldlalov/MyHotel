using Hotel.Infrastructure.Data;
using Hotel.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Core
{
    public class HotelService : IHotelService
    {
        //private int hotelId;
        private readonly ApplicatioDbRepository repo;
        private List<Building> hotels = new List<Building>();
        private List<Room> rooms = new List<Room>();
        public HotelService(ApplicatioDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<List<Building>> GetHotels()
        {
            hotels.AddRange(await repo.All<Building>().ToListAsync());
            return hotels;
        }
        public async Task AddHotel(Building newHotel)
        {
            //hotels.AddRange(await repo.All<Building>().ToListAsync());
            hotels = await GetHotels();
            if (!hotels.Any(hotel => hotel.Name == newHotel.Name))
            {
                await repo.AddAsync(newHotel);
                await repo.SaveChangesAsync();
            }
        }

        public async Task<Building> GetHotel(int hotel)

        {
            await GetHotels();
            var result = hotels.FirstOrDefault(x => x.Id == hotel);
            return result;
        }
        public async Task<Building> GetHotelByName(string hotelName)
        {
            await GetHotels();
            var result = hotels.FirstOrDefault(h => h.Name == hotelName);
            return result;
        }

        public async Task DeleteHotel(int id)
        {
            await repo.DeleteAsync<Building>(id);
            await repo.SaveChangesAsync();
        }

        public async Task<List<Room>> GetRoomsInHotel(int hotelId)
        {
            var temp = await GetHotel(hotelId);
            rooms.AddRange(await repo.All<Room>().Where(r => r.Hotel == temp).ToListAsync());
            return rooms;
        }

    }
}
