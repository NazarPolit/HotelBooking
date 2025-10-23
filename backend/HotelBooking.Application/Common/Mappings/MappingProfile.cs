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

            CreateMap<CreateBookingDto, Booking>();
            CreateMap<CreateBookingDto, BookingDto>();
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.RoomCapacity, opt => opt.MapFrom(src => src.Room.Capacity))
                .ForMember(dest => dest.RoomPricePerNight, opt => opt.MapFrom(src => src.Room.PricePerNight))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.Room.Hotel.Id))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Room.Hotel.Name))
                .ForMember(dest => dest.HotelAddress, opt => opt.MapFrom(src => src.Room.Hotel.Address))
                
                .ForMember(dest => dest.NumberOfDays, opt => opt.MapFrom(src => (src.DateTo - src.DateFrom).Days))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => (src.DateTo - src.DateFrom).Days * src.Room.PricePerNight));
        }
    }
}
