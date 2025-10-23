using HotelBooking.Application.Dto;
using HotelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Interfaces
{
    public interface IBookigService
    {
        Task<BookingDto> CreateBookingAsync(CreateBookingDto dto, string userId);
        Task<BookingDto> GetBookingByIdAsync(int id, string currentUserId, bool isCurrentUserAdmin);
		Task<ICollection<BookingDto>> GetMyBookingsAsync(string userId);
		Task<ICollection<BookingDto>> GetAllBookingsAsync();
	}
}
