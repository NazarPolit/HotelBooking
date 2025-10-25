using HotelBooking.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Interfaces
{
	public interface IStatsRepository
	{
		Task<IEnumerable<BookingStatsDto>> GetBookingStatsAsync();	
	}
}
