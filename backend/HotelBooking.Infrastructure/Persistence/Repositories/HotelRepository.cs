using HotelBooking.Application.Dto;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Interfaces;
using HotelBooking.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Persistence.Repositories
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
        }

		public Task<List<Hotel>> GetAvailableHotelsAsync(string city, DateTime dateFrom, DateTime dateTo)
		{
			return _context.Hotels
                        .Where(h => h.Address.Contains(city.ToLower()))
                        .Include(h => h.Rooms.Where(r =>
                                !r.Bookings.Any(b => dateFrom < b.DateTo && dateTo > b.DateFrom)
                        ))
                        .AsNoTracking()
                        .ToListAsync();
		}

		public async Task<Hotel?> GetHotelWithRooms(int id)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
