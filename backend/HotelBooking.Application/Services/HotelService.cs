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
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<HotelDto> CreateHotelAsync(CreateAndUpdateHotelDto dto)
        {
            var hotel = _mapper.Map<Hotel>(dto);

            await _unitOfWork.Hotels.AddAsync(hotel);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<HotelDto>(hotel);
        }

        public async Task<string?> DeleteHotelAsync(int id)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAsync(id);

            if (hotel == null)
            {
                return "Hotel not found.";
            }

            _unitOfWork.Hotels.Delete(hotel);
            await _unitOfWork.CompleteAsync();

            return null;
        }

        public async Task<List<HotelDto>> GetAllHotelsAsync()
        {
            var hotels = await _unitOfWork.Hotels.GetAllAsync();
            var hotelsDto = _mapper.Map<List<HotelDto>>(hotels);

            return hotelsDto;

        }

        public async Task<HotelDetailsDto?> GetHotelWithRooms(int id)
        {
            var hotel = await _unitOfWork.Hotels.GetHotelWithRooms(id);

            if (hotel == null)
                return null;

            return _mapper.Map<HotelDetailsDto>(hotel);
        }

        public async Task<string?> UpdateHotelAsync(int id, CreateAndUpdateHotelDto dto)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAsync(id);

            if (hotel == null)
                return null;

            _mapper.Map(dto, hotel);

            _unitOfWork.Hotels.Update(hotel);
            await _unitOfWork.CompleteAsync();

            return null;

        }
    }
}
