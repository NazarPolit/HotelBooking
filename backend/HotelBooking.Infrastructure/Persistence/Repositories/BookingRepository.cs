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
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context) { }

		public async Task<ICollection<Booking>> GetAllBookingsAsync()
		{
			return await _context.Bookings
				.Include(b => b.Room)
					.ThenInclude(r => r.Hotel)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<ICollection<Booking>> GetAllBookingsByUserIdAsync(string userId)
		{
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .AsNoTracking()
                .ToListAsync();
		}

		public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime proposedCheckIn, DateTime proposedCheckOut)
        {
            bool conflictExists = await _context.Bookings
                .AnyAsync(b =>
                    b.RoomId == roomId &&
                    (proposedCheckIn < b.DateTo && proposedCheckOut > b.DateFrom)
            );

            return !conflictExists;
        }
    }
}
