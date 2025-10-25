using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Application.Dto;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Application.Interfaces
{
    public interface IHotelService
    {
        Task<HotelDto> CreateHotelAsync(CreateAndUpdateHotelDto dto);
        Task<HotelDetailsDto?> GetHotelWithRooms(int id);
        Task<List<HotelDto>> GetAllHotelsAsync();
        Task<List<HotelDetailsDto>> SearchAvailableRoomsAsync(
            string city,
            DateTime dateFrom,
            DateTime dateTo
            );
        Task<string?> DeleteHotelAsync(int id);
        Task<string?> UpdateHotelAsync(int id, CreateAndUpdateHotelDto dto);
    }
}
