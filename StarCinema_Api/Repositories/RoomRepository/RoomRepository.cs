﻿using Microsoft.EntityFrameworkCore;
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
            if (!_context.Rooms.Any(room => !room.IsDelete && room.Name.Equals(model.Name)))
            {
                var rooms = new Rooms()
                {
                    Name = model.Name!
                };
                _context.Rooms.Add(rooms); 
                await _context.SaveChangesAsync();
                Rooms? r = _context.Rooms.FirstOrDefault(room => !room.IsDelete && room.Name.Equals(model.Name));
                if(r != null)
                {
                    char[] arr = new char[] { 'A', 'B', 'E', 'F' };
                    for (int i = 0; i < arr.Length; i++)
                    {
                        for (int j = 1; j < 10; j++)
                        {
                            _context.Seats.Add(new Seats { Name = arr[i] + "" + j, RoomId = r.Id });
                        }
                     }
                    await _context.SaveChangesAsync();
                }
                return rooms;
            }
            return null;
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

        async Task<Rooms?> IRoomRepository.GetById(int id)
        {
            return await _context.Rooms.Include(room => room.Seats).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
