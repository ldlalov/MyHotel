using Hotel.Core.Services.Interfaces;
using Hotel.Infrastructure.Data;
using Hotel.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly ApplicatioDbRepository repo;
        private List<RoomType> roomTypes = new List<RoomType>();
        public RoomTypeService(ApplicatioDbRepository _repo)
        {
            repo = _repo;
        }
        public async Task DeleteRoomType(int id)
        {
            await repo.DeleteAsync<RoomType>(id);
            await repo.SaveChangesAsync();
        }

        public async Task AddRoomType(RoomType newRoomType)
        {
            roomTypes.AddRange(await repo.All<RoomType>().ToListAsync());
            if (!roomTypes.Any(roomType => roomType.Type == newRoomType.Type))
                await repo.AddAsync(newRoomType);
            await repo.SaveChangesAsync();
        }
        public async Task UpdateRoomType(RoomType roomType)
        {
            var selectedType = await repo.GetByIdAsync<RoomType>(roomType.Id);
            selectedType.Type = roomType.Type;
            await repo.SaveChangesAsync();
        }

        public async Task<List<RoomType>> GetRoomTypes()
        {
            roomTypes.Clear();
            roomTypes.AddRange(await repo.All<RoomType>().ToListAsync());
            return roomTypes;
        }

        public async Task<RoomType> GetRoomType(int id)
        {
           return await repo.GetByIdAsync<RoomType>(id);
        }

    }
}
