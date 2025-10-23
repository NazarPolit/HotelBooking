using AutoMapper;
using HotelBooking.Application.Dto;
using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Services
{
    public class BookingService : IBookigService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto, string userId)
        {
            var isAvailable = await _unitOfWork.Bookings
                .IsRoomAvailableAsync(dto.RoomId, dto.DateFrom, dto.DateTo);

            if (!isAvailable)
            {
                throw new InvalidOperationException("This room is not available for the selected dates.");
            }

            var booking = _mapper.Map<Booking>(dto);
            booking.UserId = userId;

            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            var createdBookingWithDetails = await _unitOfWork.Bookings
                .GetBookingByIdAsync(booking.Id);

            return _mapper.Map<BookingDto>(createdBookingWithDetails);
        }

		public async Task<ICollection<BookingDto>> GetAllBookingsAsync()
		{
			var bookings = await _unitOfWork.Bookings.GetAllBookingsAsync();

			return _mapper.Map<ICollection<BookingDto>>(bookings);
		}

		public async Task<BookingDto> GetBookingByIdAsync(int id, string currentUserId, bool isCurrentUserAdmin)
        {
            var booking = await _unitOfWork.Bookings.GetBookingByIdAsync(id);

            if (booking == null)
            {
                return null;
            }

            if(!isCurrentUserAdmin && booking.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("You do not have permission to view this booking.");
            }

            return _mapper.Map<BookingDto>(booking);
        }

		public async Task<ICollection<BookingDto>> GetMyBookingsAsync(string userId)
		{
			var bookings = await _unitOfWork.Bookings.GetAllBookingsByUserIdAsync(userId);

            return _mapper.Map<ICollection<BookingDto>> (bookings);
		}
	}
}
