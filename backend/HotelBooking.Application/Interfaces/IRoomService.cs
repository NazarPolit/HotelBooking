using HotelBooking.Application.Dto;
using HotelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Interfaces
{
    public interface IRoomService
    {
        Task<Room> AddRoomAsync(CreateRoomDto dto);
        Task<string?> UpdateRoomAsync(int roomId, UpdateRoomDto dto);
        Task<string?> DeleteRoomAsync(int roomId);
        Task<RoomDto> GetRoomByIdAsync(int roomId);
    }
}
