using AutoMapper;
using HotelBooking.Application.Dto;
using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Room> AddRoomAsync(CreateRoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);

            await _unitOfWork.Rooms.AddAsync(room);
            await _unitOfWork.CompleteAsync();

            return room;
        }

        public async Task<string?> DeleteRoomAsync(int roomId)
        {
            var room = await _unitOfWork.Rooms.GetByIdWithHotel(roomId);

            if (room == null)
            {
                return "Room not found.";
            }

            _unitOfWork.Rooms.Delete(room);
            await _unitOfWork.CompleteAsync();

            return null;
        }

        public async Task<string?> UpdateRoomAsync(int roomId, UpdateRoomDto dto)
        {
            var room = await _unitOfWork.Rooms.GetByIdWithHotel(roomId);

            if (room == null)
            {
                return "Room not found.";
            }

            _mapper.Map(dto, room);

            _unitOfWork.Rooms.Update(room);
            await _unitOfWork.CompleteAsync();

            return null;
        }
    }
}
