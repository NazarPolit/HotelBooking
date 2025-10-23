using HotelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<bool> IsRoomAvailableAsync(
            int roomId, 
            DateTime proposedCheckIn, 
            DateTime proposedCheckOut
        );

        Task<Booking?> GetBookingByIdAsync(int id);

        Task<ICollection<Booking>> GetAllBookingsByUserIdAsync(string userId);
		Task<ICollection<Booking>> GetAllBookingsAsync();

	}
}
