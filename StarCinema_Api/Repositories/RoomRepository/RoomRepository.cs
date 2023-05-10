using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using System.Linq.Dynamic.Core;

namespace StarCinema_Api.Repositories.RoomRepository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ILogger<RoomRepository> _logger;
        private readonly MyDbContext _context;

        public RoomRepository(ILogger<RoomRepository> logger,
            MyDbContext context)
        {
            this._logger = logger;
            this._context = context;
        }
        async Task<Rooms?> IRoomRepository.Add(RoomDTO model)
        {
            var rooms = new Rooms()
            {
                Name = model.Name!
            };
            _context.Rooms.Add(rooms);

            await _context.SaveChangesAsync();
            return rooms;
        }

        async Task<bool?> IRoomRepository.Delete(int id)
        {
            var room = await _context.Rooms
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                return true;
            };
            return false;
        }

        async Task<bool?> IRoomRepository.Edit(RoomDTO model)
        {
            var room = await _context.Rooms
                .Where(b => b.Id == model.Id)
                .FirstOrDefaultAsync();
            if (room != null)
            {
                room.Name = model.Name;
                room.IsDelete = model.IsDelete;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();
                return true;
            };
            return false;
        }

        async Task<Rooms[]> IRoomRepository.GetAll(int pageIndex, string? sortColumn, string? sortOrder, string? filterQuery)
        {
            var query = _context.Rooms.AsQueryable();
            if (!string.IsNullOrEmpty(filterQuery))
                query = query.Where(b => b.Name.Contains(filterQuery));
            var recordCount = await query.CountAsync();
            query = query.OrderBy($"{sortColumn} {sortOrder}")
                .Skip(pageIndex * 10)
                .Take(10);
            var rooms = await query.ToArrayAsync();
            return rooms;
        }

        Task<RestDTO<Rooms?>> IRoomRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
