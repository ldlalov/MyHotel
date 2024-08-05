using Hotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services.Interfaces
{
    public interface IRoomTypeService
    {
        public Task<List<RoomType>> GetRoomTypes();
        public Task AddRoomType(RoomType roomType);
        public Task UpdateRoomType(RoomType roomType);
        public Task DeleteRoomType(int id);
        public Task<RoomType> GetRoomType(int id);


    }
}
