using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Repositories.RoomRepository
{
    public interface IRoomRepository
    {
        public Task<Rooms[]> GetAll(
            int pageIndex,
            string? sortColumn,
            string? sortOrder,
            string? filterQuery);

        public Task<Rooms?> GetById(int id);
        public Task<Rooms?> Add(RoomDTO model);
        public Task<bool?> Edit(RoomDTO model);
        public Task<bool?> Delete(int id);
    }
}
