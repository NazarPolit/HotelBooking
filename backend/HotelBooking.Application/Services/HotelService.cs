using AutoMapper;
using HotelBooking.Application.Dto;
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

    }
}
