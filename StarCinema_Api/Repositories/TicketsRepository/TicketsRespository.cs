﻿using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.TicketsRepository
{
    public class TicketsRespository : BaseRepository<Tickets>, ITicketsRepository
    {
        private readonly MyDbContext _context;
        public TicketsRespository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetLastIDTicket()
        {
            return _context.Tickets.OrderBy(t => t.Id).LastOrDefaultAsync().Result.Id;
        }
    }
}
