using AutoMapper;
using HotelBooking.Application.Dto;
using HotelBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAndUpdateHotelDto, Hotel>();
            CreateMap<Hotel, HotelDto>();
            CreateMap<Room, RoomDto>();
            CreateMap<Hotel, HotelDetailsDto>();
            CreateMap<Room, CreateRoomDto>();
            CreateMap<CreateRoomDto, Room>();
            CreateMap<Room, UpdateRoomDto>();
            CreateMap<UpdateRoomDto, Room>();
        }
    }
}
